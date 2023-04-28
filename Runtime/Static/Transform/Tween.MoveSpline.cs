#if DOTS_TWEEN_SPLINES
using DotsTween.Tweens;
using Unity.Entities;
using UnityEngine.Splines;

namespace DotsTween
{
    public static partial class Tween
    {
        public static class MoveSpline
        {
            public static uint FromStartToEnd(
                ref SystemState state,
                in Entity entity,
                ref NativeSpline spline,
                float duration,
                TweenSplineAlignmentSettings alignMode = default,
                in TweenParams tweenParams = default)
            {
                var splineTweenInfo = new SplineTweenInfo
                {
                    Spline = spline,
                    NormalizedEndPosition = 1f,
                    NormalizedStartPosition = 0f,
                    Alignment = alignMode
                };
                var command = new TweenSplineMovementCommand(entity, splineTweenInfo, duration, tweenParams);
                state.EntityManager.AddComponentData(entity, command);
                return command.TweenParams.Id;
            }
            
            public static uint FromStartToEnd(
                ref EntityCommandBuffer ecb,
                in Entity entity,
                ref NativeSpline spline,
                float duration,
                TweenSplineAlignmentSettings alignMode = default,
                in TweenParams tweenParams = default)
            {
                var splineTweenInfo = new SplineTweenInfo
                {
                    Spline = spline,
                    NormalizedEndPosition = 1f,
                    NormalizedStartPosition = 0f,
                    Alignment = alignMode
                };
                var command = new TweenSplineMovementCommand(entity, splineTweenInfo, duration, tweenParams);
                ecb.AddComponent(entity, command);
                return command.TweenParams.Id;
            }
            
            public static uint FromStartToEnd(ref EntityCommandBuffer.ParallelWriter ecb, in int sortKey, in Entity entity, ref NativeSpline spline, float duration, TweenSplineAlignmentSettings alignMode = default, in TweenParams tweenParams = default)
            {
                var splineTweenInfo = new SplineTweenInfo
                {
                    Spline = spline,
                    NormalizedEndPosition = 1f,
                    NormalizedStartPosition = 0f,
                    Alignment = alignMode
                };
                var command = new TweenSplineMovementCommand(entity, splineTweenInfo, duration, tweenParams);
                ecb.AddComponent(sortKey, entity, command);
                return command.TweenParams.Id;
            }

            public static uint FromTo(
                ref SystemState state,
                in Entity entity,
                ref NativeSpline spline,
                float normalizedSplineStartPosition,
                float normalizedSplineEndPosition,
                float duration,
                TweenSplineAlignmentSettings alignMode = default,
                in TweenParams tweenParams = default)
            {
                var splineTweenInfo = new SplineTweenInfo
                {
                    Spline = spline,
                    NormalizedEndPosition = normalizedSplineStartPosition,
                    NormalizedStartPosition = normalizedSplineEndPosition,
                    Alignment = alignMode
                };
                var command = new TweenSplineMovementCommand(entity, splineTweenInfo, duration, tweenParams);
                state.EntityManager.AddComponentData(entity, command);
                return command.TweenParams.Id;
            }

            public static uint FromTo(
                ref EntityCommandBuffer ecb,
                in Entity entity,
                ref NativeSpline spline,
                float normalizedSplineStartPosition,
                float normalizedSplineEndPosition,
                float duration,
                TweenSplineAlignmentSettings alignMode = default,
                in TweenParams tweenParams = default)
            {
                var splineTweenInfo = new SplineTweenInfo
                {
                    Spline = spline,
                    NormalizedEndPosition = normalizedSplineStartPosition,
                    NormalizedStartPosition = normalizedSplineEndPosition,
                    Alignment = alignMode
                };
                var command = new TweenSplineMovementCommand(entity, splineTweenInfo, duration, tweenParams);
                ecb.AddComponent(entity, command);
                return command.TweenParams.Id;
            }

            public static uint FromTo(
                ref EntityCommandBuffer.ParallelWriter ecb,
                in int sortKey,
                in Entity entity,
                ref NativeSpline spline,
                float normalizedSplineStartPosition,
                float normalizedSplineEndPosition,
                float duration,
                TweenSplineAlignmentSettings alignMode = default,
                in TweenParams tweenParams = default)
            {
                var splineTweenInfo = new SplineTweenInfo
                {
                    Spline = spline,
                    NormalizedEndPosition = normalizedSplineStartPosition,
                    NormalizedStartPosition = normalizedSplineEndPosition,
                    Alignment = alignMode
                };
                var command = new TweenSplineMovementCommand(entity, splineTweenInfo, duration, tweenParams);
                ecb.AddComponent(sortKey, entity, command);
                return command.TweenParams.Id;
            }

            public static uint From(
                ref SystemState state,
                in Entity entity,
                ref NativeSpline spline,
                float normalizedSplineStartPosition,
                float duration,
                TweenSplineAlignmentSettings alignMode = default,
                in TweenParams tweenParams = default)
            {
                return FromTo(ref state, entity, ref spline, normalizedSplineStartPosition, 1f, duration, alignMode, tweenParams);
            }
            
            public static uint From(
                ref EntityCommandBuffer ecb,
                in Entity entity,
                ref NativeSpline spline,
                float normalizedSplineStartPosition,
                float duration,
                TweenSplineAlignmentSettings alignMode = default,
                in TweenParams tweenParams = default)
            {
                return FromTo(ref ecb, entity, ref spline, normalizedSplineStartPosition, 1f, duration, alignMode, tweenParams);
            }
            
            public static uint From(
                ref EntityCommandBuffer.ParallelWriter parallelWriter,
                in int sortKey,
                in Entity entity,
                ref NativeSpline spline,
                float normalizedSplineStartPosition,
                float duration,
                TweenSplineAlignmentSettings alignMode = default,
                in TweenParams tweenParams = default)
            {
                return FromTo(ref parallelWriter, sortKey, entity, ref spline, normalizedSplineStartPosition, 1f, duration, alignMode, tweenParams);
            }

            public static uint To(
                ref SystemState state,
                in Entity entity,
                ref NativeSpline spline,
                float normalizedSplineEndPosition,
                float duration,
                TweenSplineAlignmentSettings alignMode = default,
                in TweenParams tweenParams = default)
            {
                return FromTo(ref state, entity, ref spline, 0f, normalizedSplineEndPosition, duration, alignMode, tweenParams);
            }
            
            public static uint To(
                ref EntityCommandBuffer ecb,
                in Entity entity,
                ref NativeSpline spline,
                float normalizedSplineEndPosition,
                float duration,
                TweenSplineAlignmentSettings alignMode = default,
                in TweenParams tweenParams = default)
            {
                return FromTo(ref ecb, entity, ref spline, 0f, normalizedSplineEndPosition, duration, alignMode, tweenParams);
            }
            
            public static uint To(
                ref EntityCommandBuffer.ParallelWriter parallelWriter,
                in int sortKey,
                in Entity entity,
                ref NativeSpline spline,
                float normalizedSplineEndPosition,
                float duration,
                TweenSplineAlignmentSettings alignMode = default,
                in TweenParams tweenParams = default)
            {
                return FromTo(ref parallelWriter, sortKey, entity, ref spline, 0f, normalizedSplineEndPosition, duration, alignMode, tweenParams);
            }
        }
    }
}
#endif