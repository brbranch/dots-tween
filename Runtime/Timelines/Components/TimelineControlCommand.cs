using Unity.Entities;

namespace DotsTween.Timelines
{
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