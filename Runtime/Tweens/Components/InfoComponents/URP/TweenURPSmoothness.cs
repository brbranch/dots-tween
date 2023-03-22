#if DOTS_TWEEN_URP
using Unity.Burst;
using Unity.Entities;
using Unity.Rendering;

namespace DotsTween.Tweens
{
    [BurstCompile]
    [WriteGroup(typeof(URPMaterialPropertySmoothness))]
    internal struct TweenURPSmoothness : IComponentData, ITweenId, ITweenInfo<float>
    {
        public uint Id;
        public float Start;
        public float End;

        public TweenURPSmoothness(in uint id, in float start, in float end)
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

        public void SetTweenInfo(in float start, in float end)
        {
            Start = start;
            End = end;
        }

        public float GetTweenStart()
        {
            return Start;
        }

        public float GetTweenEnd()
        {
            return End;
        }
        
        [BurstCompile] public void Cleanup() { }
    }
}
#endif