using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace DotsTween.Tweens
{
    [BurstCompile]
    [UpdateInGroup(typeof(TweenApplySystemGroup))]
    internal partial class TweenNonUniformScaleSystem : SystemBase
    {
        [BurstCompile]
        protected override void OnCreate()
        {
            RequireForUpdate<TweenNonUniformScale>();
        }

        [BurstCompile]
        protected override void OnUpdate()
        {
            Entities.ForEach((ref PostTransformScale postTransformScale, in DynamicBuffer<TweenState> tweenBuffer, in TweenNonUniformScale tweenInfo) =>
            {
                foreach (var tween in tweenBuffer)
                {
                    if (tween.Id == tweenInfo.Id && !tween.IsPaused)
                    {
                        var x = math.lerp(tweenInfo.Start.x, tweenInfo.End.x, tween.EasePercentage);
                        var y = math.lerp(tweenInfo.Start.y, tweenInfo.End.y, tween.EasePercentage);
                        var z = math.lerp(tweenInfo.Start.z, tweenInfo.End.z, tween.EasePercentage);
                        postTransformScale.Value = float3x3.Scale(x, y, z);
                        break;
                    }
                }
            }).ScheduleParallel();
        }
    }
}