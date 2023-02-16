#if DOTS_TWEEN_HDRP
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;

namespace DotsTween.Tweens
{
    [BurstCompile]
    [UpdateInGroup(typeof(TweenApplySystemGroup))]
    internal partial class TweenHDRPFadeSystem : SystemBase
    {
        [BurstCompile]
        protected override void OnCreate()
        {
            RequireForUpdate<TweenHDRPFade>();
        }
        
        [BurstCompile]
        protected override void OnUpdate()
        {
            Entities
                .WithNone<TweenPause>()
                .ForEach((ref HDRPMaterialPropertyBaseColor colour, in DynamicBuffer<TweenState> tweenBuffer, in TweenHDRPFade tweenInfo) =>
                {
                    foreach (var tween in tweenBuffer)
                    {
                        if (tween.Id != tweenInfo.Id) continue;
                        var alpha = math.lerp(tweenInfo.Start, tweenInfo.End, tween.EasePercentage);
                        colour.Value = new float4(colour.Value.xyz, alpha);
                    }
                }).ScheduleParallel();
        }
    }
}
#endif