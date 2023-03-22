#if DOTS_TWEEN_HDRP
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;

namespace DotsTween.Tweens
{
    [BurstCompile]
    [WriteGroup(typeof(HDRPMaterialPropertyEmissiveColor))]
    internal struct TweenHDRPEmissiveColor : IComponentData, ITweenId, ITweenInfo<float3>
    {
        public uint Id;
        public float3 Start;
        public float3 End;

        public TweenHDRPEmissiveColor(in uint id, in float3 start, in float3 end)
        {
            Id = id;
            Start = start;
            End = end;
        }

        public void SetTweenId(in uint id)
        {
            Id = id;
        }

        public uint GetTweenId()
        {
            return Id;
        }

        public void SetTweenInfo(in float3 start, in float3 end)
        {
            Start = start;
            End = end;
        }

        public float3 GetTweenStart()
        {
            return Start;
        }

        public float3 GetTweenEnd()
        {
            return End;
        }

        [BurstCompile] public void Cleanup() { }
    }
}
#endif