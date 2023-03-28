using System;
using DotsTween.Timelines;
using DotsTween.Tweens;
using Unity.Burst;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;

[assembly: RegisterGenericComponentType(typeof(TimelineElement<TweenTranslationCommand>))]
[assembly: RegisterGenericComponentType(typeof(TimelineElement<TweenScaleCommand>))]
[assembly: RegisterGenericComponentType(typeof(TimelineElement<TweenRotationCommand>))]
[assembly: RegisterGenericComponentType(typeof(TimelineElement<TweenNonUniformScaleCommand>))]

#if DOTS_TWEEN_URP
[assembly: RegisterGenericComponentType(typeof(TimelineElement<TweenURPTintCommand>))]
[assembly: RegisterGenericComponentType(typeof(TimelineElement<TweenURPFadeCommand>))]
[assembly: RegisterGenericComponentType(typeof(TimelineElement<TweenURPBumpScaleCommand>))]
[assembly: RegisterGenericComponentType(typeof(TimelineElement<TweenURPCutoffCommand>))]
[assembly: RegisterGenericComponentType(typeof(TimelineElement<TweenURPEmissionColorCommand>))]
[assembly: RegisterGenericComponentType(typeof(TimelineElement<TweenURPMetallicCommand>))]
[assembly: RegisterGenericComponentType(typeof(TimelineElement<TweenURPOcclusionStrengthCommand>))]
[assembly: RegisterGenericComponentType(typeof(TimelineElement<TweenURPSmoothnessCommand>))]
[assembly: RegisterGenericComponentType(typeof(TimelineElement<TweenURPSpecularColorCommand>))]
#elif DOTS_TWEEN_HDRP
[assembly: RegisterGenericComponentType(typeof(TimelineElement<TweenHDRPAlphaCutoffCommand>))]
[assembly: RegisterGenericComponentType(typeof(TimelineElement<TweenHDRPAmbientOcclusionRemapMaxCommand>))]
[assembly: RegisterGenericComponentType(typeof(TimelineElement<TweenHDRPAmbientOcclusionRemapMinCommand>))]
[assembly: RegisterGenericComponentType(typeof(TimelineElement<TweenHDRPDetailAlbedoScaleCommand>))]
[assembly: RegisterGenericComponentType(typeof(TimelineElement<TweenHDRPDetailNormalScaleCommand>))]
[assembly: RegisterGenericComponentType(typeof(TimelineElement<TweenHDRPDetailSmoothnessScaleCommand>))]
[assembly: RegisterGenericComponentType(typeof(TimelineElement<TweenHDRPDiffusionProfileHashCommand>))]
[assembly: RegisterGenericComponentType(typeof(TimelineElement<TweenHDRPEmissiveColorCommand>))]
[assembly: RegisterGenericComponentType(typeof(TimelineElement<TweenHDRPFadeCommand>))]
[assembly: RegisterGenericComponentType(typeof(TimelineElement<TweenHDRPFadeUnlitCommand>))]
[assembly: RegisterGenericComponentType(typeof(TimelineElement<TweenHDRPMetallicCommand>))]
[assembly: RegisterGenericComponentType(typeof(TimelineElement<TweenHDRPSmoothnessCommand>))]
[assembly: RegisterGenericComponentType(typeof(TimelineElement<TweenHDRPSmoothnessRemapMaxCommand>))]
[assembly: RegisterGenericComponentType(typeof(TimelineElement<TweenHDRPSmoothnessRemapMinCommand>))]
[assembly: RegisterGenericComponentType(typeof(TimelineElement<TweenHDRPSpecularColorCommand>))]
[assembly: RegisterGenericComponentType(typeof(TimelineElement<TweenHDRPThicknessCommand>))]
[assembly: RegisterGenericComponentType(typeof(TimelineElement<TweenHDRPThicknessRemapCommand>))]
[assembly: RegisterGenericComponentType(typeof(TimelineElement<TweenHDRPTintCommand>))]
[assembly: RegisterGenericComponentType(typeof(TimelineElement<TweenHDRPTintUnlitCommand>))]
#endif

