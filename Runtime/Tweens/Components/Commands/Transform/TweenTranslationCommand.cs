using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;

namespace DotsTween.Tweens
{
    [BurstCompile]
    public struct TweenTranslationCommand : IComponentData, ITweenParams, ITweenInfo<float3>
    {
        public TweenParams TweenParams { get; private set; }
        public float3 Start { get; private set; }
        public float3 End { get; private set; }

        public TweenTranslationCommand(in Entity entity, in float3 start, in float3 end, in float duration, TweenParams tweenParams = default)
        {
            tweenParams.Duration = duration;
            tweenParams.Id = tweenParams.GenerateId(entity.GetHashCode(), start.GetHashCode(), end.GetHashCode(), TypeManager.GetTypeIndex<TweenTranslation>().Value);
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

        [BurstCompile] public void Cleanup() { }
    }
}