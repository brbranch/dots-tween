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
        
        private void PerformComponentOperations(ref EntityCommandBuffer ecb, in List<(Entity, ComponentOperations)> operations)
        {
            foreach (var (target, operation) in operations)
            {
                operation.Perform(ref ecb, target);
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
            if (!(timelineElement.StartTime <= timeline.CurrentTime) || timeline.ActiveElements.Contains(timelineElement)) return;
            
            timeline.ActiveElements.Add(timelineElement);
            var command = timelineElement.Command;

            switch (command)
            {
                case TweenTranslationCommand translationCommand:
                    ecb.AddComponent<TweenTranslationCommand>(timelineElement.Target, translationCommand);
                    break;
                case TweenScaleCommand tweenScaleCommand:
                    ecb.AddComponent<TweenScaleCommand>(timelineElement.Target, tweenScaleCommand);
                    break;
                case TweenRotationCommand rotationCommand:
                    ecb.AddComponent<TweenRotationCommand>(timelineElement.Target, rotationCommand);
                    break;
                case TweenNonUniformScaleCommand scaleCommand:
                    ecb.AddComponent<TweenNonUniformScaleCommand>(timelineElement.Target, scaleCommand);
                    break;
#if DOTS_TWEEN_URP
                case TweenURPTintCommand tint:
                    ecb.AddComponent<TweenURPTintCommand>(timelineElement.Target, tint);
                    break;
                case TweenURPFadeCommand fade:
                    ecb.AddComponent<TweenURPFadeCommand>(timelineElement.Target, fade);
                    break;
                case TweenURPBumpScaleCommand bumpScale:
                    ecb.AddComponent<TweenURPBumpScaleCommand>(timelineElement.Target, bumpScale);
                    break;
                case TweenURPCutoffCommand cutoff:
                    ecb.AddComponent<TweenURPCutoffCommand>(timelineElement.Target, cutoff);
                    break;
                case TweenURPEmissionColorCommand emissionColour:
                    ecb.AddComponent<TweenURPEmissionColorCommand>(timelineElement.Target, emissionColour);
                    break;
                case TweenURPMetallicCommand metallic:
                    ecb.AddComponent<TweenURPMetallicCommand>(timelineElement.Target, metallic);
                    break;
                case TweenURPOcclusionStrengthCommand occlusionStrength:
                    ecb.AddComponent<TweenURPOcclusionStrengthCommand>(timelineElement.Target, occlusionStrength);
                    break;
                case TweenURPSmoothnessCommand smoothness:
                    ecb.AddComponent<TweenURPSmoothnessCommand>(timelineElement.Target, smoothness);
                    break;
                case TweenURPSpecularColorCommand specColour:
                    ecb.AddComponent<TweenURPSpecularColorCommand>(timelineElement.Target, specColour);
                    break;
#elif DOTS_TWEEN_HDRP
                case TweenHDRPAlphaCutoffCommand alphaCutoffCommand:
                    ecb.AddComponent<TweenHDRPAlphaCutoffCommand>(timelineElement.Target, alphaCutoffCommand);
                    break;
                case TweenHDRPAmbientOcclusionRemapMaxCommand ambientOcclusionRemapMaxCommand:
                    ecb.AddComponent<TweenHDRPAmbientOcclusionRemapMaxCommand>(timelineElement.Target, ambientOcclusionRemapMaxCommand);
                    break;
                case TweenHDRPAmbientOcclusionRemapMinCommand ambientOcclusionRemapMinCommand:
                    ecb.AddComponent<TweenHDRPAmbientOcclusionRemapMinCommand>(timelineElement.Target, ambientOcclusionRemapMinCommand);
                    break;
                case TweenHDRPDetailAlbedoScaleCommand detailAlbedoScaleCommand:
                    ecb.AddComponent<TweenHDRPDetailAlbedoScaleCommand>(timelineElement.Target, detailAlbedoScaleCommand);
                    break;
                case TweenHDRPDetailNormalScaleCommand detailNormalScaleCommand:
                    ecb.AddComponent<TweenHDRPDetailNormalScaleCommand>(timelineElement.Target, detailNormalScaleCommand);
                    break;
                case TweenHDRPDetailSmoothnessScaleCommand detailSmoothnessScaleCommand:
                    ecb.AddComponent<TweenHDRPDetailSmoothnessScaleCommand>(timelineElement.Target, detailSmoothnessScaleCommand);
                    break;
                case TweenHDRPDiffusionProfileHashCommand diffusionProfileHashCommand:
                    ecb.AddComponent<TweenHDRPDiffusionProfileHashCommand>(timelineElement.Target, diffusionProfileHashCommand);
                    break;
                case TweenHDRPEmissiveColorCommand emissiveColorCommand:
                    ecb.AddComponent<TweenHDRPEmissiveColorCommand>(timelineElement.Target, emissiveColorCommand);
                    break;
                case TweenHDRPFadeCommand fadeCommand:
                    ecb.AddComponent<TweenHDRPFadeCommand>(timelineElement.Target, fadeCommand);
                    break;
                case TweenHDRPFadeUnlitCommand fadeUnlitCommand:
                    ecb.AddComponent<TweenHDRPFadeUnlitCommand>(timelineElement.Target, fadeUnlitCommand);
                    break;
                case TweenHDRPMetallicCommand metallicCommand:
                    ecb.AddComponent<TweenHDRPMetallicCommand>(timelineElement.Target, metallicCommand);
                    break;
                case TweenHDRPSmoothnessCommand smoothnessCommand:
                    ecb.AddComponent<TweenHDRPSmoothnessCommand>(timelineElement.Target, smoothnessCommand);
                    break;
                case TweenHDRPSmoothnessRemapMaxCommand smoothnessRemapMaxCommand:
                    ecb.AddComponent<TweenHDRPSmoothnessRemapMaxCommand>(timelineElement.Target, smoothnessRemapMaxCommand);
                    break;
                case TweenHDRPSmoothnessRemapMinCommand smoothnessRemapMinCommand:
                    ecb.AddComponent<TweenHDRPSmoothnessRemapMinCommand>(timelineElement.Target, smoothnessRemapMinCommand);
                    break;
                case TweenHDRPSpecularColorCommand specularColorCommand:
                    ecb.AddComponent<TweenHDRPSpecularColorCommand>(timelineElement.Target, specularColorCommand);
                    break;
                case TweenHDRPThicknessCommand thicknessCommand:
                    ecb.AddComponent<TweenHDRPThicknessCommand>(timelineElement.Target, thicknessCommand);
                    break;
                case TweenHDRPThicknessRemapCommand thicknessRemapCommand:
                    ecb.AddComponent<TweenHDRPThicknessRemapCommand>(timelineElement.Target, thicknessRemapCommand);
                    break;
                case TweenHDRPTintCommand tintCommand:
                    ecb.AddComponent<TweenHDRPTintCommand>(timelineElement.Target, tintCommand);
                    break;
                case TweenHDRPTintUnlitCommand tintUnlitCommand:
                    ecb.AddComponent<TweenHDRPTintUnlitCommand>(timelineElement.Target, tintUnlitCommand);
                    break;
#endif
            }
        }
    }
}