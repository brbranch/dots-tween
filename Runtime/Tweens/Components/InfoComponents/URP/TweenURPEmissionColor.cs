#if DOTS_TWEEN_URP
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;

namespace DotsTween.Tweens
{
    [BurstCompile]
    [WriteGroup(typeof(URPMaterialPropertyEmissionColor))]
    internal struct TweenURPEmissionColor : IComponentData, ITweenId, ITweenInfo<float4>
    {
        public uint Id;
        public float4 Start;
        public float4 End;

        public TweenURPEmissionColor(in uint id, in float4 start, in float4 end)
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