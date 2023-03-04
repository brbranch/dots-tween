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
            public static void FromStartToEnd(
                ref SystemState state,
                in Entity entity,
                ref NativeSpline spline,
                float duration,
                bool alignRotation = false,
                in TweenParams tweenParams = default)
            {
                state.EntityManager.AddComponentData(entity, new TweenSplineMovementCommand(
                    new SplineTweenInfo
                    {
                        Spline = spline,
                        NormalizedEndPosition = 1f,
                        NormalizedStartPosition = 0f,
                        AlignRotationToSpline = alignRotation
                    }, duration, tweenParams));
            }
            
            public static void FromStartToEnd(
                ref EntityCommandBuffer ecb,
                in Entity entity,
                ref NativeSpline spline,
                float duration,
                bool alignRotation = false,
                in TweenParams tweenParams = default)
            {
                ecb.AddComponent(entity, new TweenSplineMovementCommand(
                    new SplineTweenInfo
                    {
                        Spline = spline,
                        NormalizedEndPosition = 1f,
                        NormalizedStartPosition = 0f,
                        AlignRotationToSpline = alignRotation
                    }, duration, tweenParams));
            }
            
            public static void FromStartToEnd(ref EntityCommandBuffer.ParallelWriter ecb, in int sortKey, in Entity entity, ref NativeSpline spline, float duration, bool alignRotation = false, in TweenParams tweenParams = default)
            {
                ecb.AddComponent(sortKey, entity, new TweenSplineMovementCommand(
                    new SplineTweenInfo
                    {
                        Spline = spline,
                        NormalizedEndPosition = 1f,
                        NormalizedStartPosition = 0f,
                        AlignRotationToSpline = alignRotation
                    }, duration, tweenParams));
            }

            public static void FromTo(
                ref SystemState state,
                in Entity entity,
                ref NativeSpline spline,
                float normalizedSplineStartPosition,
                float normalizedSplineEndPosition,
                float duration,
                bool alignRotation = false,
                in TweenParams tweenParams = default)
            {
                state.EntityManager.AddComponentData(entity, new TweenSplineMovementCommand(
                    new SplineTweenInfo
                    {
                        Spline = spline,
                        NormalizedEndPosition = normalizedSplineStartPosition,
                        NormalizedStartPosition = normalizedSplineEndPosition,
                        AlignRotationToSpline = alignRotation
                    }, duration, tweenParams));
            }

            public static void FromTo(
                ref EntityCommandBuffer ecb,
                in Entity entity,
                ref NativeSpline spline,
                float normalizedSplineStartPosition,
                float normalizedSplineEndPosition,
                float duration,
                bool alignRotation = false,
                in TweenParams tweenParams = default)
            {
                ecb.AddComponent(entity, new TweenSplineMovementCommand(
                    new SplineTweenInfo
                    {
                        Spline = spline,
                        NormalizedEndPosition = normalizedSplineStartPosition,
                        NormalizedStartPosition = normalizedSplineEndPosition,
                        AlignRotationToSpline = alignRotation
                    }, duration, tweenParams));
            }

            public static void FromTo(
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
                ecb.AddComponent(sortKey, entity, new TweenSplineMovementCommand(
                    new SplineTweenInfo
                    {
                        Spline = spline,
                        NormalizedEndPosition = normalizedSplineStartPosition,
                        NormalizedStartPosition = normalizedSplineEndPosition,
                        AlignRotationToSpline = alignRotation
                    }, duration, tweenParams));
            }

            public static void From(
                ref SystemState state,
                in Entity entity,
                ref NativeSpline spline,
                float normalizedSplineStartPosition,
                float duration,
                bool alignRotation = false,
                in TweenParams tweenParams = default)
            {
                FromTo(ref state, entity, ref spline, normalizedSplineStartPosition, 1f, duration, alignRotation, tweenParams);
            }
            
            public static void From(
                ref EntityCommandBuffer ecb,
                in Entity entity,
                ref NativeSpline spline,
                float normalizedSplineStartPosition,
                float duration,
                bool alignRotation = false,
                in TweenParams tweenParams = default)
            {
                FromTo(ref ecb, entity, ref spline, normalizedSplineStartPosition, 1f, duration, alignRotation, tweenParams);
            }
            
            public static void From(
                ref EntityCommandBuffer.ParallelWriter parallelWriter,
                in int sortKey,
                in Entity entity,
                ref NativeSpline spline,
                float normalizedSplineStartPosition,
                float duration,
                bool alignRotation = false,
                in TweenParams tweenParams = default)
            {
                FromTo(ref parallelWriter, sortKey, entity, ref spline, normalizedSplineStartPosition, 1f, duration, alignRotation, tweenParams);
            }

            public static void To(
                ref SystemState state,
                in Entity entity,
                ref NativeSpline spline,
                float normalizedSplineEndPosition,
                float duration,
                bool alignRotation = false,
                in TweenParams tweenParams = default)
            {
                FromTo(ref state, entity, ref spline, 0f, normalizedSplineEndPosition, duration, alignRotation, tweenParams);
            }
            
            public static void To(
                ref EntityCommandBuffer ecb,
                in Entity entity,
                ref NativeSpline spline,
                float normalizedSplineEndPosition,
                float duration,
                bool alignRotation = false,
                in TweenParams tweenParams = default)
            {
                FromTo(ref ecb, entity, ref spline, 0f, normalizedSplineEndPosition, duration, alignRotation, tweenParams);
            }
            
            public static void To(
                ref EntityCommandBuffer.ParallelWriter parallelWriter,
                in int sortKey,
                in Entity entity,
                ref NativeSpline spline,
                float normalizedSplineEndPosition,
                float duration,
                bool alignRotation = false,
                in TweenParams tweenParams = default)
            {
                FromTo(ref parallelWriter, sortKey, entity, ref spline, 0f, normalizedSplineEndPosition, duration, alignRotation, tweenParams);
            }
        }
    }
}
#endif