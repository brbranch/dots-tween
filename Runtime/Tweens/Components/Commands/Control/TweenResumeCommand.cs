using Unity.Burst;
using Unity.Entities;

namespace DotsTween.Tweens
{
    [BurstCompile]
    internal struct TweenResumeCommand : IComponentData
    {
        internal int TweenId;
    }
}