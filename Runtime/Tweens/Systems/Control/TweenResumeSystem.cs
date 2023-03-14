using Unity.Burst;
using Unity.Entities;

namespace DotsTween.Tweens
{
    [BurstCompile]
    [UpdateInGroup(typeof(TweenSimulationSystemGroup))]
    [UpdateAfter(typeof(TweenStateSystem))]
    internal partial class TweenResumeSystem : SystemBase
    {
        [BurstCompile]
        protected override void OnCreate()
        {
            RequireForUpdate<TweenResumeCommand>();
        }

        [BurstCompile]
        protected override void OnUpdate()
        {
            var endSimEcbSystem = World.GetOrCreateSystemManaged<EndSimulationEntityCommandBufferSystem>();
            var parallelWriter = endSimEcbSystem.CreateCommandBuffer().AsParallelWriter();

            Entities
                .WithAll<TweenResumeCommand>()
                .ForEach((int entityInQueryIndex, Entity entity, ref DynamicBuffer<TweenResumeInfo> resumeBuffer, ref DynamicBuffer<TweenState> tweenBuffer) =>
                {
                    for (int x = resumeBuffer.Length - 1; x >= 0; --x)
                    {
                        var resumeInfo = resumeBuffer[x];

                        for (int y = 0; y < tweenBuffer.Length; ++y)
                        {
                            var tween = tweenBuffer[y];

                            if (resumeInfo.SpecificTweenTarget && resumeInfo.TweenId != tween.Id) continue;

                            tween.IsPaused = false;
                            tweenBuffer[y] = tween;
                        }
                    
                        resumeBuffer.RemoveAt(x);
                    }
                
                    parallelWriter.RemoveComponent<TweenResumeCommand>(entityInQueryIndex, entity);
                }).ScheduleParallel();

            endSimEcbSystem.AddJobHandleForProducer(Dependency);
        }
    }
}