#if DOTS_TWEEN_SPLINES
[assembly: RegisterGenericComponentType(typeof(TimelineElement<TweenSplineMovementCommand>))]
#endif

namespace DotsTween.Timelines.Systems
{
    [BurstCompile]
    internal static class TimelineSystemCommandTypeHelper
    {
        private static readonly ComponentType Translation = ComponentType.ReadOnly<TweenTranslationCommand>();
        private static readonly ComponentType Scale = ComponentType.ReadOnly<TweenScaleCommand>();
        private static readonly ComponentType Rotation = ComponentType.ReadOnly<TweenRotationCommand>();
        private static readonly ComponentType NonUniformScale = ComponentType.ReadOnly<TweenNonUniformScaleCommand>();
#if DOTS_TWEEN_URP
        private static readonly ComponentType URPTint = ComponentType.ReadOnly<TweenURPTintCommand>();
        private static readonly ComponentType URPFade = ComponentType.ReadOnly<TweenURPFadeCommand>();
        private static readonly ComponentType URPBumpScale = ComponentType.ReadOnly<TweenURPBumpScaleCommand>();
        private static readonly ComponentType URPCutoff = ComponentType.ReadOnly<TweenURPCutoffCommand>();
        private static readonly ComponentType URPEmissionColor = ComponentType.ReadOnly<TweenURPEmissionColorCommand>();
        private static readonly ComponentType URPMetallic = ComponentType.ReadOnly<TweenURPMetallicCommand>();
        private static readonly ComponentType URPOcclusionStrength = ComponentType.ReadOnly<TweenURPOcclusionStrengthCommand>();
        private static readonly ComponentType URPSmoothness = ComponentType.ReadOnly<TweenURPSmoothnessCommand>();
        private static readonly ComponentType URPSpecularColor = ComponentType.ReadOnly<TweenURPSpecularColorCommand>();
#elif DOTS_TWEEN_HDRP
        private static readonly ComponentType HDRPAlphaCutoff = ComponentType.ReadOnly<TweenHDRPAlphaCutoffCommand>();
        private static readonly ComponentType HDRPAmbientOcclusionRemapMax = ComponentType.ReadOnly<TweenHDRPAmbientOcclusionRemapMaxCommand>();
        private static readonly ComponentType HDRPAmbientOcclusionRemapMin = ComponentType.ReadOnly<TweenHDRPAmbientOcclusionRemapMinCommand>();
        private static readonly ComponentType HDRPDetailAlbedoScale = ComponentType.ReadOnly<TweenHDRPDetailAlbedoScaleCommand>();
        private static readonly ComponentType HDRPDetailNormalScale = ComponentType.ReadOnly<TweenHDRPDetailNormalScaleCommand>();
        private static readonly ComponentType HDRPDetailSmoothnessScale = ComponentType.ReadOnly<TweenHDRPDetailSmoothnessScaleCommand>();
        private static readonly ComponentType HDRPDiffusionProfileHash = ComponentType.ReadOnly<TweenHDRPDiffusionProfileHashCommand>();
        private static readonly ComponentType HDRPEmissiveColor = ComponentType.ReadOnly<TweenHDRPEmissiveColorCommand>();
        private static readonly ComponentType HDRPFade = ComponentType.ReadOnly<TweenHDRPFadeCommand>();
        private static readonly ComponentType HDRPFadeUnlit = ComponentType.ReadOnly<TweenHDRPFadeUnlitCommand>();
        private static readonly ComponentType HDRPMetallic = ComponentType.ReadOnly<TweenHDRPMetallicCommand>();
        private static readonly ComponentType HDRPSmoothness = ComponentType.ReadOnly<TweenHDRPSmoothnessCommand>();
        private static readonly ComponentType HDRPSmoothnessRemapMax = ComponentType.ReadOnly<TweenHDRPSmoothnessRemapMaxCommand>();
        private static readonly ComponentType HDRPSmoothnessRemapMin = ComponentType.ReadOnly<TweenHDRPSmoothnessRemapMinCommand>();
        private static readonly ComponentType HDRPSpecularColor = ComponentType.ReadOnly<TweenHDRPSpecularColorCommand>();
        private static readonly ComponentType HDRPThickness = ComponentType.ReadOnly<TweenHDRPThicknessCommand>();
        private static readonly ComponentType HDRPThicknessRemap = ComponentType.ReadOnly<TweenHDRPThicknessRemapCommand>();
        private static readonly ComponentType HDRPTint = ComponentType.ReadOnly<TweenHDRPTintCommand>();
        private static readonly ComponentType HDRPTintUnlit = ComponentType.ReadOnly<TweenHDRPTintUnlitCommand>();
#endif
#if DOTS_TWEEN_SPLINES
        private static readonly ComponentType SplineMovement = ComponentType.ReadOnly<TweenSplineMovementCommand>();
#endif

