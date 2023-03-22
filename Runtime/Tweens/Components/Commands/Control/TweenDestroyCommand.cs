using Unity.Burst;
using Unity.Entities;

namespace DotsTween.Tweens
{
    [BurstCompile]
    public struct TweenDestroyCommand : IBufferElementData
    {
        internal readonly uint TweenId;
        
        public TweenDestroyCommand(uint tweenId)
        {
            TweenId = tweenId;
        }
    }
}