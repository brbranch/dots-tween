#if DOTS_TWEEN_SPLINES
using System;
using System.Runtime.InteropServices;
using Unity.Burst;
using Unity.Entities;
using UnityEngine.Splines;

namespace DotsTween.Tweens
{
    [BurstCompile]
    public struct TweenSplineMovementCommand : IComponentData, ITweenParams, ITweenInfo<SplineTweenInfo>
    {
        public SplineTweenInfo SplineTweenInfo { get; private set; }
        public TweenParams TweenParams { get; private set; }
        
        public TweenSplineMovementCommand(in SplineTweenInfo info, float duration, TweenParams tweenParams = default)
        {
            tweenParams.Duration = duration;
            TweenParams = tweenParams;
            SplineTweenInfo = info;
            tweenParams.GenerateId(TypeManager.GetTypeIndex<TweenSplineMovement>().Value);
        }
        
        [BurstCompile]
        public void SetTweenInfo(in SplineTweenInfo start, in SplineTweenInfo end = default)
        {
            SplineTweenInfo = start;
        }

        [BurstCompile]
        public void SetTweenParams(in TweenParams tweenParams)
        {
            TweenParams = tweenParams;
        }

        [BurstCompile]
        public TweenParams GetTweenParams()
        {
            return TweenParams;
        }

        [BurstCompile]
        public SplineTweenInfo GetTweenStart()
        {
            return SplineTweenInfo;
        }

        [BurstCompile]
        public SplineTweenInfo GetTweenEnd()
        {
            return SplineTweenInfo;
        }

        public void Cleanup()
        {
            SplineTweenInfo.Dispose();
        }
    }

    public struct SplineTweenInfo : IEquatable<SplineTweenInfo>, IDisposable
    {
        public NativeSpline Spline;
        [MarshalAs(UnmanagedType.U1)] public bool AlignRotationToSpline;
        public float NormalizedStartPosition;
        public float NormalizedEndPosition;

        public bool Equals(SplineTweenInfo other)
        {
            return Spline.Count == other.Spline.Count
                && Spline.Closed == other.Spline.Closed
                && Spline.Curves == other.Spline.Curves
                && Spline.Knots == other.Spline.Knots
                && AlignRotationToSpline == other.AlignRotationToSpline
                && NormalizedStartPosition.Equals(other.NormalizedStartPosition)
                && NormalizedEndPosition.Equals(other.NormalizedEndPosition);
        }

        public override bool Equals(object obj)
        {
            return obj is SplineTweenInfo other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Spline, AlignRotationToSpline, NormalizedStartPosition, NormalizedEndPosition);
        }

        public void Dispose()
        {
            Spline.Dispose();
        }
    }
}
#endif