        internal static ITimelineElement DereferenceNextTimelineElement(in ComponentType componentType, ref UnsafeAppendBuffer.Reader bufferReader)
        {
            if (componentType == Translation) return bufferReader.ReadNext<TimelineElement<TweenTranslationCommand>>();
            if (componentType == Scale) return bufferReader.ReadNext<TimelineElement<TweenScaleCommand>>();
            if (componentType == Rotation) return bufferReader.ReadNext<TimelineElement<TweenRotationCommand>>();
            if (componentType == NonUniformScale) return bufferReader.ReadNext<TimelineElement<TweenNonUniformScaleCommand>>();
#if DOTS_TWEEN_URP
            if (componentType == URPTint) return bufferReader.ReadNext<TimelineElement<TweenURPTintCommand>>();
            if (componentType == URPFade) return bufferReader.ReadNext<TimelineElement<TweenURPFadeCommand>>();
            if (componentType == URPBumpScale) return bufferReader.ReadNext<TimelineElement<TweenURPBumpScaleCommand>>();
            if (componentType == URPCutoff) return bufferReader.ReadNext<TimelineElement<TweenURPCutoffCommand>>();
            if (componentType == URPEmissionColor) return bufferReader.ReadNext<TimelineElement<TweenURPEmissionColorCommand>>();
            if (componentType == URPMetallic) return bufferReader.ReadNext<TimelineElement<TweenURPMetallicCommand>>();
            if (componentType == URPOcclusionStrength) return bufferReader.ReadNext<TimelineElement<TweenURPOcclusionStrengthCommand>>();
            if (componentType == URPSmoothness) return bufferReader.ReadNext<TimelineElement<TweenURPSmoothnessCommand>>();
            if (componentType == URPSpecularColor) return bufferReader.ReadNext<TimelineElement<TweenURPSpecularColorCommand>>();
#elif DOTS_TWEEN_HDRP
            if (componentType == HDRPAlphaCutoff) return bufferReader.ReadNext<TimelineElement<TweenHDRPAlphaCutoffCommand>>();
            if (componentType == HDRPAmbientOcclusionRemapMax) return bufferReader.ReadNext<TimelineElement<TweenHDRPAmbientOcclusionRemapMaxCommand>>();
            if (componentType == HDRPAmbientOcclusionRemapMin) return bufferReader.ReadNext<TimelineElement<TweenHDRPAmbientOcclusionRemapMinCommand>>();
            if (componentType == HDRPDetailAlbedoScale) return bufferReader.ReadNext<TimelineElement<TweenHDRPDetailAlbedoScaleCommand>>();
            if (componentType == HDRPDetailNormalScale) return bufferReader.ReadNext<TimelineElement<TweenHDRPDetailNormalScaleCommand>>();
            if (componentType == HDRPDetailSmoothnessScale) return bufferReader.ReadNext<TimelineElement<TweenHDRPDetailSmoothnessScaleCommand>>();
            if (componentType == HDRPDiffusionProfileHash) return bufferReader.ReadNext<TimelineElement<TweenHDRPDiffusionProfileHashCommand>>();
            if (componentType == HDRPEmissiveColor) return bufferReader.ReadNext<TimelineElement<TweenHDRPEmissiveColorCommand>>();
            if (componentType == HDRPFade) return bufferReader.ReadNext<TimelineElement<TweenHDRPFadeCommand>>();
            if (componentType == HDRPFadeUnlit) return bufferReader.ReadNext<TimelineElement<TweenHDRPFadeUnlitCommand>>();
            if (componentType == HDRPMetallic) return bufferReader.ReadNext<TimelineElement<TweenHDRPMetallicCommand>>();
            if (componentType == HDRPSmoothness) return bufferReader.ReadNext<TimelineElement<TweenHDRPSmoothnessCommand>>();
            if (componentType == HDRPSmoothnessRemapMax) return bufferReader.ReadNext<TimelineElement<TweenHDRPSmoothnessRemapMaxCommand>>();
            if (componentType == HDRPSmoothnessRemapMin) return bufferReader.ReadNext<TimelineElement<TweenHDRPSmoothnessRemapMinCommand>>();
            if (componentType == HDRPSpecularColor) return bufferReader.ReadNext<TimelineElement<TweenHDRPSpecularColorCommand>>();
            if (componentType == HDRPThickness) return bufferReader.ReadNext<TimelineElement<TweenHDRPThicknessCommand>>();
            if (componentType == HDRPThicknessRemap) return bufferReader.ReadNext<TimelineElement<TweenHDRPThicknessRemapCommand>>();
            if (componentType == HDRPTint) return bufferReader.ReadNext<TimelineElement<TweenHDRPTintCommand>>();
            if (componentType == HDRPTintUnlit) return bufferReader.ReadNext<TimelineElement<TweenHDRPTintUnlitCommand>>();
#endif
#if DOTS_TWEEN_SPLINES
            if (componentType == SplineMovement) return bufferReader.ReadNext<TimelineElement<TweenSplineMovementCommand>>();
#endif

            throw new InvalidOperationException("Never seen this ComponentType before in my whole career");
        }

