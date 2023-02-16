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
        public int Id;
        public quaternion Start;
        public quaternion End;

        public TweenRotation(in int id, in quaternion start, in quaternion end)
        {
            Id = id;
            Start = start;
            End = end;
        }

        [BurstCompile]
        public void SetTweenId(in int id)
        {
            Id = id;
        }

        [BurstCompile]
        public int GetTweenId()
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
    }
}