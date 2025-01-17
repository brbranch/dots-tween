﻿using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;

namespace DotsTween.Tweens
{
    [BurstCompile]
    public partial struct TweenRotationCommand : IComponentData, ITweenParams, ITweenInfo<quaternion>
    {
        public TweenParams TweenParams;
        public quaternion Start;
        public quaternion End;

        public TweenRotationCommand(in Entity entity, in quaternion start, in quaternion end, in float duration, TweenParams tweenParams = default)
        {
            tweenParams.Duration = duration;
            tweenParams.Id = tweenParams.GenerateId(entity.GetHashCode(), start.GetHashCode(), end.GetHashCode(), TypeManager.GetTypeIndex<TweenRotation>().Value);
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

        [BurstCompile] public void Cleanup() { }
    }
}