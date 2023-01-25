using System.Runtime.CompilerServices;
using DotsTween.Math;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[assembly: InternalsVisibleTo("Timespawn.EntityTween.Tests")]

namespace DotsTween.Tweens
{
    public static class Tween
    {
        public const byte Infinite = TweenState.LOOP_COUNT_INFINITE;

        #region Move
        public static void Move(
            in EntityManager entityManager,
            in Entity entity,
            in float3 start,
            in float3 end,
            in TweenParams tweenParams)
        {
            Move(entityManager, entity, start, end, tweenParams.Duration, tweenParams.EaseType, tweenParams.IsPingPong, tweenParams.LoopCount, tweenParams.StartDelay);
        }

        public static void Move(
            in EntityCommandBuffer commandBuffer,
            in Entity entity,
            in float3 start,
            in float3 end,
            in TweenParams tweenParams)
        {
            Move(commandBuffer, entity, start, end, tweenParams.Duration, EaseType.Linear, tweenParams.IsPingPong, tweenParams.LoopCount, tweenParams.StartDelay);
        }

        public static void Move(
            in EntityCommandBuffer.ParallelWriter parallelWriter,
            in int sortKey,
            in Entity entity,
            in float3 start,
            in float3 end,
            in TweenParams tweenParams)
        {
            Move(parallelWriter, sortKey, entity, start, end, tweenParams.Duration, tweenParams.EaseType, tweenParams.IsPingPong, tweenParams.LoopCount, tweenParams.StartDelay);
        }

        public static void Move(
            in EntityManager entityManager,
            in Entity entity,
            in float3 start,
            in float3 end,
            in float duration,
            in EaseType easeType = EaseType.Linear,
            in bool isPingPong = false,
            in int loopCount = 1,
            in float startDelay = 0.0f)
        {
            if (!CheckParams(loopCount))
            {
                return;
            }

            TweenParams tweenParams = new TweenParams(duration, easeType, isPingPong, loopCount, startDelay);
            entityManager.AddComponentData(entity, new TweenTranslationCommand(tweenParams, start, end));
        }

        public static void Move(
            in EntityCommandBuffer commandBuffer,
            in Entity entity,
            in float3 start,
            in float3 end,
            in float duration,
            in EaseType easeType = EaseType.Linear,
            in bool isPingPong = false,
            in int loopCount = 1,
            in float startDelay = 0.0f)
        {
            if (!CheckParams(loopCount))
            {
                return;
            }

            TweenParams tweenParams = new TweenParams(duration, easeType, isPingPong, loopCount, startDelay);
            commandBuffer.AddComponent(entity, new TweenTranslationCommand(tweenParams, start, end));
        }

        public static void Move(
            in EntityCommandBuffer.ParallelWriter parallelWriter,
            in int sortKey,
            in Entity entity,
            in float3 start,
            in float3 end,
            in float duration,
            in EaseType easeType = EaseType.Linear,
            bool isPingPong = false,
            in int loopCount = 1,
            in float startDelay = 0.0f)
        {
            if (!CheckParams(loopCount))
            {
                return;
            }

            TweenParams tweenParams = new TweenParams(duration, easeType, isPingPong, loopCount, startDelay);
            parallelWriter.AddComponent(sortKey, entity, new TweenTranslationCommand(tweenParams, start, end));
        }
#endregion
        
        #region Rotate
        public static void Rotate(
            in EntityManager entityManager,
            in Entity entity,
            in quaternion start,
            in quaternion end,
            in TweenParams tweenParams)
        {
            Rotate(entityManager, entity, start, end, tweenParams.Duration, EaseType.Linear, tweenParams.IsPingPong, tweenParams.LoopCount, tweenParams.StartDelay);
        }

        public static void Rotate(
            in EntityCommandBuffer commandBuffer,
            in Entity entity,
            in quaternion start,
            in quaternion end,
            in TweenParams tweenParams)
        {
            Rotate(commandBuffer, entity, start, end, tweenParams.Duration, EaseType.Linear, tweenParams.IsPingPong, tweenParams.LoopCount, tweenParams.StartDelay);
        }

        public static void Rotate(
            in EntityCommandBuffer.ParallelWriter parallelWriter,
            in int sortKey,
            in Entity entity,
            in quaternion start,
            in quaternion end,
            in TweenParams tweenParams)
        {
            Rotate(parallelWriter, sortKey, entity, start, end, tweenParams.Duration, EaseType.Linear, tweenParams.IsPingPong, tweenParams.LoopCount, tweenParams.StartDelay);
        }

