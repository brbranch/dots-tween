using Unity.Entities;

namespace DotsTween.Tweens
{
    [UpdateInGroup(typeof(TweenSimulationSystemGroup))]
    [UpdateAfter(typeof(TweenStateSystem))]
    internal partial class TweenResumeSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var endSimEcbSystem = World.GetOrCreateSystemManaged<EndSimulationEntityCommandBufferSystem>();
            var parallelWriter = endSimEcbSystem.CreateCommandBuffer().AsParallelWriter();
            var pauseFromEntity = GetComponentLookup<TweenPause>();

            Entities
                .WithReadOnly(pauseFromEntity)
                .WithAll<TweenResumeCommand>()
                .ForEach((int entityInQueryIndex, Entity entity) =>
                {
                    if (pauseFromEntity.HasComponent(entity))
                    {
                        parallelWriter.RemoveComponent<TweenPause>(entityInQueryIndex, entity);
                    }

                    parallelWriter.RemoveComponent<TweenResumeCommand>(entityInQueryIndex, entity);
                }).ScheduleParallel();

            endSimEcbSystem.AddJobHandleForProducer(Dependency);
        }
    }
}