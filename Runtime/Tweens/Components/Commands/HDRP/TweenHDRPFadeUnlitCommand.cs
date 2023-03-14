#if DOTS_TWEEN_HDRP
using Unity.Burst;
using Unity.Entities;

namespace DotsTween.Tweens
{
    [BurstCompile]
    public struct TweenHDRPFadeUnlitCommand : IComponentData, ITweenParams, ITweenInfo<float>
    {
        public TweenParams TweenParams;
        public float Start;
        public float End;

        public TweenHDRPFadeUnlitCommand(in float start, in float end, in float duration, TweenParams tweenParams = default)
        {
            tweenParams.Duration = duration;
            TweenParams = tweenParams;
            Start = start;
            End = end;
            tweenParams.GenerateId(TypeManager.GetTypeIndex<TweenHDRPFadeUnlit>().Value);
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