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
            #region ControlSpecific
            [BurstCompile]
            public static void Pause(ref EntityManager entityManager, in Entity entity, in int tweenId)
            {
                entityManager.AddComponent<TweenPause>(entity);
            }

            [BurstCompile]
            public static void Pause(ref EntityCommandBuffer commandBuffer, in Entity entity, in int tweenId)
            {
                commandBuffer.AddComponent<TweenPause>(entity);
            }

            [BurstCompile]
            public static void Pause(ref EntityCommandBuffer.ParallelWriter parallelWriter, in int sortKey, in Entity entity, in int tweenId)
            {
                parallelWriter.AddComponent<TweenPause>(sortKey, entity);
            }

            [BurstCompile]
            public static void Resume(ref EntityManager entityManager, in Entity entity, in int tweenId)
            {
                entityManager.AddComponent<TweenResumeCommand>(entity);
            }

            [BurstCompile]
            public static void Resume(ref EntityCommandBuffer commandBuffer, in Entity entity, in int tweenId)
            {
                commandBuffer.AddComponent<TweenResumeCommand>(entity);
            }

            [BurstCompile]
            public static void Resume(ref EntityCommandBuffer.ParallelWriter parallelWriter, in int sortKey, in Entity entity, in int tweenId)
            {
                parallelWriter.AddComponent<TweenResumeCommand>(sortKey, entity);
            }

            [BurstCompile]
            public static void Stop(ref EntityManager entityManager, in Entity entity, in int tweenId)
            {
                entityManager.AddComponent<TweenStopCommand>(entity);
            }

            [BurstCompile]
            public static void Stop(ref EntityCommandBuffer commandBuffer, in Entity entity, in int tweenId)
            {
                commandBuffer.AddComponent<TweenStopCommand>(entity);
            }

            [BurstCompile]
            public static void Stop(ref EntityCommandBuffer.ParallelWriter parallelWriter, in int sortKey, in Entity entity, in int tweenId)
            {
                parallelWriter.AddComponent<TweenStopCommand>(sortKey, entity);
            }
            #endregion
            
            #region Control All
            [BurstCompile]
            public static void PauseAll(ref EntityManager entityManager, in Entity entity)
            {
                entityManager.AddComponent<TweenPause>(entity);
            }

            [BurstCompile]
            public static void PauseAll(ref EntityCommandBuffer commandBuffer, in Entity entity)
            {
                commandBuffer.AddComponent<TweenPause>(entity);
            }

            [BurstCompile]
            public static void PauseAll(ref EntityCommandBuffer.ParallelWriter parallelWriter, in int sortKey, in Entity entity)
            {
                parallelWriter.AddComponent<TweenPause>(sortKey, entity);
            }

            [BurstCompile]
            public static void ResumeAll(ref EntityManager entityManager, in Entity entity)
            {
                entityManager.AddComponent<TweenResumeCommand>(entity);
            }

            [BurstCompile]
            public static void ResumeAll(ref EntityCommandBuffer commandBuffer, in Entity entity)
            {
                commandBuffer.AddComponent<TweenResumeCommand>(entity);
            }

            [BurstCompile]
            public static void ResumeAll(ref EntityCommandBuffer.ParallelWriter parallelWriter, in int sortKey, in Entity entity)
            {
                parallelWriter.AddComponent<TweenResumeCommand>(sortKey, entity);
            }

            [BurstCompile]
            public static void StopAll(ref EntityManager entityManager, in Entity entity)
            {
                entityManager.AddComponent<TweenStopCommand>(entity);
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