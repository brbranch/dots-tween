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
        protected override void OnUpdate()
        {
            var destroyBufferFromEntity = GetBufferLookup<TweenDestroyCommand>(true);

            EndSimulationEntityCommandBufferSystem endSimEcbSystem = World.GetOrCreateSystemManaged<EndSimulationEntityCommandBufferSystem>();
            EntityCommandBuffer.ParallelWriter parallelWriter = endSimEcbSystem.CreateCommandBuffer().AsParallelWriter();

            Entities
                .WithReadOnly(destroyBufferFromEntity)
                .WithAll<TweenStopCommand>()
                .ForEach((int entityInQueryIndex, Entity entity, ref DynamicBuffer<TweenState> tweenBuffer) =>
                {
                    foreach (var tween in tweenBuffer)
                    {
                        if (!destroyBufferFromEntity.HasBuffer(entity))
                        {
                            parallelWriter.AddBuffer<TweenDestroyCommand>(entityInQueryIndex, entity);
                        }

                        parallelWriter.AppendToBuffer(entityInQueryIndex, entity, new TweenDestroyCommand(tween.Id));
                    }

                    parallelWriter.RemoveComponent<TweenStopCommand>(entityInQueryIndex, entity);

                    if (SystemAPI.HasComponent<TweenPause>(entity))
                    {
                        parallelWriter.RemoveComponent<TweenPause>(entityInQueryIndex, entity);
                    }
                }).ScheduleParallel();

            endSimEcbSystem.AddJobHandleForProducer(Dependency);
        }
    }
}