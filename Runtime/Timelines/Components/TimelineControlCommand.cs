using Unity.Burst;
using Unity.Entities;

namespace DotsTween.Timelines
{
    [BurstCompile]
    internal struct TimelineControlCommand : IComponentData
    {
        public int TimelinePlaybackId;
        public TimelineControlCommands Command;
    }

    internal enum TimelineControlCommands : byte
    {
        Resume = 0,
        Pause = 1,
        Stop = 2,
    }
}