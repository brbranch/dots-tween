﻿#if DOTS_TWEEN_URP
using Unity.Burst;
using Unity.Entities;

namespace DotsTween.Tweens
{
    [BurstCompile]
    public struct TweenURPOcclusionStrengthCommand : IComponentData, ITweenParams, ITweenInfo<float>
    {
        public TweenParams TweenParams;
        public float Start;
        public float End;

        public TweenURPOcclusionStrengthCommand(in Entity entity, in float start, in float end, in float duration, TweenParams tweenParams = default)
        {
            tweenParams.Duration = duration;
            tweenParams.Id = tweenParams.GenerateId(entity.GetHashCode(), start.GetHashCode(), end.GetHashCode(), TypeManager.GetTypeIndex<TweenURPOcclusionStrength>().Value);
            TweenParams = tweenParams;
            Start = start;
            End = end;
        }

        public void SetTweenInfo(in float start, in float end)
        {
            Start = start;
            End = end;
        }

        public void SetTweenParams(in TweenParams tweenParams)
        {
            TweenParams = tweenParams;
        }

        public TweenParams GetTweenParams()
        {
            return TweenParams;
        }

        public float GetTweenStart()
        {
            return Start;
        }

        public float GetTweenEnd()
        {
            return End;
        }
        
        [BurstCompile] public void Cleanup() { }
    }
}
#endif