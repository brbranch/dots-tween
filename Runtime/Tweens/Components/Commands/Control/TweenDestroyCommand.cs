using Unity.Burst;
using Unity.Entities;

namespace DotsTween.Tweens
{
    [BurstCompile]
    public struct TweenDestroyCommand : IBufferElementData
    {
        internal readonly int TweenId;
        
        public TweenDestroyCommand(int tweenId)
        {
            TweenId = tweenId;
        }
    }
}