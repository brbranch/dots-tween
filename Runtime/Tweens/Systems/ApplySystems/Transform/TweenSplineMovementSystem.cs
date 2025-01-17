﻿#if DOTS_TWEEN_SPLINES
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
            foreach (var (localTransformRef, tweenInfoRef, tweenBuffer) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<TweenSplineMovement>, DynamicBuffer<TweenState>>())
            {
                foreach (var tween in tweenBuffer)
                {
                    if (tween.Id != tweenInfoRef.ValueRO.Id || tween.IsPaused) continue;
                        
                    float splinePosition = math.lerp(tweenInfoRef.ValueRO.SplineTweenInfo.NormalizedStartPosition, tweenInfoRef.ValueRO.SplineTweenInfo.NormalizedEndPosition, tween.EasePercentage);
                    localTransformRef.ValueRW.Position = tweenInfoRef.ValueRO.SplineTweenInfo.Spline.EvaluatePosition(splinePosition);

                    switch (tweenInfoRef.ValueRO.SplineTweenInfo.Alignment.Mode)
                    {
                        case TweenSplineAlignMode.Full:
                        {
                            var forward = math.normalize(tweenInfoRef.ValueRO.SplineTweenInfo.Spline.EvaluateTangent(splinePosition));
                            var up = tweenInfoRef.ValueRO.SplineTweenInfo.Spline.EvaluateUpVector(splinePosition);
                            localTransformRef.ValueRW.Rotation = math.normalize(quaternion.LookRotationSafe(forward, up));
                            break;
                        }

                        case TweenSplineAlignMode.XAxis:
                        {
                            float3 tangent = math.normalize(tweenInfoRef.ValueRO.SplineTweenInfo.Spline.EvaluateTangent(splinePosition));
                            float angleRadians = math.atan2(tangent.y, tangent.z);
                            if (!float.IsNaN(angleRadians)) localTransformRef.ValueRW.Rotation = quaternion.Euler(new float3(angleRadians - tweenInfoRef.ValueRO.SplineTweenInfo.Alignment.AngleOffsetRadians, 0f, 0f));
                            break;
                        }
                        
                        case TweenSplineAlignMode.YAxis:
                        {
                            float3 tangent = math.normalize(tweenInfoRef.ValueRO.SplineTweenInfo.Spline.EvaluateTangent(splinePosition));
                            float angleRadians = math.atan2(tangent.z, tangent.x);
                            if (!float.IsNaN(angleRadians)) localTransformRef.ValueRW.Rotation = quaternion.Euler(new float3(0f, angleRadians - tweenInfoRef.ValueRO.SplineTweenInfo.Alignment.AngleOffsetRadians, 0f));
                            break;
                        }

                        case TweenSplineAlignMode.ZAxis:
                        {
                            float3 tangent = math.normalize(tweenInfoRef.ValueRO.SplineTweenInfo.Spline.EvaluateTangent(splinePosition));
                            float angleRadians = math.atan2(tangent.y, tangent.x);
                            if (!float.IsNaN(angleRadians)) localTransformRef.ValueRW.Rotation = quaternion.Euler(new float3(0f, 0f, angleRadians - tweenInfoRef.ValueRO.SplineTweenInfo.Alignment.AngleOffsetRadians));
                            break;
                        }
                        
                        case TweenSplineAlignMode.None:
                        default:
                        {
                            break;
                        }
                    }
                }
            }
        }
    }
}
#endif