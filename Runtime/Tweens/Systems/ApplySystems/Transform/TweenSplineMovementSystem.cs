#if DOTS_TWEEN_SPLINES
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine.Splines;

namespace DotsTween.Tweens
{
    [BurstCompile]
    [UpdateInGroup(typeof(TweenApplySystemGroup))]
    internal partial class TweenSplineMovementSystem : SystemBase
    {
        [BurstCompile]
        protected override void OnCreate()
        {
            RequireForUpdate<TweenSplineMovement>();
        }

        protected override void OnUpdate()
        {
            foreach (var (localTransformRef, tweenInfo, tweenBuffer) in SystemAPI.Query<RefRW<LocalTransform>, TweenSplineMovement, DynamicBuffer<TweenState>>())
            {
                foreach (var tween in tweenBuffer)
                {
                    if (tween.Id != tweenInfo.Id || tween.IsPaused) continue;
                        
                    float splinePosition = math.lerp(tweenInfo.SplineTweenInfo.NormalizedStartPosition, tweenInfo.SplineTweenInfo.NormalizedEndPosition, tween.EasePercentage);
                    localTransformRef.ValueRW.Position = tweenInfo.SplineTweenInfo.Spline.EvaluatePosition(splinePosition);

                    if (tweenInfo.SplineTweenInfo.AlignRotationToSpline)
                    {
                        var forward = math.normalize(tweenInfo.SplineTweenInfo.Spline.EvaluateTangent(splinePosition));
                        var up = tweenInfo.SplineTweenInfo.Spline.EvaluateUpVector(splinePosition);
                        localTransformRef.ValueRW.Rotation = quaternion.LookRotation(forward, up);
                    }
                }
            }
        }
    }
}
#endif