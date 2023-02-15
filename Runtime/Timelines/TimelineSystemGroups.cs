using DotsTween.Tweens;
using Unity.Entities;

namespace DotsTween.Timelines
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateBefore(typeof(TweenSimulationSystemGroup))]
    internal partial class TimelineSimulationSystemGroup : ComponentSystemGroup {}
}