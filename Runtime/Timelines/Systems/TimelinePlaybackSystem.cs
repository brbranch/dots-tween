using DotsTween.Tweens;
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
            
            foreach (var (timeline, timelineEntity) in SystemAPI.Query<RefRW<TimelineComponent>>().WithNone<TimelinePausedTag>().WithEntityAccess())
            {
                timeline.ValueRW.CurrentTime += deltaTime;
                
                // Perform Component operations after the initial Timeline Delay (if any)
                if (timeline.ValueRO is { IsPlaying: false, CurrentTime: >= 0f })
                {
                    timeline.ValueRW.IsPlaying = true;
                    PerformComponentOperations(ref ecb, ref timeline.ValueRW.OnStart);
                }

                var bufferReader = timeline.ValueRW.TimelineElements.AsReader();
                var index = 0;
                
                while (!bufferReader.EndOfBuffer && index < timeline.ValueRO.TimelineElementTypes.Length)
                {
                    var timelineElement = TimelineSystemCommandTypeHelper.DereferenceNextTimelineElement(timeline.ValueRO.TimelineElementTypes[index], ref bufferReader);
                    TryToStartPlayingTimelineElement(ref timelineElement, ref timeline.ValueRW, ref ecb);
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
                PerformComponentOperations(ref ecb, ref timeline.OnComplete);
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
        private void TryToStartPlayingTimelineElement(ref ITimelineElement timelineElement, ref TimelineComponent timeline, ref EntityCommandBuffer ecb)
        {
            if (!(timelineElement.GetStartTime() <= timeline.CurrentTime) || timeline.ActiveElementIds.Contains(timelineElement.GetId())) return;
            
            timeline.ActiveElementIds.Add(timelineElement.GetId());
            var command = timelineElement.GetCommand();
            ecb.AddComponent(timelineElement.GetTargetEntity(), command);
        }
        
        [BurstCompile]
        private void TryToRemoveTimelineElementIdFromActiveList(ref ITimelineElement timelineElement, ref TimelineComponent timeline)
        {
            if (timeline.CurrentTime >= timelineElement.GetEndTime())
            {
                timeline.ActiveElementIds.RemoveAt(timeline.ActiveElementIds.IndexOf(timelineElement.GetId()));
            }
        }
    }
}