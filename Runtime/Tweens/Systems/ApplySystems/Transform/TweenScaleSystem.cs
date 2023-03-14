using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace DotsTween.Tweens
{
    [BurstCompile]
    [UpdateInGroup(typeof(TweenApplySystemGroup))]
    internal partial class TweenScaleSystem : SystemBase
    {
        [BurstCompile]
        protected override void OnCreate()
        {
            RequireForUpdate<TweenScale>();
        }

        [BurstCompile]
        protected override void OnUpdate()
        {
            Entities.ForEach((ref LocalTransform localTransform, in DynamicBuffer<TweenState> tweenBuffer, in TweenScale tweenInfo) =>
            {
                foreach (var tween in tweenBuffer)
                {
                    if (tween.Id == tweenInfo.Id && !tween.IsPaused)
                    {
                        localTransform.Scale = math.lerp(tweenInfo.Start, tweenInfo.End, tween.EasePercentage);
                        break;
                    }
                }
            }).ScheduleParallel();
        }
    }
}