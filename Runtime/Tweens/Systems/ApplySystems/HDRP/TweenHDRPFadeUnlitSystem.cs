#if DOTS_TWEEN_HDRP
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;

namespace DotsTween.Tweens
{
    [BurstCompile]
    [UpdateInGroup(typeof(TweenApplySystemGroup))]
    internal partial class TweenHDRPFadeUnlitSystem : SystemBase
    {
        [BurstCompile]
        protected override void OnCreate()
        {
            RequireForUpdate<TweenHDRPFadeUnlit>();
        }
        
        [BurstCompile]
        protected override void OnUpdate()
        {
            Entities.ForEach((ref HDRPMaterialPropertyUnlitColor colour, in DynamicBuffer<TweenState> tweenBuffer, in TweenHDRPFadeUnlit tweenInfo) =>
            {
                foreach (var tween in tweenBuffer)
                {
                    if (tween.Id != tweenInfo.Id || tween.IsPaused) continue;
                    var alpha = math.lerp(tweenInfo.Start, tweenInfo.End, tween.EasePercentage);
                    colour.Value = new float4(colour.Value.xyz, alpha);
                }
            }).ScheduleParallel();
        }
    }
}
#endif