        [BurstDiscard]
        internal static void Add(ref EntityCommandBuffer ecb, in ComponentType componentType, ref ITimelineElement timelineElement)
        {
            var target = timelineElement.GetTargetEntity();
            var command = timelineElement.GetCommand();
            
            if (componentType == Translation) { ecb.AddComponent<TweenTranslationCommand>(target, (TweenTranslationCommand)command); return; }
            if (componentType == Scale) { ecb.AddComponent<TweenScaleCommand>(target, (TweenScaleCommand)command); return; }
            if (componentType == Rotation) { ecb.AddComponent<TweenRotationCommand>(target, (TweenRotationCommand)command); return; }
            if (componentType == NonUniformScale) { ecb.AddComponent<TweenNonUniformScaleCommand>(target, (TweenNonUniformScaleCommand)command); return; }
#if DOTS_TWEEN_URP
            if (componentType == URPTint) { ecb.AddComponent<TweenURPTintCommand>(target, (TweenURPTintCommand)command); return; }
            if (componentType == URPFade) { ecb.AddComponent<TweenURPFadeCommand>(target, (TweenURPFadeCommand)command); return; }
            if (componentType == URPBumpScale) { ecb.AddComponent<TweenURPBumpScaleCommand>(target, (TweenURPBumpScaleCommand)command); return; }
            if (componentType == URPCutoff) { ecb.AddComponent<TweenURPCutoffCommand>(target, (TweenURPCutoffCommand)command); return; }
            if (componentType == URPEmissionColor) { ecb.AddComponent<TweenURPEmissionColorCommand>(target, (TweenURPEmissionColorCommand)command); return; }
            if (componentType == URPMetallic) { ecb.AddComponent<TweenURPMetallicCommand>(target, (TweenURPMetallicCommand)command); return; }
            if (componentType == URPOcclusionStrength) { ecb.AddComponent<TweenURPOcclusionStrengthCommand>(target, (TweenURPOcclusionStrengthCommand)command); return; }
            if (componentType == URPSmoothness) { ecb.AddComponent<TweenURPSmoothnessCommand>(target, (TweenURPSmoothnessCommand)command); return; }
            if (componentType == URPSpecularColor) { ecb.AddComponent<TweenURPSpecularColorCommand>(target, (TweenURPSpecularColorCommand)command); return; }
#elif DOTS_TWEEN_HDRP
            if (componentType == HDRPAlphaCutoff) { ecb.AddComponent<TweenHDRPAlphaCutoffCommand>(target, (TweenHDRPAlphaCutoffCommand)command); return; }
            if (componentType == HDRPAmbientOcclusionRemapMax) { ecb.AddComponent<TweenHDRPAmbientOcclusionRemapMaxCommand>(target, (TweenHDRPAmbientOcclusionRemapMaxCommand)command); return; }
            if (componentType == HDRPAmbientOcclusionRemapMin) { ecb.AddComponent<TweenHDRPAmbientOcclusionRemapMinCommand>(target, (TweenHDRPAmbientOcclusionRemapMinCommand)command); return; }
            if (componentType == HDRPDetailAlbedoScale) { ecb.AddComponent<TweenHDRPDetailAlbedoScaleCommand>(target, (TweenHDRPDetailAlbedoScaleCommand)command); return; }
            if (componentType == HDRPDetailNormalScale) { ecb.AddComponent<TweenHDRPDetailNormalScaleCommand>(target, (TweenHDRPDetailNormalScaleCommand)command); return; }
            if (componentType == HDRPDetailSmoothnessScale) { ecb.AddComponent<TweenHDRPDetailSmoothnessScaleCommand>(target, (TweenHDRPDetailSmoothnessScaleCommand)command); return; }
            if (componentType == HDRPDiffusionProfileHash) { ecb.AddComponent<TweenHDRPDiffusionProfileHashCommand>(target, (TweenHDRPDiffusionProfileHashCommand)command); return; }
            if (componentType == HDRPEmissiveColor) { ecb.AddComponent<TweenHDRPEmissiveColorCommand>(target, (TweenHDRPEmissiveColorCommand)command); return; }
            if (componentType == HDRPFade) { ecb.AddComponent<TweenHDRPFadeCommand>(target, (TweenHDRPFadeCommand)command); return; }
            if (componentType == HDRPFadeUnlit) { ecb.AddComponent<TweenHDRPFadeUnlitCommand>(target, (TweenHDRPFadeUnlitCommand)command); return; }
            if (componentType == HDRPMetallic) { ecb.AddComponent<TweenHDRPMetallicCommand>(target, (TweenHDRPMetallicCommand)command); return; }
            if (componentType == HDRPSmoothness) { ecb.AddComponent<TweenHDRPSmoothnessCommand>(target, (TweenHDRPSmoothnessCommand)command); return; }
            if (componentType == HDRPSmoothnessRemapMax) { ecb.AddComponent<TweenHDRPSmoothnessRemapMaxCommand>(target, (TweenHDRPSmoothnessRemapMaxCommand)command); return; }
            if (componentType == HDRPSmoothnessRemapMin) { ecb.AddComponent<TweenHDRPSmoothnessRemapMinCommand>(target, (TweenHDRPSmoothnessRemapMinCommand)command); return; }
            if (componentType == HDRPSpecularColor) { ecb.AddComponent<TweenHDRPSpecularColorCommand>(target, (TweenHDRPSpecularColorCommand)command); return; }
            if (componentType == HDRPThickness) { ecb.AddComponent<TweenHDRPThicknessCommand>(target, (TweenHDRPThicknessCommand)command); return; }
            if (componentType == HDRPThicknessRemap) { ecb.AddComponent<TweenHDRPThicknessRemapCommand>(target, (TweenHDRPThicknessRemapCommand)command); return; }
            if (componentType == HDRPTint) { ecb.AddComponent<TweenHDRPTintCommand>(target, (TweenHDRPTintCommand)command); return; }
            if (componentType == HDRPTintUnlit) { ecb.AddComponent<TweenHDRPTintUnlitCommand>(target, (TweenHDRPTintUnlitCommand)command); return; }
#endif
#if DOTS_TWEEN_SPLINES
            if (componentType == SplineMovement) { ecb.AddComponent<TweenSplineMovementCommand>(target, (TweenSplineMovementCommand)command); return; }
#endif
        }

