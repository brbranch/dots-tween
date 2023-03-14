#if DOTS_TWEEN_HDRP
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;

namespace DotsTween.Tweens
{
    [BurstCompile]
    [UpdateInGroup(typeof(TweenApplySystemGroup))]
    internal partial class TweenHDRPEmissiveColorSystem : SystemBase
    {
        [BurstCompile]
        protected override void OnCreate()
        {
            RequireForUpdate<TweenHDRPEmissiveColor>();
        }

        [BurstCompile]
        protected override void OnUpdate()
        {
            Entities.ForEach((ref HDRPMaterialPropertyEmissiveColor emissiveColor, in DynamicBuffer<TweenState> tweenBuffer, in TweenHDRPEmissiveColor tweenInfo) =>
            {
                foreach (var tween in tweenBuffer)
                {
                    if (tween.Id != tweenInfo.Id || tween.IsPaused) continue;
                    emissiveColor.Value = math.lerp(tweenInfo.Start, tweenInfo.End, tween.EasePercentage);
                }
            }).ScheduleParallel();
        }
    }
}
#endif