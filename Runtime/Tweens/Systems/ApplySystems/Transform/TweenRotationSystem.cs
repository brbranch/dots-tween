using DotsTween.Tweens;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace DotsTween.Tweens
{
    [BurstCompile]
    [UpdateInGroup(typeof(TweenApplySystemGroup))]
    internal partial class TweenRotationSystem : SystemBase
    {
        [BurstCompile]
        protected override void OnCreate()
        {
            RequireForUpdate<TweenRotation>();
        }
        
        [BurstCompile]
        protected override void OnUpdate()
        {
            Entities.ForEach((ref LocalTransform localTransform, in DynamicBuffer<TweenState> tweenBuffer, in TweenRotation tweenInfo) =>
            {
                foreach (var tween in tweenBuffer)
                {
                    if (tween.Id == tweenInfo.Id && !tween.IsPaused)
                    {
                        localTransform.Rotation = math.slerp(tweenInfo.Start, tweenInfo.End, tween.EasePercentage);
                        break;
                    }
                }
            }).ScheduleParallel();
        }
    }
}