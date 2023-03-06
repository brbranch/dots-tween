using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

namespace DotsTween.Timelines.Systems
{
    [UpdateInGroup(typeof(TimelineSimulationSystemGroup), OrderFirst = true)]
    [RequireMatchingQueriesForUpdate]
    [BurstCompile]
    internal partial class TimelineControlSystem : SystemBase
    {
        private EntityQuery timelineQuery;
        
        [BurstCompile]
        protected override void OnCreate()
        {
            RequireForUpdate<TimelineComponent>();
            RequireForUpdate<TimelineControlCommand>();
            
            timelineQuery = SystemAPI.QueryBuilder().WithAll<TimelineComponent>().Build();
        }

        [BurstCompile]
        protected override void OnUpdate()
        {
            if (timelineQuery.IsEmpty) return;
            
            var timelineEntities = timelineQuery.ToEntityArray(Allocator.Temp);
            var ecb = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(CheckedStateRef.WorldUnmanaged);

            // TODO (@dyonng) probably needs optimization to reduce search complexity
            foreach (var (control, controlEntity) in SystemAPI.Query<TimelineControlCommand>().WithEntityAccess())
            {
                foreach (var timelineEntity in timelineEntities)
                {
                    var timeline = EntityManager.GetComponentData<TimelineComponent>(timelineEntity);
                    if (timeline.PlaybackId != control.TimelinePlaybackId) continue;
                    
                    switch (control.Command)
                    {
                        case TimelineControlCommands.Pause:
                            PauseTimeline(ref ecb, timelineEntity, ref timeline);
                            break;
                        
                        case TimelineControlCommands.Resume:
                            ResumeTimeline(ref ecb, timelineEntity, ref timeline);
                            break;
                            
                        case TimelineControlCommands.Stop:
                            StopTimeline(ref ecb, timelineEntity);
                            break;
                            
                        default: break;
                    }
                }
                
                ecb.DestroyEntity(controlEntity);
            }
        }

        [BurstCompile]
        private void PauseTimeline(ref EntityCommandBuffer ecb, Entity timelineEntity, ref TimelineComponent timeline)
        {
            ecb.AddComponent<TimelinePausedTag>(timelineEntity);

            var bufferReader = timeline.TimelineElements.AsReader();
            var index = 0;
                
            while (!bufferReader.EndOfBuffer && index < timeline.TimelineElementTypes.Length)
            {
                var timelineElement = TimelineSystemCommandTypeHelper.DereferenceNextTimelineElement(timeline.TimelineElementTypes[index], ref bufferReader);
                ++index;
                if (timeline.ActiveElementIds.Contains(timelineElement.GetId()))
                {
                    Tween.Controls.Pause(ref ecb, timelineElement.GetTargetEntity());
                }
            }
        }

        [BurstCompile]
        private void ResumeTimeline(ref EntityCommandBuffer ecb, Entity timelineEntity, ref TimelineComponent timeline)
        {
            ecb.RemoveComponent<TimelinePausedTag>(timelineEntity);

            var bufferReader = timeline.TimelineElements.AsReader();
            var index = 0;
                
            while (!bufferReader.EndOfBuffer && index < timeline.TimelineElementTypes.Length)
            {
                var timelineElement = TimelineSystemCommandTypeHelper.DereferenceNextTimelineElement(timeline.TimelineElementTypes[index], ref bufferReader);
                ++index;
                if (timeline.ActiveElementIds.Contains(timelineElement.GetId()))
                {
                    Tween.Controls.Resume(ref ecb, timelineElement.GetTargetEntity());
                }
            }
        }

        [BurstCompile]
        private void StopTimeline(ref EntityCommandBuffer ecb, Entity timelineEntity)
        {
            ecb.AddComponent<TimelineDestroyTag>(timelineEntity);
        }
    }
}