﻿#if DOTS_TWEEN_URP
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;

namespace DotsTween.Tweens
{
    [BurstCompile]
    public struct TweenURPTintCommand : IComponentData, ITweenParams, ITweenInfo<float4>
    {
        public TweenParams TweenParams;
        public float4 Start;
        public float4 End;

        public TweenURPTintCommand(in Entity entity, in float4 start, in float4 end, in float duration, TweenParams tweenParams = default)
        {
            tweenParams.Duration = duration;
            tweenParams.Id = tweenParams.GenerateId(entity.GetHashCode(), start.GetHashCode(), end.GetHashCode(), TypeManager.GetTypeIndex<TweenURPTint>().Value);
            TweenParams = tweenParams;
            Start = start;
            End = end;
        }

        public void SetTweenInfo(in float4 start, in float4 end)
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

        public float4 GetTweenStart()
        {
            return Start;
        }

        public float4 GetTweenEnd()
        {
            return End;
        }
        
        [BurstCompile] public void Cleanup() { }
    }
}
#endif