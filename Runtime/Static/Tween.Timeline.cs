using System.Runtime.CompilerServices;
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
            [BurstCompile]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void Create(out TimelineComponent timelineComponent) => timelineComponent = new TimelineComponent(0f);

            [BurstCompile]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void Pause(ref EntityManager entityManager, uint timelinePlaybackId)
            {
                var e = entityManager.CreateEntity();
                entityManager.AddComponentData(e, new TimelineControlCommand()
                {
                    Command = TimelineControlCommands.Pause,
                    TimelinePlaybackId = timelinePlaybackId,
                });
            }

            [BurstCompile]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void Pause(ref EntityCommandBuffer ecb, uint timelinePlaybackId)
            {
                var e = ecb.CreateEntity();
                ecb.AddComponent(e, new TimelineControlCommand()
                {
                    Command = TimelineControlCommands.Pause,
                    TimelinePlaybackId = timelinePlaybackId,
                });
            }

            [BurstCompile]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void Resume(ref EntityManager entityManager, uint timelinePlaybackId)
            {
                var e = entityManager.CreateEntity();
                entityManager.AddComponentData(e, new TimelineControlCommand()
                {
                    Command = TimelineControlCommands.Resume,
                    TimelinePlaybackId = timelinePlaybackId,
                });
            }

            [BurstCompile]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void Resume(ref EntityCommandBuffer ecb, uint timelinePlaybackId)
            {
                var e = ecb.CreateEntity();
                ecb.AddComponent(e, new TimelineControlCommand()
                {
                    Command = TimelineControlCommands.Resume,
                    TimelinePlaybackId = timelinePlaybackId,
                });
            }

            [BurstCompile]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void Stop(ref EntityManager entityManager, uint timelinePlaybackId)
            {
                var e = entityManager.CreateEntity();
                entityManager.AddComponentData(e, new TimelineControlCommand()
                {
                    Command = TimelineControlCommands.Stop,
                    TimelinePlaybackId = timelinePlaybackId,
                });
            }

            [BurstCompile]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void Stop(ref EntityCommandBuffer ecb, uint timelinePlaybackId)
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