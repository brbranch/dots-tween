using Unity.Burst;
using Unity.Entities;

namespace DotsTween.Tweens
{
    [BurstCompile]
    internal struct TweenStopCommand : IComponentData
    {
        internal int TweenId;
    }
}