        public static void Rotate(
            in EntityManager entityManager,
            in Entity entity,
            in quaternion start,
            in quaternion end,
            in float duration,
            in EaseType easeType = EaseType.Linear,
            in bool isPingPong = false,
            in int loopCount = 1,
            in float startDelay = 0.0f)
        {
            if (!CheckParams(loopCount))
            {
                return;
            }

            TweenParams tweenParams = new TweenParams(duration, easeType, isPingPong, loopCount, startDelay);
            entityManager.AddComponentData(entity, new TweenRotationCommand(tweenParams, start, end));
        }

        public static void Rotate(
            in EntityCommandBuffer commandBuffer,
            in Entity entity,
            in quaternion start,
            in quaternion end,
            in float duration,
            in EaseType easeType = EaseType.Linear,
            in bool isPingPong = false,
            in int loopCount = 1,
            in float startDelay = 0.0f)
        {
            if (!CheckParams(loopCount))
            {
                return;
            }

            TweenParams tweenParams = new TweenParams(duration, easeType, isPingPong, loopCount, startDelay);
            commandBuffer.AddComponent(entity, new TweenRotationCommand(tweenParams, start, end));
        }

        public static void Rotate(
            in EntityCommandBuffer.ParallelWriter parallelWriter,
            in int sortKey,
            in Entity entity,
            in quaternion start,
            in quaternion end,
            in float duration,
            in EaseType easeType = EaseType.Linear,
            in bool isPingPong = false,
            in int loopCount = 1,
            in float startDelay = 0.0f)
        {
            if (!CheckParams(loopCount))
            {
                return;
            }

            TweenParams tweenParams = new TweenParams(duration, easeType, isPingPong, loopCount, startDelay);
            parallelWriter.AddComponent(sortKey, entity, new TweenRotationCommand(tweenParams, start, end));
        }
#endregion

        #region Scale
        public static void Scale(
            in EntityManager entityManager,
            in Entity entity,
            in float start,
            in float end,
            in TweenParams tweenParams)
        {
            Scale(entityManager, entity, start, end, tweenParams.Duration, EaseType.Linear, tweenParams.IsPingPong, tweenParams.LoopCount, tweenParams.StartDelay);
        }

        public static void Scale(
            in EntityCommandBuffer commandBuffer,
            in Entity entity,
            in float start,
            in float end,
            in TweenParams tweenParams)
        {
            Scale(commandBuffer, entity, start, end, tweenParams.Duration, EaseType.Linear, tweenParams.IsPingPong, tweenParams.LoopCount, tweenParams.StartDelay);
        }

        public static void Scale(
            in EntityCommandBuffer.ParallelWriter parallelWriter,
            in int sortKey,
            in Entity entity,
            in float start,
            in float end,
            in TweenParams tweenParams)
        {
            Scale(parallelWriter, sortKey, entity, start, end, tweenParams.Duration, EaseType.Linear, tweenParams.IsPingPong, tweenParams.LoopCount, tweenParams.StartDelay);
        }

        public static void Scale(
            in EntityManager entityManager,
            in Entity entity,
            in float start,
            in float end,
            in float duration,
            in EaseType easeType = EaseType.Linear,
            in bool isPingPong = false,
            in int loopCount = 1,
            in float startDelay = 0.0f)
        {
            if (!CheckParams(loopCount))
            {
                return;
            }

            TweenParams tweenParams = new TweenParams(duration, easeType, isPingPong, loopCount, startDelay);
            entityManager.AddComponentData(entity, new TweenScaleCommand(tweenParams, start, end));
        }

        public static void Scale(
            in EntityCommandBuffer commandBuffer,
            in Entity entity,
            in float start,
            in float end,
            in float duration,
            in EaseType easeType = EaseType.Linear,
            in bool isPingPong = false,
            in int loopCount = 1,
            in float startDelay = 0.0f)
        {
            if (!CheckParams(loopCount))
            {
                return;
            }

            TweenParams tweenParams = new TweenParams(duration, easeType, isPingPong, loopCount, startDelay);
            commandBuffer.AddComponent(entity, new TweenScaleCommand(tweenParams, start, end));
        }

