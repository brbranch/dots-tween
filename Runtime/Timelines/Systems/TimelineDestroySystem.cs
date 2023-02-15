using Unity.Entities;

namespace DotsTween.Timelines.Systems
{
    [UpdateInGroup(typeof(TimelineSimulationSystemGroup), OrderLast = true)]
    public partial class TimelineDestroySystem : SystemBase
    {
        protected override void OnCreate()
        {
            RequireForUpdate<TimelineComponent>();
            RequireForUpdate<TimelineDestroyTag>();
        }

        protected override void OnUpdate()
        {
            var ecb = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(CheckedStateRef.WorldUnmanaged);
            
            foreach (var (timeline, destroyTag, entity) in SystemAPI.Query<TimelineComponent, TimelineDestroyTag>().WithEntityAccess())
            {
                foreach (var timelineElement in timeline.ActiveElements)
                {
                    Tween.Controls.Stop(ref ecb, timelineElement.Target);
                }
                
                ecb.DestroyEntity(entity);
            }
        }
    }
}