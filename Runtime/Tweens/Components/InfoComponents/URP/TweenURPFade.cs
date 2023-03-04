#if DOTS_TWEEN_URP
using Unity.Burst;
using Unity.Entities;
using Unity.Rendering;

namespace DotsTween.Tweens
{
    [BurstCompile]
    [WriteGroup(typeof(URPMaterialPropertyBaseColor))]
    internal struct TweenURPFade : IComponentData, ITweenId, ITweenInfo<float>
    {
        public int Id;
        public float Start;
        public float End;

        public TweenURPFade(in int id, in float start, in float end)
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