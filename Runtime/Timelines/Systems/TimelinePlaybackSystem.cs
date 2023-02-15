using System.Collections.Generic;
using DotsTween.Tweens;
using Unity.Entities;

namespace DotsTween.Timelines.Systems
{
    [UpdateInGroup(typeof(TimelineSimulationSystemGroup))]
    public partial class TimelinePlaybackSystem : SystemBase
    {
        protected override void OnCreate()
        {
            RequireForUpdate<TimelineComponent>();
        }

        protected override void OnUpdate()
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            var ecb = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(CheckedStateRef.WorldUnmanaged);
            
            foreach (var (timeline, entity) in SystemAPI.Query<TimelineComponent>().WithNone<TimelinePausedTag>().WithEntityAccess())
            {
                timeline.CurrentTime += deltaTime;
                
                // Perform Component operations after the initial Timeline Delay (if any)
                if (!timeline.IsPlaying && timeline.CurrentTime >= 0f)
                {
                    timeline.IsPlaying = true;
                    PerformComponentOperations(ref ecb, timeline.OnStart);
                }

                foreach (var timelineElement in timeline.TimelineElements)
                {
                    TryToStartPlayingTimelineElement(timelineElement, timeline, ref ecb);
                }

                TryToDestroyTimeline(timeline, entity, ref ecb);
            }
        }

        private void TryToDestroyTimeline(TimelineComponent timeline, Entity entity, ref EntityCommandBuffer ecb)
        {
            if (timeline.CurrentTime < timeline.DurationWithLoopDelay) return;

            if (timeline.LoopCount == 0)
            {
                PerformComponentOperations(ref ecb, timeline.OnComplete);
                return;
            }

            if (timeline.LoopCount > 0)
            {
                --timeline.LoopCount;
            }

            timeline.CurrentTime = 0f;
        }

        private void TryToStartPlayingTimelineElement(TimelineElement timelineElement, TimelineComponent timeline, ref EntityCommandBuffer ecb)
        {
            if (timelineElement.StartTime <= timeline.CurrentTime && !timeline.ActiveElements.Contains(timelineElement))
            {
                timeline.ActiveElements.Add(timelineElement);

                var command = timelineElement.Command;

                if (command is TweenTranslationCommand translationCommand) ecb.AddComponent<TweenTranslationCommand>(timelineElement.Target, translationCommand);
                else if (command is TweenScaleCommand tweenScaleCommand) ecb.AddComponent<TweenScaleCommand>(timelineElement.Target, tweenScaleCommand);
                else if (command is TweenRotationCommand rotationCommand) ecb.AddComponent<TweenRotationCommand>(timelineElement.Target, rotationCommand);
                else if (command is TweenNonUniformScaleCommand scaleCommand) ecb.AddComponent<TweenNonUniformScaleCommand>(timelineElement.Target, scaleCommand);
                else if (command is TweenURPTintCommand tintCommand) ecb.AddComponent<TweenURPTintCommand>(timelineElement.Target, tintCommand);
            }
        }
        
        private void PerformComponentOperations(ref EntityCommandBuffer ecb, in List<(Entity, ComponentOperations)> operations)
        {
            foreach (var (target, operation) in operations)
            {
                operation.Perform(ref ecb, target);
            }
        }
    }
}