#if DOTS_TWEEN_URP
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;

namespace DotsTween.Tweens
{
    [BurstCompile]
    [UpdateInGroup(typeof(TweenApplySystemGroup))]
    internal partial class TweenURPFadeSystem : SystemBase
    {
        [BurstCompile]
        protected override void OnCreate()
        {
            RequireForUpdate<TweenURPFade>();
        }

        [BurstCompile]
        protected override void OnUpdate()
        {
            Entities.ForEach((ref URPMaterialPropertyBaseColor baseColor, in DynamicBuffer<TweenState> tweenBuffer, in TweenURPFade tweenInfo) =>
            {
                foreach (var tween in tweenBuffer)
                {
                    if (tween.Id != tweenInfo.Id || tween.IsPaused) continue;
                    var alpha = math.lerp(tweenInfo.Start, tweenInfo.End, tween.EasePercentage);
                    baseColor.Value = new float4(baseColor.Value.xyz, alpha);
                }
            }).ScheduleParallel();
        }
    }
}
#endif