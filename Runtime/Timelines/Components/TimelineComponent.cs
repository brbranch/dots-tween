using System.Runtime.InteropServices;
using DotsTween.Tweens;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;

namespace DotsTween.Timelines
{
    [BurstCompile]
    public struct TimelineComponent : IComponentData, INativeDisposable
    {
        internal uint PlaybackId;
        internal float Duration;
        internal float StartDelay;
        internal float LoopDelay;
        internal int LoopCount;
        
        internal NativeList<TimelineComponentOperationTuple> OnStart;
        internal NativeList<TimelineComponentOperationTuple> OnComplete;
        private NativeHashSet<uint> activeElementIds;
        private NativeList<ComponentType> timelineElementTypes;
        private UnsafeAppendBuffer timelineElements;
        
        internal float CurrentTime;
        [MarshalAs(UnmanagedType.U1)] internal bool IsPlaying;
        
        public float DurationWithLoopDelay => Duration + LoopDelay;
        public int Size => timelineElements.Length;

        internal TimelineComponent(float startDelay = 0f)
        {
            CurrentTime = 0;
            IsPlaying = false;
            PlaybackId = 0;
            Duration = 0;
            StartDelay = 0;
            LoopDelay = 0;
            LoopCount = 0;
            
            OnStart = new NativeList<TimelineComponentOperationTuple>(Allocator.Persistent);
            OnComplete = new NativeList<TimelineComponentOperationTuple>(Allocator.Persistent);
            activeElementIds = new NativeHashSet<uint>(1, Allocator.Persistent);
            timelineElementTypes = new NativeList<ComponentType>(Allocator.Persistent);
            timelineElements = new UnsafeAppendBuffer(1, 4, Allocator.Persistent);
        }

        [BurstCompile]
        public void Insert<T>(float atPosition, in Entity target, in T command) where T : unmanaged, IComponentData, ITweenParams
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
            tweenParams.SetTimelinePositions(atPosition, atPosition + totalDuration);

            if (tweenParams.TimelineEndPosition > Duration)
            {
                Duration = tweenParams.TimelineEndPosition;
            }

            command.SetTweenParams(tweenParams);
            timelineElements.Add(new TimelineElement<T>(target, atPosition, tweenParams.TimelineEndPosition, command));
            timelineElementTypes.Add(ComponentType.ReadOnly<T>());
        }
        
        [BurstCompile]
        public void Append<T>(in Entity target, in T command) where T : unmanaged, IComponentData, ITweenParams
        {
            Insert(Duration, target, command);
        }

        [BurstCompile]
        public void AddOnComplete(in ComponentOperations onComplete, in Entity target)
        {
            OnComplete.Add(new TimelineComponentOperationTuple
            {
                Operations = onComplete,
                Target = target,
            });
        }

        /// <summary>
        /// Component Operations only occur AFTER a Timeline's StartDelay.
        /// </summary>
        /// <param name="onStart"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        [BurstCompile]
        public void AddOnStart(in ComponentOperations onStart, in Entity target)
        {
            OnStart.Add(new TimelineComponentOperationTuple
            {
                Operations = onStart,
                Target = target,
            });
        }

        /// <summary>
        /// Delays playback of a Timeline. Only occurs for the first playback, looping playbacks will skip this.
        /// </summary>
        /// <param name="delay"></param>
        /// <returns></returns>
        [BurstCompile]
        public void SetStartDelay(float delay)
        {
            StartDelay = delay;
            CurrentTime = -delay;
        }

        /// <summary>
        /// Adds a delay to the end of a Timeline before looping
        /// </summary>
        /// <param name="delay"></param>
        /// <returns></returns>
        [BurstCompile]
        public void SetLoopDelay(float delay)
        {
            LoopDelay = delay;
        }

        /// <summary>
        /// Set to less than 0 for Infinite Looping.
        /// </summary>
        /// <param name="loopCount"></param>
        /// <returns></returns>
        [BurstCompile]
        public void SetLoopCount(int loopCount)
        {
            LoopCount = loopCount;
        }

        /// <summary>
        /// !! NOTE: Causes a structural change if not careful.
        /// Uses state.EntityManager to create the Entity and immediately adds the required components.
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        [BurstCompile]
        public uint Play(ref SystemState state)
        {
            var e = state.EntityManager.CreateEntity();
            
            SetupPlaybackId(ref e, state.WorldUnmanaged.Time.ElapsedTime.GetHashCode());
            state.EntityManager.AddComponentData(e, this);
            return PlaybackId;
        }

