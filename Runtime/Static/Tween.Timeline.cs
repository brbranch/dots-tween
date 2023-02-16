using DotsTween.Timelines;
using Unity.Entities;

namespace DotsTween
{
    public static partial class Tween
    {
        public static class Timeline
        {
            public static TimelineComponent Create() => new();

            public static void Pause(EntityManager entityManager, int timelinePlaybackId)
            {
                var e = entityManager.CreateEntity();
                entityManager.AddComponentData(e, new TimelineControlCommand()
                {
                    Command = TimelineControlCommands.Pause,
                    TimelinePlaybackId = timelinePlaybackId,
                });
            }

            public static void Pause(ref EntityCommandBuffer ecb, int timelinePlaybackId)
            {
                var e = ecb.CreateEntity();
                ecb.AddComponent(e, new TimelineControlCommand()
                {
                    Command = TimelineControlCommands.Pause,
                    TimelinePlaybackId = timelinePlaybackId,
                });
            }

            public static void Resume(EntityManager entityManager, int timelinePlaybackId)
            {
                var e = entityManager.CreateEntity();
                entityManager.AddComponentData(e, new TimelineControlCommand()
                {
                    Command = TimelineControlCommands.Resume,
                    TimelinePlaybackId = timelinePlaybackId,
                });
            }

            public static void Resume(ref EntityCommandBuffer ecb, int timelinePlaybackId)
            {
                var e = ecb.CreateEntity();
                ecb.AddComponent(e, new TimelineControlCommand()
                {
                    Command = TimelineControlCommands.Resume,
                    TimelinePlaybackId = timelinePlaybackId,
                });
            }

            public static void Stop(EntityManager entityManager, int timelinePlaybackId)
            {
                var e = entityManager.CreateEntity();
                entityManager.AddComponentData(e, new TimelineControlCommand()
                {
                    Command = TimelineControlCommands.Stop,
                    TimelinePlaybackId = timelinePlaybackId,
                });
            }

            public static void Stop(ref EntityCommandBuffer ecb, int timelinePlaybackId)
            {
                var e = ecb.CreateEntity();
                ecb.AddComponent(e, new TimelineControlCommand()
                {
                    Command = TimelineControlCommands.Stop,
                    TimelinePlaybackId = timelinePlaybackId,
                });
            }
        }
    }
}