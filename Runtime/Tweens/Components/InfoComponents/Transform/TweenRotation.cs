using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace DotsTween.Tweens
{
    [BurstCompile]
    [WriteGroup(typeof(LocalTransform))]
    internal struct TweenRotation : IComponentData, ITweenId, ITweenInfo<quaternion>
    {
        public uint Id;
        public quaternion Start;
        public quaternion End;

        public TweenRotation(in uint id, in quaternion start, in quaternion end)
        {
            Id = id;
            Start = start;
            End = end;
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
        public void SetTweenInfo(in quaternion start, in quaternion end)
        {
            Start = start;
            End = end;
        }

        [BurstCompile]
        public quaternion GetTweenStart()
        {
            return Start;
        }

        [BurstCompile]
        public quaternion GetTweenEnd()
        {
            return End;
        }
        
        [BurstCompile] public void Cleanup() { }
    }
}