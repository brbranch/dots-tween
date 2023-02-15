using System.Collections.Generic;
using DotsTween.Tweens;
using Unity.Entities;
using UnityEngine;

namespace DotsTween.Timelines
{
    public class TimelineComponent : IComponentData
    {
        public int PlaybackId { get; private set; }
        public float Duration { get; private set; }
        public float StartDelay { get; private set; }
        public float LoopDelay { get; private set; }
        public int LoopCount { get; internal set; }
        
        internal readonly List<(Entity, ComponentOperations)> OnStart = new();
        internal readonly List<(Entity, ComponentOperations)> OnComplete = new();
        internal readonly List<TimelineElement> TimelineElements = new();
        internal readonly List<TimelineElement> ActiveElements = new();
        
        internal float CurrentTime;
        internal bool IsPlaying;
        
        public float DurationWithLoopDelay => Duration + LoopDelay;

        public TimelineComponent Insert<T>(float atPosition, Entity target, T command) where T : IComponentData, ITweenParams
        {
            TweenParams tweenParams = command.GetTweenParams();
            atPosition += tweenParams.StartDelay;

            if (tweenParams.LoopCount < 0)
            {
                tweenParams.LoopCount = short.MaxValue - 1;
                Debug.LogError("Infinite Loops aren't allowed in Timelines. Changing to short.MaxValue - 1.");
            }

            float totalDuration = tweenParams.Duration * (tweenParams.LoopCount + 1); // +1 for first non-loop playback
            tweenParams.StartDelay = 0;
            tweenParams.SetTimelinePositions(atPosition, totalDuration);

            if (tweenParams.TimelineEndPosition > Duration)
            {
                Duration = tweenParams.TimelineEndPosition;
            }

            command.SetTweenParams(tweenParams);
            TimelineElements.Add(new TimelineElement
            {
                Target = target,
                Command = command,
                StartTime = atPosition,
                EndTime = tweenParams.TimelineEndPosition,
            });
            return this;
        }
        
        public TimelineComponent Append<T>(Entity target, T command) where T : IComponentData, ITweenParams
        {
            return Insert(Duration, target, command);
        }

        public TimelineComponent AddOnComplete(ComponentOperations onComplete, Entity target)
        {
            OnComplete.Add((target, onComplete));
            return this;
        }

        /// <summary>
        /// Component Operations only occur AFTER a Timeline's StartDelay.
        /// </summary>
        /// <param name="onStart"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public TimelineComponent AddOnStart(ComponentOperations onStart, Entity target)
        {
            OnStart.Add((target, onStart));
            return this;
        }

        /// <summary>
        /// Delays playback of a Timeline. Only occurs for the first playback, looping playbacks will skip this.
        /// </summary>
        /// <param name="delay"></param>
        /// <returns></returns>
        public TimelineComponent SetStartDelay(float delay)
        {
            StartDelay = delay;
            CurrentTime = -delay;
            return this;
        }

        /// <summary>
        /// Adds a delay to the end of a Timeline before looping
        /// </summary>
        /// <param name="delay"></param>
        /// <returns></returns>
        public TimelineComponent SetLoopDelay(float delay)
        {
            LoopDelay = delay;
            return this;
        }

        /// <summary>
        /// Set to less than 0 for Infinite Looping.
        /// </summary>
        /// <param name="loopCount"></param>
        /// <returns></returns>
        public TimelineComponent SetLoopCount(int loopCount)
        {
            LoopCount = loopCount;
            return this;
        }

        /// <summary>
        /// !! NOTE: Causes a structural change if not careful.
        /// Uses state.EntityManager to create the Entity and immediately adds the required components.
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public int Play(ref SystemState state)
        {
            var e = state.EntityManager.CreateEntity();
            
            SetupPlaybackId(ref e, state.WorldUnmanaged.Time.ElapsedTime.GetHashCode());
            state.EntityManager.AddComponentData(e, this);
            return PlaybackId;
        }

        /// <summary>
        /// Uses the entityCommandBuffer to create a deferred timeline entity. No structural changes.
        /// </summary>
        /// <param name="entityCommandBuffer"></param>
        /// /// <returns></returns>
        public int Play(ref EntityCommandBuffer entityCommandBuffer)
        {
            var e = entityCommandBuffer.CreateEntity();
            
            SetupPlaybackId(ref e, entityCommandBuffer.GetHashCode());
            entityCommandBuffer.AddComponent(e, this);
            return PlaybackId;
        }
        
        /// <summary>
        /// Uses EntityManager to create the Entity, but uses the entityCommandBuffer to add the required components in attempts to avoid structural changes.
        /// </summary>
        /// <param name="entityManager"></param>
        /// <param name="entityCommandBuffer"></param>
        /// <returns></returns>
        public int Play(EntityManager entityManager, ref EntityCommandBuffer entityCommandBuffer)
        {
            var e = entityCommandBuffer.CreateEntity();
            SetupPlaybackId(ref e, entityManager.GetHashCode());
            entityCommandBuffer.AddComponent(e, this);
            return PlaybackId;
        }

        private void SetupPlaybackId(ref Entity e, in int extraHash)
        {
            unchecked
            {
                int hashCode = TimelineElements.GetHashCode();
                hashCode = (hashCode * 421) ^ Duration.GetHashCode();
                hashCode = (hashCode * 421) ^ LoopCount.GetHashCode();
                hashCode = (hashCode * 421) ^ extraHash.GetHashCode();
                hashCode = (hashCode * 421) ^ DurationWithLoopDelay.GetHashCode();
                hashCode = (hashCode * 421) ^ StartDelay.GetHashCode();
                hashCode = (hashCode * 421) ^ e.Index.GetHashCode();
                hashCode = (hashCode * 421) ^ e.Version.GetHashCode();
                PlaybackId = hashCode;
            }
        }
    }
}