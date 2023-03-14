using Unity.Burst;
using Unity.Entities;

namespace DotsTween.Tweens
{
    [BurstCompile]
    [UpdateInGroup(typeof(TweenSimulationSystemGroup))]
    [UpdateAfter(typeof(TweenStateSystem))]
    internal partial class TweenPauseSystem : SystemBase
    {
        [BurstCompile]
        protected override void OnCreate()
        {
            RequireForUpdate<TweenPauseCommand>();
        }

        [BurstCompile]
        protected override void OnUpdate()
        {
            EndSimulationEntityCommandBufferSystem endSimEcbSystem = World.GetOrCreateSystemManaged<EndSimulationEntityCommandBufferSystem>();
            EntityCommandBuffer.ParallelWriter parallelWriter = endSimEcbSystem.CreateCommandBuffer().AsParallelWriter();

            Entities
                .WithAll<TweenPauseCommand>()
                .ForEach((int entityInQueryIndex, Entity entity, ref DynamicBuffer<TweenPauseInfo> pauseBuffer, ref DynamicBuffer<TweenState> tweenBuffer) =>
            {
                for (int x = pauseBuffer.Length - 1; x >= 0; --x)
                {
                    var pauseInfo = pauseBuffer[x];

                    for (int y = 0; y < tweenBuffer.Length; ++y)
                    {
                        var tween = tweenBuffer[y];

                        if (pauseInfo.SpecificTweenTarget && pauseInfo.TweenId != tween.Id) continue;

                        tween.IsPaused = true;
                        tweenBuffer[y] = tween;
                    }
                    
                    pauseBuffer.RemoveAt(x);
                }
                
                parallelWriter.RemoveComponent<TweenPauseCommand>(entityInQueryIndex, entity);
            }).ScheduleParallel();

            endSimEcbSystem.AddJobHandleForProducer(Dependency);
        }
    }
}