        public static void Scale(
            in EntityCommandBuffer.ParallelWriter parallelWriter,
            in int sortKey,
            in Entity entity,
            in float start,
            in float end,
            in float duration,
            in EaseType easeType = EaseType.Linear,
            in bool isPingPong = false,
            in int loopCount = 1,
            in float startDelay = 0.0f)
        {
            if (!CheckParams(loopCount))
            {
                return;
            }

            TweenParams tweenParams = new TweenParams(duration, easeType, isPingPong, loopCount, startDelay);
            parallelWriter.AddComponent(sortKey, entity, new TweenScaleCommand(tweenParams, start, end));
        }
        #endregion
        
        #region NonUniformScale
        public static void NonUniformScale(
            in EntityManager entityManager,
            in Entity entity,
            in float3 start,
            in float3 end,
            in TweenParams tweenParams)
        {
            NonUniformScale(entityManager, entity, start, end, tweenParams.Duration, EaseType.Linear, tweenParams.IsPingPong, tweenParams.LoopCount, tweenParams.StartDelay);
        }

        public static void NonUniformScale(
            in EntityCommandBuffer commandBuffer,
            in Entity entity,
            in float3 start,
            in float3 end,
            in TweenParams tweenParams)
        {
            NonUniformScale(commandBuffer, entity, start, end, tweenParams.Duration, EaseType.Linear, tweenParams.IsPingPong, tweenParams.LoopCount, tweenParams.StartDelay);
        }

        public static void NonUniformScale(
            in EntityCommandBuffer.ParallelWriter parallelWriter,
            in int sortKey,
            in Entity entity,
            in float3 start,
            in float3 end,
            in TweenParams tweenParams)
        {
            NonUniformScale(parallelWriter, sortKey, entity, start, end, tweenParams.Duration, EaseType.Linear, tweenParams.IsPingPong, tweenParams.LoopCount, tweenParams.StartDelay);
        }

        public static void NonUniformScale(
            in EntityManager entityManager,
            in Entity entity,
            in float3 start,
            in float3 end,
            in float duration,
            in EaseType easeType = EaseType.Linear,
            in bool isPingPong = false,
            in int loopCount = 1,
            in float startDelay = 0.0f)
        {
            if (!CheckParams(loopCount))
            {
                return;
            }

            TweenParams tweenParams = new TweenParams(duration, easeType, isPingPong, loopCount, startDelay);
            entityManager.AddComponentData(entity, new TweenNonUniformScaleCommand(tweenParams, start, end));
        }

        public static void NonUniformScale(
            in EntityCommandBuffer commandBuffer,
            in Entity entity,
            in float3 start,
            in float3 end,
            in float duration,
            in EaseType easeType = EaseType.Linear,
            in bool isPingPong = false,
            in int loopCount = 1,
            in float startDelay = 0.0f)
        {
            if (!CheckParams(loopCount))
            {
                return;
            }

            TweenParams tweenParams = new TweenParams(duration, easeType, isPingPong, loopCount, startDelay);
            commandBuffer.AddComponent(entity, new TweenNonUniformScaleCommand(tweenParams, start, end));
        }

        public static void NonUniformScale(
            in EntityCommandBuffer.ParallelWriter parallelWriter,
            in int sortKey,
            in Entity entity,
            in float3 start,
            in float3 end,
            in float duration,
            in EaseType easeType = EaseType.Linear,
            in bool isPingPong = false,
            in int loopCount = 1,
            in float startDelay = 0.0f)
        {
            if (!CheckParams(loopCount))
            {
                return;
            }

            TweenParams tweenParams = new TweenParams(duration, easeType, isPingPong, loopCount, startDelay);
            parallelWriter.AddComponent(sortKey, entity, new TweenNonUniformScaleCommand(tweenParams, start, end));
        }
        #endregion
        
        #region Tint
        public static void Tint(
            in EntityManager entityManager,
            in Entity entity,
            in Color start,
            in Color end,
            in TweenParams tweenParams)
        {
            Tint(entityManager, entity, start, end, tweenParams.Duration, EaseType.Linear, tweenParams.IsPingPong, tweenParams.LoopCount, tweenParams.StartDelay);
        }

