﻿using DotsTween.Math;
using Unity.Entities;
using Unity.Mathematics;

namespace DotsTween.Tweens
{
    public struct TweenState : IBufferElementData, ITweenId
    {
        internal const byte LOOP_COUNT_INFINITE = 0;

        public int Id;
        public EaseType EaseType;
        public float Duration;
        public float CurrentTime;
        public float EasePercentage;
        public bool IsPingPong;
        public byte LoopCount;
        public bool IsReverting;

        internal TweenState(
            in EaseType easeType,
            in float duration, 
            in bool isPingPong, 
            in byte loopCount, 
            in float delayedStartTime, 
            in double elapsedTime, 
            in int chunkIndex, 
            in int tweenInfoTypeIndex) : this()
        {
            EaseType = easeType;
            Duration = duration;
            IsPingPong = isPingPong;
            LoopCount = loopCount;

            CurrentTime = -math.max(delayedStartTime, 0.0f);
            Id = GenerateId(elapsedTime, chunkIndex, tweenInfoTypeIndex);
        }

        internal TweenState(in TweenParams tweenParams, in double elapsedTime, in int chunkIndex, in int tweenInfoTypeIndex)
            : this(tweenParams.EaseType, tweenParams.Duration, tweenParams.IsPingPong, tweenParams.LoopCount, tweenParams.StartDelay, elapsedTime, chunkIndex, tweenInfoTypeIndex)
        {
        }

        public float GetNormalizedTime()
        {
            float oneWayDuration = Duration;
            if (IsPingPong)
            {
                oneWayDuration /= 2.0f;
            }

            return math.clamp(CurrentTime / oneWayDuration, 0.0f, 1.0f);
        }

        public void SetTweenId(in int id)
        {
            Id = id;
        }

        public int GetTweenId()
        {
            return Id;
        }

        private int GenerateId(in double elapsedTime, in int entityInQueryIndex, in int tweenInfoTypeIndex)
        {
            unchecked
            {
                int hashCode = (int) EaseType;
                hashCode = (hashCode * 397) ^ ((byte)EaseType).GetHashCode();
                hashCode = (hashCode * 397) ^ Duration.GetHashCode();
                hashCode = (hashCode * 397) ^ IsPingPong.GetHashCode();
                hashCode = (hashCode * 397) ^ LoopCount.GetHashCode();
                hashCode = (hashCode * 397) ^ elapsedTime.GetHashCode();
                hashCode = (hashCode * 397) ^ entityInQueryIndex;
                hashCode = (hashCode * 397) ^ tweenInfoTypeIndex;

                return hashCode;
            }
        }
    }
}