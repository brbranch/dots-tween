using Unity.Burst;
using Unity.Entities;

namespace DotsTween.Tweens
{
    [BurstCompile]
    [UpdateInGroup(typeof(TweenSimulationSystemGroup))]
    [UpdateAfter(typeof(TweenStateSystem))]
    [UpdateBefore(typeof(TweenDestroySystemGroup))]
    internal partial class TweenStopSystem : SystemBase
    {
        [BurstCompile]
        protected override void OnCreate()
        {
            RequireForUpdate<TweenStopCommand>();
        }

        [BurstCompile]
        protected override void OnUpdate()
        {
            var destroyBufferFromEntity = GetBufferLookup<TweenDestroyCommand>(true);
            
            EndSimulationEntityCommandBufferSystem endSimEcbSystem = World.GetOrCreateSystemManaged<EndSimulationEntityCommandBufferSystem>();
            EntityCommandBuffer.ParallelWriter parallelWriter = endSimEcbSystem.CreateCommandBuffer().AsParallelWriter();

            Entities
                .WithReadOnly(destroyBufferFromEntity)
                .WithAll<TweenStopCommand>()
                .ForEach((int entityInQueryIndex, Entity entity, ref DynamicBuffer<TweenStopInfo> stopBuffer, ref DynamicBuffer<TweenState> tweenBuffer) =>
                {
                    for (int x = stopBuffer.Length - 1; x >= 0; --x)
                    {
                        var resumeInfo = stopBuffer[x];

                        for (int y = 0; y < tweenBuffer.Length; ++y)
                        {
                            var tween = tweenBuffer[y];

                            if (resumeInfo.SpecificTweenTarget && resumeInfo.TweenId != tween.Id) continue;
                            
                            if (!destroyBufferFromEntity.HasBuffer(entity))
                            {
                                parallelWriter.AddBuffer<TweenDestroyCommand>(entityInQueryIndex, entity);
                            }

                            parallelWriter.AppendToBuffer(entityInQueryIndex, entity, new TweenDestroyCommand(tween.Id));
                            tweenBuffer[y] = tween;
                        }
                    
                        stopBuffer.RemoveAt(x);
                    }
                
                    parallelWriter.RemoveComponent<TweenStopCommand>(entityInQueryIndex, entity);
                }).ScheduleParallel();

            endSimEcbSystem.AddJobHandleForProducer(Dependency);
        }
    }
}