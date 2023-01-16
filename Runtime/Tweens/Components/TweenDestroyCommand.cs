using Unity.Burst;
using Unity.Entities;

namespace DotsTween.Tweens
{
    [BurstCompile]
    public struct TweenDestroyCommand : IBufferElementData
    {
        public TweenDestroyCommand(int id)
        {
            Id = id;
        }

        public int Id;
    }
}