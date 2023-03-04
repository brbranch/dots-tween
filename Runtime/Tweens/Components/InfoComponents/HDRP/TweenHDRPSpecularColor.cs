#if DOTS_TWEEN_HDRP
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;

namespace DotsTween.Tweens
{
    [BurstCompile]
    [WriteGroup(typeof(HDRPMaterialPropertySpecularColor))]
    internal struct TweenHDRPSpecularColor : IComponentData, ITweenId, ITweenInfo<float4>
    {
        public int Id;
        public float4 Start;
        public float4 End;

        public TweenHDRPSpecularColor(in int id, in float4 start, in float4 end)
        {
            Id = id;
            Start = start;
            End = end;
        }

        public void SetTweenId(in int id)
        {
            Id = id;
        }

        public int GetTweenId()
        {
            return Id;
        }

        public void SetTweenInfo(in float4 start, in float4 end)
        {
            Start = start;
            End = end;
        }

        public float4 GetTweenStart()
        {
            return Start;
        }

        public float4 GetTweenEnd()
        {
            return End;
        }

        [BurstCompile] public void Cleanup() { }
    }
}
#endif