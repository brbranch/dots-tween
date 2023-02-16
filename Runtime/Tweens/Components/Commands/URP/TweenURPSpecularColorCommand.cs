#if DOTS_TWEEN_URP
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;

namespace DotsTween.Tweens
{
    [BurstCompile]
    public struct TweenURPSpecularColorCommand : IComponentData, ITweenParams, ITweenInfo<float4>
    {
        public TweenParams TweenParams;
        public float4 Start;
        public float4 End;

        public TweenURPSpecularColorCommand(in float4 start, in float4 end, in float duration, TweenParams tweenParams = default)
        {
            tweenParams.Duration = duration;
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
    }
}
#endif