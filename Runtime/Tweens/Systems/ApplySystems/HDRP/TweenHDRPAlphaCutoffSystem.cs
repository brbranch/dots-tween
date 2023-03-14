#if DOTS_TWEEN_HDRP
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;

namespace DotsTween.Tweens
{
    [BurstCompile]
    [UpdateInGroup(typeof(TweenApplySystemGroup))]
    internal partial class TweenHDRPAlphaCutoffSystem : SystemBase
    {
        [BurstCompile]
        protected override void OnCreate()
        {
            RequireForUpdate<TweenHDRPAlphaCutoff>();
        }
        
        [BurstCompile]
        protected override void OnUpdate()
        {
            Entities.ForEach((ref HDRPMaterialPropertyAlphaCutoff cutoff, in DynamicBuffer<TweenState> tweenBuffer, in TweenHDRPAlphaCutoff tweenInfo) =>
            {
                foreach (var tween in tweenBuffer)
                {
                    if (tween.Id != tweenInfo.Id || tween.IsPaused) continue;
                    cutoff.Value = math.lerp(tweenInfo.Start, tweenInfo.End, tween.EasePercentage);
                }
            }).ScheduleParallel();
        }
    }
}
#endif