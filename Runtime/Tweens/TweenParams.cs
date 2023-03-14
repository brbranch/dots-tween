using System;
using System.Runtime.InteropServices;
using DotsTween.Math;
using Unity.Burst;

namespace DotsTween.Tweens
{
    public struct TweenParams
    {
        public float Duration { get; internal set; }
        public int Id { get; private set; }
        
        public EaseType EaseType;
        [MarshalAs(UnmanagedType.U1)] public bool IsPingPong;
        public short LoopCount;
        public float StartDelay;

        internal float TimelineStartPosition;
        internal float TimelineEndPosition;

        public ComponentOperations OnComplete;
        public ComponentOperations OnStart;

        [BurstCompile]
        public void SetTimelinePositions(float startTime, float endTime)
        {
            TimelineStartPosition = startTime;
            TimelineEndPosition = endTime;
        }

        public override string ToString()
        {
            string msg = $"{Duration} secs";

            if (EaseType != EaseType.Linear)
            {
                msg += $", {EaseType}";
            }

            if (IsPingPong)
            {
                msg += ", Ping Pong";
            }

            if (LoopCount != 0)
            {
                msg += LoopCount == -1 ? ", Infinite" : $", {LoopCount} times";
            }

            if (StartDelay > 0.0f)
            {
                msg += $", Delayed {StartDelay} secs";
            }

            return msg;
        }
        
        [BurstCompile]
        internal void GenerateId(in int tweenInfoTypeIndex)
        {
            unchecked
            {
                int hashCode = (int) EaseType;
                hashCode = (hashCode * 397) ^ ((byte)EaseType).GetHashCode();
                hashCode = (hashCode * 397) ^ Duration.GetHashCode();
                hashCode = (hashCode * 397) ^ IsPingPong.GetHashCode();
                hashCode = (hashCode * 397) ^ StartDelay.GetHashCode();
                hashCode = (hashCode * 397) ^ OnComplete.GetHashCode();
                hashCode = (hashCode * 397) ^ OnStart.GetHashCode();
                hashCode = (hashCode * 397) ^ Guid.NewGuid().GetHashCode();
                hashCode = (hashCode * 397) ^ tweenInfoTypeIndex;

                Id = hashCode;
            }
        }
    }
}