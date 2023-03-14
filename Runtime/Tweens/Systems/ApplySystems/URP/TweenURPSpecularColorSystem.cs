#if DOTS_TWEEN_URP
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;

namespace DotsTween.Tweens
{
    [BurstCompile]
    [UpdateInGroup(typeof(TweenApplySystemGroup))]
    internal partial class TweenURPSpecularColorSystem : SystemBase
    {
        [BurstCompile]
        protected override void OnCreate()
        {
            RequireForUpdate<TweenURPSpecularColor>();
        }

        [BurstCompile]
        protected override void OnUpdate()
        {
            Entities.ForEach((ref URPMaterialPropertySpecColor specularColor, in DynamicBuffer<TweenState> tweenBuffer, in TweenURPSpecularColor tweenInfo) =>
                {
                    foreach (var tween in tweenBuffer)
                    {
                        if (tween.Id != tweenInfo.Id || tween.IsPaused) continue;
                        specularColor.Value = math.lerp(tweenInfo.Start, tweenInfo.End, tween.EasePercentage);
                    }
                }).ScheduleParallel();
        }
    }
}
#endif