        /// <summary>
        /// Uses the entityCommandBuffer to create a deferred timeline entity. No structural changes.
        /// [Optional] Provide elapsed time for better timeline id generation. eg: SystemAPI.Time.ElapsedTime
        /// </summary>
        /// <param name="entityCommandBuffer"></param>
        /// <param name="elapsedTime"></param>
        /// <returns></returns>
        [BurstCompile]
        public uint Play(ref EntityCommandBuffer entityCommandBuffer, double elapsedTime = 0d)
        {
            var e = entityCommandBuffer.CreateEntity();
            
            SetupPlaybackId(ref e, elapsedTime.GetHashCode());
            entityCommandBuffer.AddComponent(e, this);
            return PlaybackId;
        }
        
        /// <summary>
        /// Uses EntityManager to create the Entity, but uses the entityCommandBuffer to add the required components in attempts to avoid structural changes.
        /// </summary>
        /// <param name="entityManager"></param>
        /// <param name="entityCommandBuffer"></param>
        /// <returns></returns>
        [BurstCompile]
        public uint Play(EntityManager entityManager, ref EntityCommandBuffer entityCommandBuffer)
        {
            var e = entityManager.CreateEntity();
            SetupPlaybackId(ref e, entityManager.WorldUnmanaged.Time.ElapsedTime.GetHashCode());
            entityCommandBuffer.AddComponent(e, this);
            return PlaybackId;
        }

        [BurstCompile]
        public uint Play(ref EntityCommandBuffer.ParallelWriter parallelWriter, in int sortKey)
        {
            var e = parallelWriter.CreateEntity(sortKey);
            
            SetupPlaybackId(ref e, sortKey);
            parallelWriter.AddComponent(sortKey, e, this);
            return PlaybackId;
        }

        [BurstCompile]
        public uint Play(EntityManager entityManager, ref EntityCommandBuffer.ParallelWriter parallelWriter, in int sortKey)
        {
            var e = entityManager.CreateEntity();
            
            SetupPlaybackId(ref e, sortKey ^ entityManager.WorldUnmanaged.Time.ElapsedTime.GetHashCode());
            parallelWriter.AddComponent(sortKey, e, this);
            return PlaybackId;
        }

        [BurstCompile]
        public void Dispose()
        {
            OnStart.Dispose();
            OnComplete.Dispose();
            activeElementIds.Dispose();
            timelineElementTypes.Dispose();
            timelineElements.Dispose();
        }

        [BurstCompile]
        public JobHandle Dispose(JobHandle inputDeps)
        {
            NativeArray<JobHandle> disposeJobs = new NativeArray<JobHandle>(5, Allocator.TempJob);
            disposeJobs[0] = OnStart.Dispose(inputDeps);
            disposeJobs[1] = OnComplete.Dispose(inputDeps);
            disposeJobs[2] = activeElementIds.Dispose(inputDeps);
            disposeJobs[3] = timelineElementTypes.Dispose(inputDeps);
            disposeJobs[4] = timelineElements.Dispose(inputDeps);
            return JobHandle.CombineDependencies(disposeJobs);
        }

        [BurstCompile]
        internal void SetupPlaybackId(ref Entity e, in int extraHash)
        {
            unchecked
            {
                int hashCode = extraHash.GetHashCode();
                hashCode = (hashCode * 421) ^ Size.GetHashCode();
                hashCode = (hashCode * 421) ^ Duration.GetHashCode();
                hashCode = (hashCode * 421) ^ LoopCount.GetHashCode();
                hashCode = (hashCode * 421) ^ DurationWithLoopDelay.GetHashCode();
                hashCode = (hashCode * 421) ^ StartDelay.GetHashCode();
                hashCode = (hashCode * 421) ^ e.Index.GetHashCode();
                hashCode = (hashCode * 421) ^ e.Version.GetHashCode();

                foreach (var type in timelineElementTypes)
                {
                    hashCode = (hashCode * 421) ^ type.GetHashCode();
                }
                
                PlaybackId = (uint)hashCode;
            }
        }

        internal void AddTimelineElementIdToActive(uint timelineElementId) => activeElementIds.Add(timelineElementId);
        internal void RemoveTimelineElementIdFromActive(uint timelineElementId) => activeElementIds.Remove(timelineElementId);
        internal bool IsTimelineElementActive(uint timelineElementId) => activeElementIds.Contains(timelineElementId);
        internal UnsafeAppendBuffer.Reader GetTimelineReader() => timelineElements.AsReader();
        internal ComponentType GetTimelineElementType(int index) => timelineElementTypes[index];
    }
}