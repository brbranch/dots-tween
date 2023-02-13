using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;

namespace DotsTween.Tweens
{
    internal struct TweenURPTintCommand : ITweenCommand, ITweenInfo<float4>
    {
        public TweenParams TweenParams;
        public float4 Start;
        public float4 End;

        public TweenURPTintCommand(in TweenParams tweenParams, in float4 start, in float4 end)
        {
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