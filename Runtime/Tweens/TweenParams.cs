using System.Runtime.InteropServices;
using DotsTween.Math;
using Unity.Collections;
using Unity.Entities;

namespace DotsTween.Tweens
{
    public struct TweenParams
    {
        public float Duration;
        public EaseType EaseType;
        [MarshalAs(UnmanagedType.U1)] public bool IsPingPong;
        public short LoopCount;
        public float StartDelay;

        public float TimelineStartPosition { get; private set; }
        public float TimelineEndPosition { get; private set; }
        
        public ComponentType AddOnComplete;
        public ComponentType RemoveOnComplete;
        public ComponentType EnableOnComplete;
        public ComponentType DisableOnComplete;
        
        public ComponentType AddOnStart;
        public ComponentType RemoveOnStart;
        public ComponentType EnableOnStart;
        public ComponentType DisableOnStart;

        public TweenParams(
            in float duration,
            in EaseType easeType = EaseType.Linear) : this()
        {
            Duration = duration;
            EaseType = easeType;
            IsPingPong = false;
            LoopCount = 0;
            StartDelay = 0f;
            
            TimelineStartPosition = 0f;
            TimelineEndPosition = 0f;
        }

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
    }
}