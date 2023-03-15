using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace DotsTween.Tweens
{
    [BurstCompile]
    [UpdateInGroup(typeof(TweenApplySystemGroup))]
    internal partial class TweenTranslationSystem : SystemBase
    {
        [BurstCompile]
        protected override void OnCreate()
        {
            RequireForUpdate<TweenTranslation>();
        }

        [BurstCompile]
        protected override void OnUpdate()
        {
            Entities.ForEach((ref LocalTransform localTransform, in DynamicBuffer<TweenState> tweenBuffer, in TweenTranslation tweenInfo) =>
            {
                foreach (var tween in tweenBuffer)
                {
                    if (tween.Id == tweenInfo.Id && !tween.IsPaused)
                    {
                        localTransform.Position = math.lerp(tweenInfo.Start, tweenInfo.End, tween.EasePercentage);
                        break;
                    }
                }
            }).ScheduleParallel();
        }
    }
}