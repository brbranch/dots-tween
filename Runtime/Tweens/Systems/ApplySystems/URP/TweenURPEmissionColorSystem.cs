#if DOTS_TWEEN_URP
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;

namespace DotsTween.Tweens
{
    [BurstCompile]
    [UpdateInGroup(typeof(TweenApplySystemGroup))]
    internal partial class TweenURPEmissionColorSystem : SystemBase
    {
        [BurstCompile]
        protected override void OnCreate()
        {
            RequireForUpdate<TweenURPEmissionColor>();
        }

        [BurstCompile]
        protected override void OnUpdate()
        {
            Entities
                .WithNone<TweenPause>()
                .ForEach((ref URPMaterialPropertyEmissionColor emissionColor, in DynamicBuffer<TweenState> tweenBuffer, in TweenURPEmissionColor tweenInfo) =>
                {
                    foreach (var tween in tweenBuffer)
                    {
                        if (tween.Id != tweenInfo.Id) continue;
                        emissionColor.Value = math.lerp(tweenInfo.Start, tweenInfo.End, tween.EasePercentage);
                    }
                }).ScheduleParallel();
        }
    }
}
#endif