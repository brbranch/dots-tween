using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace DotsTween.Tweens
{
    [BurstCompile]
    internal struct TweenNonUniformScaleCommand : ITweenCommand, ITweenInfo<float3>
    {
        public TweenParams TweenParams;
        public float3 Start;
        public float3 End;

        public TweenNonUniformScaleCommand(in TweenParams tweenParams, in float3 start, in float3 end)
        {
            TweenParams = tweenParams;
            Start = start;
            End = end;
        }

        [BurstCompile]
        public void SetTweenInfo(in float3 start, in float3 end)
        {
            Start = start;
            End = end;
        }

        [BurstCompile]
        public void SetTweenParams(in TweenParams tweenParams)
        {
            TweenParams = tweenParams;
        }

        [BurstCompile]
        public TweenParams GetTweenParams()
        {
            return TweenParams;
        }

        [BurstCompile]
        public float3 GetTweenStart()
        {
            return Start;
        }

        [BurstCompile]
        public float3 GetTweenEnd()
        {
            return End;
        }
    }
}