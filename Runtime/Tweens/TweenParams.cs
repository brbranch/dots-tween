using DotsTween.Math;

namespace DotsTween
{
    public struct TweenParams
    {
        public float Duration;
        public EaseType EaseType;
        public bool IsPingPong;
        public byte LoopCount;
        public float StartDelay;

        public TweenParams(
            in float duration, 
            in EaseType easeType = EaseType.Linear,
            in bool isPingPong = false, 
            in int loopCount = 1, 
            in float startDelay = 0.0f)
        {
            Duration = duration;
            EaseType = easeType;
            IsPingPong = isPingPong;
            LoopCount = (byte) loopCount;
            StartDelay = startDelay;
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

            if (LoopCount != 1)
            {
                msg += LoopCount == 0 ? ", Infinite" : $", {LoopCount} times";
            }

            if (StartDelay > 0.0f)
            {
                msg += $", Delayed {StartDelay} secs";
            }

            return msg;
        }
    }
}