        public static void Tint(
            in EntityCommandBuffer commandBuffer,
            in Entity entity,
            in Color start,
            in Color end,
            in TweenParams tweenParams)
        {
            Tint(commandBuffer, entity, start, end, tweenParams.Duration, EaseType.Linear, tweenParams.IsPingPong, tweenParams.LoopCount, tweenParams.StartDelay);
        }

        public static void Tint(
            in EntityCommandBuffer.ParallelWriter parallelWriter,
            in int sortKey,
            in Entity entity,
            in Color start,
            in Color end,
            in TweenParams tweenParams)
        {
            Tint(parallelWriter, sortKey, entity, start, end, tweenParams.Duration, EaseType.Linear, tweenParams.IsPingPong, tweenParams.LoopCount, tweenParams.StartDelay);
        }

        public static void Tint(
            in EntityManager entityManager,
            in Entity entity,
            in Color start,
            in Color end,
            in float duration,
            in EaseType easeType = EaseType.Linear,
            in bool isPingPong = false,
            in int loopCount = 1,
            in float startDelay = 0.0f)
        {
            if (!CheckParams(loopCount))
            {
                return;
            }

            TweenParams tweenParams = new TweenParams(duration, easeType, isPingPong, loopCount, startDelay);
            entityManager.AddComponentData(entity, new TweenTintCommand(tweenParams, start.ToFloat4(), end.ToFloat4()));
        }

        public static void Tint(
            in EntityCommandBuffer commandBuffer,
            in Entity entity,
            in Color start,
            in Color end,
            in float duration,
            in EaseType easeType = EaseType.Linear,
            in bool isPingPong = false,
            in int loopCount = 1,
            in float startDelay = 0.0f)
        {
            if (!CheckParams(loopCount))
            {
                return;
            }

            TweenParams tweenParams = new TweenParams(duration, easeType, isPingPong, loopCount, startDelay);
            commandBuffer.AddComponent(entity, new TweenTintCommand(tweenParams, start.ToFloat4(), end.ToFloat4()));
        }

        public static void Tint(
            in EntityCommandBuffer.ParallelWriter parallelWriter,
            in int sortKey,
            in Entity entity,
            in Color start,
            in Color end,
            in float duration,
            in EaseType easeType = EaseType.Linear,
            in bool isPingPong = false,
            in int loopCount = 1,
            in float startDelay = 0.0f)
        {
            if (!CheckParams(loopCount))
            {
                return;
            }

            TweenParams tweenParams = new TweenParams(duration, easeType, isPingPong, loopCount, startDelay);
            parallelWriter.AddComponent(sortKey, entity, new TweenTintCommand(tweenParams, start.ToFloat4(), end.ToFloat4()));
        }
        #endregion
        
        #region Controls
        public static void Pause(in EntityManager entityManager, in Entity entity)
        {
            entityManager.AddComponent<TweenPause>(entity);
        }

        public static void Pause(in EntityCommandBuffer commandBuffer, in Entity entity)
        {
            commandBuffer.AddComponent<TweenPause>(entity);
        }

        public static void Pause(in EntityCommandBuffer.ParallelWriter parallelWriter, in int sortKey, in Entity entity)
        {
            parallelWriter.AddComponent<TweenPause>(sortKey, entity);
        }

        public static void Resume(in EntityManager entityManager, in Entity entity)
        {
            entityManager.AddComponent<TweenResumeCommand>(entity);
        }

        public static void Resume(in EntityCommandBuffer commandBuffer, in Entity entity)
        {
            commandBuffer.AddComponent<TweenResumeCommand>(entity);
        }

        public static void Resume(in EntityCommandBuffer.ParallelWriter parallelWriter, in int sortKey, in Entity entity)
        {
            parallelWriter.AddComponent<TweenResumeCommand>(sortKey, entity);
        }

        public static void Stop(in EntityManager entityManager, in Entity entity)
        {
            entityManager.AddComponent<TweenStopCommand>(entity);
        }

        public static void Stop(in EntityCommandBuffer commandBuffer, in Entity entity)
        {
            commandBuffer.AddComponent<TweenStopCommand>(entity);
        }

        public static void Stop(in EntityCommandBuffer.ParallelWriter parallelWriter, in int sortKey, in Entity entity)
        {
            parallelWriter.AddComponent<TweenStopCommand>(sortKey, entity);
        }

        private static bool CheckParams(in int loopCount)
        {
            return loopCount is >= byte.MinValue and <= byte.MaxValue;
        }
        #endregion
    }
}