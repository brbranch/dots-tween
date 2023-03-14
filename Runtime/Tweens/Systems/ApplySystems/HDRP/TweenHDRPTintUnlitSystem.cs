#if DOTS_TWEEN_HDRP
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;

namespace DotsTween.Tweens
{
    [BurstCompile]
    [UpdateInGroup(typeof(TweenApplySystemGroup))]
    internal partial class TweenHDRPTintUnlitSystem : SystemBase
    {
        [BurstCompile]
        protected override void OnCreate()
        {
            RequireForUpdate<TweenHDRPTintUnlit>();
        }

        [BurstCompile]
        protected override void OnUpdate()
        {
            Entities.ForEach((ref HDRPMaterialPropertyUnlitColor colour, in DynamicBuffer<TweenState> tweenBuffer, in TweenHDRPTintUnlit tweenInfo) =>
            {
                foreach (var tween in tweenBuffer)
                {
                    if (tween.Id != tweenInfo.Id || tween.IsPaused) continue;
                    colour.Value = math.lerp(tweenInfo.Start, tweenInfo.End, tween.EasePercentage);
                }
            }).ScheduleParallel();
        }
    }
}
#endif