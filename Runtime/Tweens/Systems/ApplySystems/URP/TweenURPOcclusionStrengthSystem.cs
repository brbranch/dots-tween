#if DOTS_TWEEN_URP
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;

namespace DotsTween.Tweens
{
    [BurstCompile]
    [UpdateInGroup(typeof(TweenApplySystemGroup))]
    internal partial class TweenURPOcclusionStrengthSystem : SystemBase
    {
        [BurstCompile]
        protected override void OnCreate()
        {
            RequireForUpdate<TweenURPOcclusionStrength>();
        }

        [BurstCompile]
        protected override void OnUpdate()
        {
            Entities
                .WithNone<TweenPause>()
                .ForEach((ref URPMaterialPropertyOcclusionStrength strength, in DynamicBuffer<TweenState> tweenBuffer, in TweenURPOcclusionStrength tweenInfo) =>
                {
                    foreach (var tween in tweenBuffer)
                    {
                        if (tween.Id != tweenInfo.Id) continue;
                        strength.Value = math.lerp(tweenInfo.Start, tweenInfo.End, tween.EasePercentage);
                    }
                }).ScheduleParallel();
        }
    }
}
#endif