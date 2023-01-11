using DotsTween.Tweens;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace DotsTween
{
    [UpdateInGroup(typeof(TweenApplySystemGroup))]
    internal partial class TweenScaleSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            Entities
                .WithNone<TweenPause>()
                .ForEach((ref LocalTransform localTransform, in DynamicBuffer<TweenState> tweenBuffer, in TweenScale tweenInfo) =>
                {
                    foreach (var tween in tweenBuffer)
                    {
                        if (tween.Id == tweenInfo.Id)
                        {
                            localTransform.Scale = math.lerp(tweenInfo.Start, tweenInfo.End, tween.EasePercentage);
                            break;
                        }
                    }
                }).ScheduleParallel();
        }
    }
}