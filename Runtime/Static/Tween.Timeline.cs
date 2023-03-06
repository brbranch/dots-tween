using DotsTween.Timelines;
using Unity.Burst;
using Unity.Entities;

namespace DotsTween
{
    public static partial class Tween
    {
        [BurstCompile]
        public static class Timeline
        {
            [BurstDiscard]
            public static TimelineComponent Create() => new(0f);

            [BurstCompile]
            public static void Pause(ref EntityManager entityManager, int timelinePlaybackId)
            {
                var e = entityManager.CreateEntity();
                entityManager.AddComponentData(e, new TimelineControlCommand()
                {
                    Command = TimelineControlCommands.Pause,
                    TimelinePlaybackId = timelinePlaybackId,
                });
            }

            [BurstCompile]
            public static void Pause(ref EntityCommandBuffer ecb, int timelinePlaybackId)
            {
                var e = ecb.CreateEntity();
                ecb.AddComponent(e, new TimelineControlCommand()
                {
                    Command = TimelineControlCommands.Pause,
                    TimelinePlaybackId = timelinePlaybackId,
                });
            }

            [BurstCompile]
            public static void Resume(ref EntityManager entityManager, int timelinePlaybackId)
            {
                var e = entityManager.CreateEntity();
                entityManager.AddComponentData(e, new TimelineControlCommand()
                {
                    Command = TimelineControlCommands.Resume,
                    TimelinePlaybackId = timelinePlaybackId,
                });
            }

            [BurstCompile]
            public static void Resume(ref EntityCommandBuffer ecb, int timelinePlaybackId)
            {
                var e = ecb.CreateEntity();
                ecb.AddComponent(e, new TimelineControlCommand()
                {
                    Command = TimelineControlCommands.Resume,
                    TimelinePlaybackId = timelinePlaybackId,
                });
            }

            [BurstCompile]
            public static void Stop(ref EntityManager entityManager, int timelinePlaybackId)
            {
                var e = entityManager.CreateEntity();
                entityManager.AddComponentData(e, new TimelineControlCommand()
                {
                    Command = TimelineControlCommands.Stop,
                    TimelinePlaybackId = timelinePlaybackId,
                });
            }

            [BurstCompile]
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