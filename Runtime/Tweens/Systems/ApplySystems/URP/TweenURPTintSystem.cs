#if DOTS_TWEEN_URP
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;

namespace DotsTween.Tweens
{
    [BurstCompile]
    [UpdateInGroup(typeof(TweenApplySystemGroup))]
    internal partial class TweenURPTintSystem : SystemBase
    {
        [BurstCompile]
        protected override void OnCreate()
        {
            RequireForUpdate<TweenURPTint>();
        }

        [BurstCompile]
        protected override void OnUpdate()
        {
            Entities.ForEach((ref URPMaterialPropertyBaseColor baseColor, in DynamicBuffer<TweenState> tweenBuffer, in TweenURPTint tweenInfo) =>
            {
                foreach (var tween in tweenBuffer)
                {
                    if (tween.Id != tweenInfo.Id || tween.IsPaused) continue;
                    baseColor.Value = math.lerp(tweenInfo.Start, tweenInfo.End, tween.EasePercentage);
                }
            }).ScheduleParallel();
        }
    }
}
#endif