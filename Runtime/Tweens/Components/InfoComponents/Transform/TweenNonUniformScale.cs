using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace DotsTween.Tweens
{
    [BurstCompile]
    [WriteGroup(typeof(PostTransformScale))]
    internal struct TweenNonUniformScale : IComponentData, ITweenId, ITweenInfo<float3>
    {
        public int Id;
        public float3 Start;
        public float3 End;

        public TweenNonUniformScale(in int id, in float3 start, in float3 end)
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
        public void SetTweenInfo(in float3 start, in float3 end)
        {
            Start = start;
            End = end;
        }

        [BurstCompile]
        public float3 GetTweenStart()
        {
            return Start;
        }

        [BurstCompile]
        public float3 GetTweenEnd()
        {
            return End;
        }
        
        [BurstCompile] public void Cleanup() { }
    }
}