using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;

namespace DotsTween.Tweens
{
    [BurstCompile]
    internal struct TweenRotationCommand : IComponentData, ITweenParams, ITweenInfo<quaternion>
    {
        public TweenParams TweenParams;
        public quaternion Start;
        public quaternion End;

        public TweenRotationCommand(in TweenParams tweenParams, in quaternion start, in quaternion end)
        {
            TweenParams = tweenParams;
            Start = start;
            End = end;
        }

        [BurstCompile]
        public void SetTweenInfo(in quaternion start, in quaternion end)
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
        public quaternion GetTweenStart()
        {
            return Start;
        }

        [BurstCompile]
        public quaternion GetTweenEnd()
        {
            return End;
        }
    }
}