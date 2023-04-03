using System;
using System.Runtime.InteropServices;
using DotsTween.Math;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;

namespace DotsTween.Tweens
{
    [BurstCompile]
    public struct TweenState : IBufferElementData, ITweenId
    {
        public const short LOOP_COUNT_INFINITE = -1;

        public TweenParams Settings;

        public EaseType EaseType => Settings.EaseType;
        public float Duration => Settings.Duration;
        public bool IsPingPong => Settings.IsPingPong;
        
        public uint Id => Settings.Id;
        public float CurrentTime;
        public float EasePercentage;
        public short PlayCount;
        [MarshalAs(UnmanagedType.U1)]public bool IsReverting;
        [MarshalAs(UnmanagedType.U1)]public bool IsPaused;

        // Cleanup and Sequencing with Timelines
        // public bool AutoKill;
        public bool IsComplete => CurrentTime >= Duration;

        internal TweenState(in TweenParams tweenParams) : this()
        {
            Settings = tweenParams;
            PlayCount = tweenParams.LoopCount >= 0 ? tweenParams.LoopCount : LOOP_COUNT_INFINITE;

            CurrentTime = -math.max(tweenParams.StartDelay, 0.0f);
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
        public void SetTweenId(in uint id)
        {
            throw new NotImplementedException();
        }

        [BurstCompile]
        public uint GetTweenId()
        {
            return Id;
        }
    }
}