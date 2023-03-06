using Unity.Burst;
using Unity.Collections;
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
                var bufferReader = timelineRef.ValueRO.TimelineElements.AsReader();
                var index = 0;
                
                while (!bufferReader.EndOfBuffer && index < timelineRef.ValueRO.TimelineElementTypes.Length)
                {
                    var timelineElement = TimelineSystemCommandTypeHelper.DereferenceNextTimelineElement(timelineRef.ValueRO.TimelineElementTypes[index], ref bufferReader);
                    if (timelineRef.ValueRO.ActiveElementIds.Contains(timelineElement.GetId()))
                    {
                        Tween.Controls.Stop(ref ecb, timelineElement.GetTargetEntity());
                    }
                    ++index;
                }

                timelineRef.ValueRW.Dispose();
                ecb.DestroyEntity(entity);
            }
        }
    }
}