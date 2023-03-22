#if DOTS_TWEEN_SPLINES
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace DotsTween.Tweens
{
    [BurstCompile]
    [WriteGroup(typeof(LocalTransform))]
    internal struct TweenSplineMovement : IComponentData, ITweenId, ITweenInfo<SplineTweenInfo>
    {
        public uint Id;
        public SplineTweenInfo SplineTweenInfo;

        public TweenSplineMovement(in uint id, in SplineTweenInfo start, in SplineTweenInfo end = default)
        {
            Id = id;
            SplineTweenInfo = start;
        }

        [BurstCompile]
        public void SetTweenId(in uint id)
        {
            Id = id;
        }

        [BurstCompile]
        public uint GetTweenId()
        {
            return Id;
        }

        [BurstCompile]
        public void SetTweenInfo(in SplineTweenInfo start, in SplineTweenInfo end = default)
        {
            SplineTweenInfo = start;
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

        [BurstCompile] public void Cleanup()
        {
            SplineTweenInfo.Dispose();            
        }
    }
}
#endif