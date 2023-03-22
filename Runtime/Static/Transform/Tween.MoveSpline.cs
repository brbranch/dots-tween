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
                bool alignRotation = false,
                in TweenParams tweenParams = default)
            {
                var splineTweenInfo = new SplineTweenInfo
                {
                    Spline = spline,
                    NormalizedEndPosition = 1f,
                    NormalizedStartPosition = 0f,
                    AlignRotationToSpline = alignRotation
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
                bool alignRotation = false,
                in TweenParams tweenParams = default)
            {
                var splineTweenInfo = new SplineTweenInfo
                {
                    Spline = spline,
                    NormalizedEndPosition = 1f,
                    NormalizedStartPosition = 0f,
                    AlignRotationToSpline = alignRotation
                };
                var command = new TweenSplineMovementCommand(entity, splineTweenInfo, duration, tweenParams);
                ecb.AddComponent(entity, command);
                return command.TweenParams.Id;
            }
            
            public static uint FromStartToEnd(ref EntityCommandBuffer.ParallelWriter ecb, in int sortKey, in Entity entity, ref NativeSpline spline, float duration, bool alignRotation = false, in TweenParams tweenParams = default)
            {
                var splineTweenInfo = new SplineTweenInfo
                {
                    Spline = spline,
                    NormalizedEndPosition = 1f,
                    NormalizedStartPosition = 0f,
                    AlignRotationToSpline = alignRotation
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
                bool alignRotation = false,
                in TweenParams tweenParams = default)
            {
                var splineTweenInfo = new SplineTweenInfo
                {
                    Spline = spline,
                    NormalizedEndPosition = normalizedSplineStartPosition,
                    NormalizedStartPosition = normalizedSplineEndPosition,
                    AlignRotationToSpline = alignRotation
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
                bool alignRotation = false,
                in TweenParams tweenParams = default)
            {
                var splineTweenInfo = new SplineTweenInfo
                {
                    Spline = spline,
                    NormalizedEndPosition = normalizedSplineStartPosition,
                    NormalizedStartPosition = normalizedSplineEndPosition,
                    AlignRotationToSpline = alignRotation
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
                bool alignRotation = false,
                in TweenParams tweenParams = default)
            {
                var splineTweenInfo = new SplineTweenInfo
                {
                    Spline = spline,
                    NormalizedEndPosition = normalizedSplineStartPosition,
                    NormalizedStartPosition = normalizedSplineEndPosition,
                    AlignRotationToSpline = alignRotation
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
                bool alignRotation = false,
                in TweenParams tweenParams = default)
            {
                return FromTo(ref state, entity, ref spline, normalizedSplineStartPosition, 1f, duration, alignRotation, tweenParams);
            }
            
            public static uint From(
                ref EntityCommandBuffer ecb,
                in Entity entity,
                ref NativeSpline spline,
                float normalizedSplineStartPosition,
                float duration,
                bool alignRotation = false,
                in TweenParams tweenParams = default)
            {
                return FromTo(ref ecb, entity, ref spline, normalizedSplineStartPosition, 1f, duration, alignRotation, tweenParams);
            }
            
            public static uint From(
                ref EntityCommandBuffer.ParallelWriter parallelWriter,
                in int sortKey,
                in Entity entity,
                ref NativeSpline spline,
                float normalizedSplineStartPosition,
                float duration,
                bool alignRotation = false,
                in TweenParams tweenParams = default)
            {
                return FromTo(ref parallelWriter, sortKey, entity, ref spline, normalizedSplineStartPosition, 1f, duration, alignRotation, tweenParams);
            }

            public static uint To(
                ref SystemState state,
                in Entity entity,
                ref NativeSpline spline,
                float normalizedSplineEndPosition,
                float duration,
                bool alignRotation = false,
                in TweenParams tweenParams = default)
            {
                return FromTo(ref state, entity, ref spline, 0f, normalizedSplineEndPosition, duration, alignRotation, tweenParams);
            }
            
            public static uint To(
                ref EntityCommandBuffer ecb,
                in Entity entity,
                ref NativeSpline spline,
                float normalizedSplineEndPosition,
                float duration,
                bool alignRotation = false,
                in TweenParams tweenParams = default)
            {
                return FromTo(ref ecb, entity, ref spline, 0f, normalizedSplineEndPosition, duration, alignRotation, tweenParams);
            }
            
            public static uint To(
                ref EntityCommandBuffer.ParallelWriter parallelWriter,
                in int sortKey,
                in Entity entity,
                ref NativeSpline spline,
                float normalizedSplineEndPosition,
                float duration,
                bool alignRotation = false,
                in TweenParams tweenParams = default)
            {
                return FromTo(ref parallelWriter, sortKey, entity, ref spline, 0f, normalizedSplineEndPosition, duration, alignRotation, tweenParams);
            }
        }
    }
}
#endif