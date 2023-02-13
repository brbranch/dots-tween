using DotsTween.Math;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;

namespace DotsTween.Tweens
{
    [BurstCompile]
    public struct TweenState : IBufferElementData, ITweenId
    {
        internal const short LOOP_COUNT_INFINITE = -1;

        public TweenParams Settings;

        public EaseType EaseType => Settings.EaseType;
        public float Duration => Settings.Duration;
        public bool IsPingPong => Settings.IsPingPong;
        
        public int Id;
        public float CurrentTime;
        public float EasePercentage;
        public short PlayCount;
        public bool IsReverting;

        // Cleanup and Sequencing with Timelines
        // public bool AutoKill;
        public bool IsComplete => CurrentTime >= Duration;

        internal TweenState(in TweenParams tweenParams, in double elapsedTime, in int chunkIndex, in int tweenInfoTypeIndex) : this()
        {
            Settings = tweenParams;
            PlayCount = tweenParams.LoopCount >= 0 ? (short)(tweenParams.LoopCount + 1) : LOOP_COUNT_INFINITE;

            CurrentTime = -math.max(tweenParams.StartDelay, 0.0f);
            Id = GenerateId(elapsedTime, chunkIndex, tweenInfoTypeIndex);
        }

        [BurstCompile]
        public float GetNormalizedTime()
        {
            float oneWayDuration = Duration;
            if (IsPingPong)
            {
                oneWayDuration /= 2.0f;
            }

            return math.clamp(CurrentTime / oneWayDuration, 0.0f, 1.0f);
        }

        [BurstCompile]
        public void SetTweenId(in int id)
        {
            Id = id;
        }

        [BurstCompile]
        public int GetTweenId()
        {
            return Id;
        }

        [BurstCompile]
        private int GenerateId(in double elapsedTime, in int entityInQueryIndex, in int tweenInfoTypeIndex)
        {
            unchecked
            {
                int hashCode = (int) EaseType;
                hashCode = (hashCode * 397) ^ ((byte)EaseType).GetHashCode();
                hashCode = (hashCode * 397) ^ Duration.GetHashCode();
                hashCode = (hashCode * 397) ^ IsPingPong.GetHashCode();
                hashCode = (hashCode * 397) ^ PlayCount.GetHashCode();
                hashCode = (hashCode * 397) ^ elapsedTime.GetHashCode();
                hashCode = (hashCode * 397) ^ entityInQueryIndex;
                hashCode = (hashCode * 397) ^ tweenInfoTypeIndex;

                return hashCode;
            }
        }
    }
}