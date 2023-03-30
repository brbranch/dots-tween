using DotsTween.Tweens;
using Unity.Burst;
using Unity.Entities;

namespace DotsTween
{
    public static partial class Tween
    {
        [BurstCompile]
        public static class Controls
        {
#region Control Specific Tween

            [BurstCompile]
            public static void Pause(ref EntityManager entityManager, in Entity entity, in uint tweenId)
            {
                entityManager.GetBuffer<TweenPauseInfo>(entity).Add(new TweenPauseInfo(tweenId));
                entityManager.AddComponent<TweenPauseCommand>(entity);
            }

            [BurstCompile]
            public static void Pause(ref EntityCommandBuffer commandBuffer, in Entity entity, in uint tweenId)
            {
                commandBuffer.AppendToBuffer<TweenPauseInfo>(entity, new TweenPauseInfo(tweenId));
                commandBuffer.AddComponent<TweenPauseCommand>(entity);
            }

            [BurstCompile]
            public static void Pause(ref EntityCommandBuffer.ParallelWriter parallelWriter, in int sortKey, in Entity entity, in uint tweenId)
            {
                parallelWriter.AppendToBuffer<TweenPauseInfo>(sortKey, entity, new TweenPauseInfo(tweenId));
                parallelWriter.AddComponent<TweenPauseCommand>(sortKey, entity);
            }

            [BurstCompile]
            public static void Resume(ref EntityManager entityManager, in Entity entity, in uint tweenId)
            {
                entityManager.GetBuffer<TweenResumeInfo>(entity).Add(new TweenResumeInfo(tweenId));
                entityManager.AddComponent<TweenResumeCommand>(entity);
            }

            [BurstCompile]
            public static void Resume(ref EntityCommandBuffer commandBuffer, in Entity entity, in uint tweenId)
            {
                commandBuffer.AppendToBuffer<TweenResumeInfo>(entity, new TweenResumeInfo(tweenId));
                commandBuffer.AddComponent<TweenResumeCommand>(entity);
            }

            [BurstCompile]
            public static void Resume(ref EntityCommandBuffer.ParallelWriter parallelWriter, in int sortKey, in Entity entity, in uint tweenId)
            {
                parallelWriter.AppendToBuffer<TweenResumeInfo>(sortKey, entity, new TweenResumeInfo(tweenId));
                parallelWriter.AddComponent<TweenResumeCommand>(sortKey, entity);
            }

            [BurstCompile]
            public static void Stop(ref EntityManager entityManager, in Entity entity, in uint tweenId)
            {
                var destroyBuffer = entityManager.GetBuffer<TweenDestroyCommand>(entity);
                destroyBuffer.Add(new TweenDestroyCommand(tweenId));
            }

            [BurstCompile]
            public static void Stop(ref EntityCommandBuffer commandBuffer, in Entity entity, in uint tweenId)
            {
                commandBuffer.AppendToBuffer<TweenDestroyCommand>(entity, new TweenDestroyCommand(tweenId));
            }

            [BurstCompile]
            public static void Stop(ref EntityCommandBuffer.ParallelWriter parallelWriter, in int sortKey, in Entity entity, in uint tweenId)
            {
                parallelWriter.AppendToBuffer<TweenDestroyCommand>(sortKey, entity, new TweenDestroyCommand(tweenId));
            }

#endregion

#region Control All

            [BurstCompile]
            public static void PauseAll(ref EntityManager entityManager, in Entity entity)
            {
                entityManager.GetBuffer<TweenPauseInfo>(entity).Add(new TweenPauseInfo());
                entityManager.AddComponent<TweenPauseCommand>(entity);
            }

            [BurstCompile]
            public static void PauseAll(ref EntityCommandBuffer commandBuffer, in Entity entity)
            {
                commandBuffer.AppendToBuffer<TweenPauseInfo>(entity, new TweenPauseInfo());
                commandBuffer.AddComponent<TweenPauseCommand>(entity);
            }

            [BurstCompile]
            public static void PauseAll(ref EntityCommandBuffer.ParallelWriter parallelWriter, in int sortKey, in Entity entity)
            {
                parallelWriter.AppendToBuffer<TweenPauseInfo>(sortKey, entity, new TweenPauseInfo());
                parallelWriter.AddComponent<TweenPauseCommand>(sortKey, entity);
            }

            [BurstCompile]
            public static void ResumeAll(ref EntityManager entityManager, in Entity entity)
            {
                entityManager.GetBuffer<TweenResumeInfo>(entity).Add(new TweenResumeInfo());
                entityManager.AddComponent<TweenResumeCommand>(entity);
            }

            [BurstCompile]
            public static void ResumeAll(ref EntityCommandBuffer commandBuffer, in Entity entity)
            {
                commandBuffer.AppendToBuffer<TweenResumeInfo>(entity, new TweenResumeInfo());
                commandBuffer.AddComponent<TweenResumeCommand>(entity);
            }

            [BurstCompile]
            public static void ResumeAll(ref EntityCommandBuffer.ParallelWriter parallelWriter, in int sortKey, in Entity entity)
            {
                parallelWriter.AppendToBuffer<TweenResumeInfo>(sortKey, entity, new TweenResumeInfo());
                parallelWriter.AddComponent<TweenResumeCommand>(sortKey, entity);
            }

            [BurstCompile]
            public static void StopAll(ref EntityManager entityManager, in Entity entity)
            {
                if (!entityManager.HasBuffer<TweenState>(entity)) return;
                var tweenBuffer = entityManager.GetBuffer<TweenState>(entity);
                foreach (var tweenState in tweenBuffer)
                {
                    Stop(ref entityManager, entity, tweenState.Id);
                }
            }

            [BurstCompile]
            public static void StopAll(ref EntityCommandBuffer commandBuffer, in Entity entity)
            {
                commandBuffer.AddComponent<TweenStopCommand>(entity);
            }

            [BurstCompile]
            public static void StopAll(ref EntityCommandBuffer.ParallelWriter parallelWriter, in int sortKey, in Entity entity)
            {
                parallelWriter.AddComponent<TweenStopCommand>(sortKey, entity);
            }

#endregion
        }
    }
}