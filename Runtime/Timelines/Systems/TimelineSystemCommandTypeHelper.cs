using System;
using DotsTween.Tweens;
using Unity.Burst;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;

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

        [BurstDiscard]
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
    }
}