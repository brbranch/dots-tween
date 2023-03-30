using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

namespace DotsTween.Timelines.Systems
{
    [UpdateInGroup(typeof(TimelineSimulationSystemGroup))]
    [BurstCompile]
    public partial class TimelinePlaybackSystem : SystemBase
    {
        [BurstCompile]
        protected override void OnCreate()
        {
            RequireForUpdate<TimelineComponent>();
        }

        [BurstCompile]
        protected override void OnUpdate()
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            var ecb = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(CheckedStateRef.WorldUnmanaged);

            foreach (var (timeline, timelineEntity) in SystemAPI.Query<RefRW<TimelineComponent>>().WithNone<TimelinePausedTag, TimelineDestroyTag>().WithEntityAccess())
            {
                timeline.ValueRW.CurrentTime += deltaTime;

                // Perform Component operations after the initial Timeline Delay (if any)
                if (timeline.ValueRO is { IsPlaying: false, CurrentTime: >= 0f })
                {
                    timeline.ValueRW.IsPlaying = true;
                    PerformComponentOperations(ref ecb, ref timeline.ValueRW.OnStart);
                }

                var bufferReader = timeline.ValueRW.GetTimelineReader();
                var index = 0;

                while (!bufferReader.EndOfBuffer && index < timeline.ValueRO.Size)
                {
                    var componentType = timeline.ValueRO.GetTimelineElementType(index);
                    var timelineElement = TimelineSystemCommandTypeHelper.DereferenceNextTimelineElement(componentType, ref bufferReader);
                    TryToStartPlayingTimelineElement(componentType, ref timelineElement, ref timeline.ValueRW, ref ecb);
                    TryToRemoveTimelineElementIdFromActiveList(ref timelineElement, ref timeline.ValueRW);
                    ++index;
                }

                TryToDestroyTimeline(ref timeline.ValueRW, timelineEntity, ref ecb);
            }
        }

        [BurstCompile]
        private void PerformComponentOperations(ref EntityCommandBuffer ecb, ref NativeList<TimelineComponentOperationTuple> operations)
        {
            foreach (var tuple in operations)
            {
                tuple.Operations.Perform(ref ecb, tuple.Target);
            }
        }

        [BurstCompile]
        private void TryToDestroyTimeline(ref TimelineComponent timeline, Entity timelineEntity, ref EntityCommandBuffer ecb)
        {
            if (timeline.CurrentTime < timeline.DurationWithLoopDelay) return;

            if (timeline.LoopCount == 0)
            {
                ecb.AddComponent<TimelineDestroyTag>(timelineEntity);
                return;
            }

            if (timeline.LoopCount > 0)
            {
                --timeline.LoopCount;
            }

            timeline.CurrentTime = 0f;
        }

        [BurstCompile]
        private void TryToStartPlayingTimelineElement(in ComponentType componentType, ref ITimelineElement timelineElement, ref TimelineComponent timeline, ref EntityCommandBuffer ecb)
        {
            var pastStartTime = timelineElement.GetStartTime() <= timeline.CurrentTime;
            var alreadyPlaying = timeline.IsTimelineElementActive(timelineElement.GetId());
            var hasAlreadyPlayed = timelineElement.GetEndTime() <= timeline.CurrentTime;

            if (!pastStartTime || alreadyPlaying || hasAlreadyPlayed) return;
            if (TimelineSystemCommandTypeHelper.AlreadyHasInfoComponent(timelineElement.GetTargetEntity(), componentType, EntityManager)) return;

            timeline.AddTimelineElementIdToActive(timelineElement.GetId());
            TimelineSystemCommandTypeHelper.Add(ref ecb, componentType, ref timelineElement);
        }

        [BurstCompile]
        private void TryToRemoveTimelineElementIdFromActiveList(ref ITimelineElement timelineElement, ref TimelineComponent timeline)
        {
            if (timeline.CurrentTime >= timelineElement.GetEndTime())
            {
                timeline.RemoveTimelineElementIdFromActive(timelineElement.GetId());
            }
        }
    }
}