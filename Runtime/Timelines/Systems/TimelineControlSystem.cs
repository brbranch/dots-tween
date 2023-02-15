using Unity.Collections;
using Unity.Entities;

namespace DotsTween.Timelines.Systems
{
    [UpdateInGroup(typeof(TimelineSimulationSystemGroup), OrderFirst = true)]
    [RequireMatchingQueriesForUpdate]
    internal partial class TimelineControlSystem : SystemBase
    {
        private EntityQuery timelineQuery;
        
        protected override void OnCreate()
        {
            RequireForUpdate<TimelineComponent>();
            RequireForUpdate<TimelineControlCommand>();
            
            timelineQuery = SystemAPI.QueryBuilder().WithAll<TimelineComponent>().Build();
        }

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
                            PauseTimeline(ref ecb, timelineEntity, timeline);
                            break;
                        
                        case TimelineControlCommands.Resume:
                            ResumeTimeline(ref ecb, timelineEntity, timeline);
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

        private void PauseTimeline(ref EntityCommandBuffer ecb, Entity timelineEntity, TimelineComponent timelineComponent)
        {
            ecb.AddComponent<TimelinePausedTag>(timelineEntity);

            foreach (var timelineElement in timelineComponent.ActiveElements)
            {
                Tween.Controls.Pause(ref ecb, timelineElement.Target);
            }
        }

        private void ResumeTimeline(ref EntityCommandBuffer ecb, Entity timelineEntity, TimelineComponent timelineComponent)
        {
            ecb.RemoveComponent<TimelinePausedTag>(timelineEntity);

            foreach (var timelineElement in timelineComponent.ActiveElements)
            {
                Tween.Controls.Resume(ref ecb, timelineElement.Target);
            }
        }

        private void StopTimeline(ref EntityCommandBuffer ecb, Entity timelineEntity)
        {
            ecb.AddComponent<TimelineDestroyTag>(timelineEntity);
        }
    }
}