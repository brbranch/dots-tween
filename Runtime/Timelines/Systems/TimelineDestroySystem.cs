using Unity.Burst;
using Unity.Entities;

namespace DotsTween.Timelines.Systems
{
    [UpdateInGroup(typeof(TimelineSimulationSystemGroup), OrderLast = true)]
    [BurstCompile]
    public partial class TimelineDestroySystem : SystemBase
    {
        [BurstCompile]
        protected override void OnCreate()
        {
            RequireForUpdate<TimelineComponent>();
            RequireForUpdate<TimelineDestroyTag>();
        }

        [BurstCompile]
        protected override void OnUpdate()
        {
            var ecb = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(CheckedStateRef.WorldUnmanaged);
            
            foreach (var (timelineRef, destroyTag, entity) in SystemAPI.Query<RefRW<TimelineComponent>, TimelineDestroyTag>().WithEntityAccess())
            {
                var bufferReader = timelineRef.ValueRW.GetTimelineReader();
                var index = 0;
                
                while (!bufferReader.EndOfBuffer && index < timelineRef.ValueRO.Size)
                {
                    var timelineElement = TimelineSystemCommandTypeHelper.DereferenceNextTimelineElement(timelineRef.ValueRO.GetTimelineElementType(index), ref bufferReader);
                    if (timelineRef.ValueRO.IsTimelineElementActive(timelineElement.GetId()))
                    {
                        Tween.Controls.StopAll(ref ecb, timelineElement.GetTargetEntity());
                    }
                    ++index;
                }

                timelineRef.ValueRW.Dispose();
                ecb.DestroyEntity(entity);
            }
        }
    }
}