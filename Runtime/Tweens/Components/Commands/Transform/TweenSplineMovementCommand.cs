#if DOTS_TWEEN_SPLINES
using System;
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
        
        public TweenSplineMovementCommand(in Entity entity, in SplineTweenInfo info, float duration, TweenParams tweenParams = default)
        {
            tweenParams.Duration = duration;
            tweenParams.Id = tweenParams.GenerateId(entity.GetHashCode(), info.NormalizedStartPosition.GetHashCode(), info.NormalizedEndPosition.GetHashCode(), TypeManager.GetTypeIndex<TweenSplineMovement>().Value);
            TweenParams = tweenParams;
            SplineTweenInfo = info;
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
        public TweenSplineAlignmentSettings Alignment;
        public float NormalizedStartPosition;
        public float NormalizedEndPosition;

        public bool Equals(SplineTweenInfo other)
        {
            return Spline.Count == other.Spline.Count
                && Spline.Closed == other.Spline.Closed
                && Spline.Curves == other.Spline.Curves
                && Spline.Knots == other.Spline.Knots
                && Alignment == other.Alignment
                && NormalizedStartPosition.Equals(other.NormalizedStartPosition)
                && NormalizedEndPosition.Equals(other.NormalizedEndPosition);
        }

        public override bool Equals(object obj)
        {
            return obj is SplineTweenInfo other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Spline, Alignment, NormalizedStartPosition, NormalizedEndPosition);
        }

        public void Dispose()
        {
            Spline.Dispose();
        }
    }

    public struct TweenSplineAlignmentSettings : IEquatable<TweenSplineAlignmentSettings>
    {
        private const float Deg2Rad = 0.017453292f;
        
        public TweenSplineAlignMode Mode;
        public float AngleOffsetRadians;

        public TweenSplineAlignmentSettings(TweenSplineAlignMode mode, float angleOffsetRadians)
        {
            Mode = mode;
            AngleOffsetRadians = angleOffsetRadians;
        }
        
        public TweenSplineAlignmentSettings(float angleOffsetDegrees, TweenSplineAlignMode mode)
        {
            Mode = mode;
            AngleOffsetRadians = angleOffsetDegrees * Deg2Rad;
        }

        public static bool operator ==(TweenSplineAlignmentSettings a, TweenSplineAlignmentSettings b) => a.Equals(b);
        public static bool operator !=(TweenSplineAlignmentSettings a, TweenSplineAlignmentSettings b) => !(a == b);

        public bool Equals(TweenSplineAlignmentSettings other)
        {
            return Mode == other.Mode && AngleOffsetRadians.Equals(other.AngleOffsetRadians);
        }

        public override bool Equals(object obj)
        {
            return obj is TweenSplineAlignmentSettings other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine((int)Mode, AngleOffsetRadians);
        }
    }

    public enum TweenSplineAlignMode
    {
        None = 0,
        Full,
        XAxis,
        YAxis,
        ZAxis,
    }
}
#endif