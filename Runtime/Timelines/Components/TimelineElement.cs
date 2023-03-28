using DotsTween.Tweens;
using Unity.Burst;
using Unity.Entities;

namespace DotsTween.Timelines
{
    [BurstCompile]
    internal readonly struct TimelineElement<T> : ITimelineElement where T : unmanaged, IComponentData, ITweenParams
    {
        public uint Id { get; }
        public Entity Target { get; }
        public float StartTime { get; }
        public float EndTime { get; }
        public T Command { get; }
        
        [BurstCompile] public uint GetId() => Id;
        [BurstCompile] public float GetStartTime() => StartTime;
        [BurstCompile] public float GetEndTime() => EndTime;
        [BurstCompile] public uint GetTweenId() => Command.GetTweenParams().Id;
        public Entity GetTargetEntity() => Target;
        public IComponentData GetCommand() => Command;
        
        public TimelineElement(in Entity target, in float startTime, in float endTime, in T command)
        {
            Target = target;
            StartTime = startTime;
            EndTime = endTime;
            Command = command;
            Id = GenerateId(target, startTime, endTime, command);
        }

        [BurstCompile]
        private static uint GenerateId(in Entity target, in float startTime, in float endTime, in T command)
        {
            unchecked
            {
                var tweenParams = command.GetTweenParams();
                int hashCode = target.GetHashCode();
                hashCode = (hashCode * 1091) ^ startTime.GetHashCode();
                hashCode = (hashCode * 1091) ^ endTime.GetHashCode();
                hashCode = (hashCode * 1091) ^ tweenParams.Duration.GetHashCode();
                hashCode = (hashCode * 1091) ^ ((byte)tweenParams.EaseType).GetHashCode();
                hashCode = (hashCode * 1091) ^ tweenParams.IsPingPong.GetHashCode();
                hashCode = (hashCode * 1091) ^ tweenParams.LoopCount.GetHashCode();
                hashCode = (hashCode * 1091) ^ tweenParams.StartDelay.GetHashCode();
                hashCode = (hashCode * 1091) ^ tweenParams.TimelineStartPosition.GetHashCode();
                hashCode = (hashCode * 1091) ^ tweenParams.TimelineEndPosition.GetHashCode();
                hashCode = (hashCode * 1091) ^ tweenParams.Id.GetHashCode();
                return (uint)hashCode;
            }
        }
    }
}