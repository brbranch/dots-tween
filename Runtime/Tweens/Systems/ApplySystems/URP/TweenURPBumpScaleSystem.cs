#if DOTS_TWEEN_URP
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;

namespace DotsTween.Tweens
{
    [BurstCompile]
    [UpdateInGroup(typeof(TweenApplySystemGroup))]
    internal partial class TweenURPBumpScaleSystem : SystemBase
    {
        [BurstCompile]
        protected override void OnCreate()
        {
            RequireForUpdate<TweenURPBumpScale>();
        }

        [BurstCompile]
        protected override void OnUpdate()
        {
            Entities
                .WithNone<TweenPause>()
                .ForEach((ref URPMaterialPropertyBumpScale bumpScale, in DynamicBuffer<TweenState> tweenBuffer, in TweenURPBumpScale tweenInfo) =>
                {
                    foreach (var tween in tweenBuffer)
                    {
                        if (tween.Id != tweenInfo.Id) continue;
                        bumpScale.Value = math.lerp(tweenInfo.Start, tweenInfo.End, tween.EasePercentage);
                    }
                }).ScheduleParallel();
        }
    }
}
#endif