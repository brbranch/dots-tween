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
        protected override void OnUpdate()
        {
            Entities
                .WithNone<TweenPause>()
                .ForEach((ref LocalTransform localTransform, in DynamicBuffer<TweenState> tweenBuffer, in TweenRotation tweenInfo) =>
                {
                    foreach (var tween in tweenBuffer)
                    {
                        if (tween.Id == tweenInfo.Id)
                        {
                            localTransform.Rotation = math.slerp(tweenInfo.Start, tweenInfo.End, tween.EasePercentage);
                            break;
                        }
                    }
                }).ScheduleParallel();
        }
    }
}