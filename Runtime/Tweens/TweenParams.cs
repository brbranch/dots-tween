using System.Runtime.InteropServices;
using DotsTween.Math;

namespace DotsTween.Tweens
{
    public struct TweenParams
    {
        public float Duration { get; internal set; }
        public EaseType EaseType;
        [MarshalAs(UnmanagedType.U1)] public bool IsPingPong;
        public short LoopCount;
        public float StartDelay;

        public float TimelineStartPosition { get; private set; }
        public float TimelineEndPosition { get; private set; }

        public ComponentOperations OnComplete;
        public ComponentOperations OnStart;

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