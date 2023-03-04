using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace DotsTween.Tweens
{
    [BurstCompile]
    [WriteGroup(typeof(LocalTransform))]
    internal struct TweenScale : IComponentData, ITweenId, ITweenInfo<float>
    {
        public int Id;
        public float Start;
        public float End;

        public TweenScale(in int id, in float start, in float end)
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
        public void SetTweenInfo(in float start, in float end)
        {
            Start = start;
            End = end;
        }

        [BurstCompile]
        public float GetTweenStart()
        {
            return Start;
        }

        [BurstCompile]
        public float GetTweenEnd()
        {
            return End;
        }
        
        [BurstCompile] public void Cleanup() { }
    }
}