        [BurstDiscard]
        internal static bool AlreadyHasInfoComponent(in Entity target, in ComponentType componentType, in EntityManager entityManager)
        {
            if (componentType == Translation) return entityManager.HasComponent<TweenTranslation>(target);
            if (componentType == Scale) return entityManager.HasComponent<TweenRotation>(target);
            if (componentType == Rotation) return entityManager.HasComponent<TweenScale>(target);
            if (componentType == NonUniformScale) return entityManager.HasComponent<TweenNonUniformScale>(target);
#if DOTS_TWEEN_URP
            if (componentType == URPTint) return entityManager.HasComponent<TweenURPTint>(target);
            if (componentType == URPFade) return entityManager.HasComponent<TweenURPFade>(target);
            if (componentType == URPBumpScale) return entityManager.HasComponent<TweenURPBumpScale>(target);
            if (componentType == URPCutoff) return entityManager.HasComponent<TweenURPCutoff>(target);
            if (componentType == URPEmissionColor) return entityManager.HasComponent<TweenURPEmissionColor>(target);
            if (componentType == URPMetallic) return entityManager.HasComponent<TweenURPMetallic>(target);
            if (componentType == URPOcclusionStrength) return entityManager.HasComponent<TweenURPOcclusionStrength>(target);
            if (componentType == URPSmoothness) return entityManager.HasComponent<TweenURPSmoothness>(target);
            if (componentType == URPSpecularColor) return entityManager.HasComponent<TweenURPSpecularColor>(target);
#elif DOTS_TWEEN_HDRP
            if (componentType == HDRPAlphaCutoff) return entityManager.HasComponent<TweenHDRPAlphaCutoff>(target);
            if (componentType == HDRPAmbientOcclusionRemapMax) return entityManager.HasComponent<TweenHDRPAmbientOcclusionRemapMax>(target);
            if (componentType == HDRPAmbientOcclusionRemapMin) return entityManager.HasComponent<TweenHDRPAmbientOcclusionRemapMin>(target);
            if (componentType == HDRPDetailAlbedoScale) return entityManager.HasComponent<TweenHDRPDetailAlbedoScale>(target);
            if (componentType == HDRPDetailNormalScale) return entityManager.HasComponent<TweenHDRPDetailNormalScale>(target);
            if (componentType == HDRPDetailSmoothnessScale) return entityManager.HasComponent<TweenHDRPDetailSmoothnessScale>(target);
            if (componentType == HDRPDiffusionProfileHash) return entityManager.HasComponent<TweenHDRPDiffusionProfileHash>(target);
            if (componentType == HDRPEmissiveColor) return entityManager.HasComponent<TweenHDRPEmissiveColor>(target);
            if (componentType == HDRPMetallic) return entityManager.HasComponent<TweenHDRPMetallic>(target);
            if (componentType == HDRPSmoothness) return entityManager.HasComponent<TweenHDRPSmoothness>(target);
            if (componentType == HDRPSmoothnessRemapMax) return entityManager.HasComponent<TweenHDRPSmoothnessRemapMax>(target);
            if (componentType == HDRPSmoothnessRemapMin) return entityManager.HasComponent<TweenHDRPSmoothnessRemapMin>(target);
            if (componentType == HDRPSpecularColor) return entityManager.HasComponent<TweenHDRPSpecularColor>(target);
            if (componentType == HDRPThickness) return entityManager.HasComponent<TweenHDRPThickness>(target);
            if (componentType == HDRPThicknessRemap) return entityManager.HasComponent<TweenHDRPThicknessRemap>(target);
            if (componentType == HDRPTint) return entityManager.HasComponent<TweenHDRPTint>(target);
            if (componentType == HDRPTintUnlit) return entityManager.HasComponent<TweenHDRPTintUnlit>(target);
            if (componentType == HDRPFade) return entityManager.HasComponent<TweenHDRPFade>(target);
            if (componentType == HDRPFadeUnlit) return entityManager.HasComponent<TweenHDRPFadeUnlit>(target);
#endif
#if DOTS_TWEEN_SPLINES
            if (componentType == SplineMovement) return entityManager.HasComponent<TweenSplineMovement>(target);
#endif
            return false;
        }
    }
}