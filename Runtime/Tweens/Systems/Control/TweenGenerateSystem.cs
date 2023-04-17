using System.Runtime.InteropServices;
using Unity.Burst;
using Unity.Burst.Intrinsics;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

[assembly: RegisterGenericJobType(typeof(DotsTween.Tweens.TweenTranslationGenerateSystem.GenerateJob))]
[assembly: RegisterGenericJobType(typeof(DotsTween.Tweens.TweenRotationGenerateSystem.GenerateJob))]
[assembly: RegisterGenericJobType(typeof(DotsTween.Tweens.TweenScaleGenerateSystem.GenerateJob))]
[assembly: RegisterGenericJobType(typeof(DotsTween.Tweens.TweenNonUniformScaleGenerateSystem.GenerateJob))]

#if DOTS_TWEEN_URP
[assembly: RegisterGenericJobType(typeof(DotsTween.Tweens.TweenURPTintGenerateSystem.GenerateJob))]
[assembly: RegisterGenericJobType(typeof(DotsTween.Tweens.TweenURPFadeGenerateSystem.GenerateJob))]
[assembly: RegisterGenericJobType(typeof(DotsTween.Tweens.TweenURPBumpScaleGenerateSystem.GenerateJob))]
[assembly: RegisterGenericJobType(typeof(DotsTween.Tweens.TweenURPCutoffGenerateSystem.GenerateJob))]
[assembly: RegisterGenericJobType(typeof(DotsTween.Tweens.TweenURPEmissionColorGenerateSystem.GenerateJob))]
[assembly: RegisterGenericJobType(typeof(DotsTween.Tweens.TweenURPMetallicGenerateSystem.GenerateJob))]
[assembly: RegisterGenericJobType(typeof(DotsTween.Tweens.TweenURPOcclusionStrengthGenerateSystem.GenerateJob))]
[assembly: RegisterGenericJobType(typeof(DotsTween.Tweens.TweenURPSmoothnessGenerateSystem.GenerateJob))]
[assembly: RegisterGenericJobType(typeof(DotsTween.Tweens.TweenURPSpecularColorGenerateSystem.GenerateJob))]
#elif DOTS_TWEEN_HDRP
[assembly: RegisterGenericJobType(typeof(DotsTween.Tweens.TweenHDRPAlphaCutoffGenerateSystem.GenerateJob))]
[assembly: RegisterGenericJobType(typeof(DotsTween.Tweens.TweenHDRPAmbientOcclusionRemapMaxGenerateSystem.GenerateJob))]
[assembly: RegisterGenericJobType(typeof(DotsTween.Tweens.TweenHDRPAmbientOcclusionRemapMinGenerateSystem.GenerateJob))]
[assembly: RegisterGenericJobType(typeof(DotsTween.Tweens.TweenHDRPDetailAlbedoScaleGenerateSystem.GenerateJob))]
[assembly: RegisterGenericJobType(typeof(DotsTween.Tweens.TweenHDRPDetailNormalScaleGenerateSystem.GenerateJob))]
[assembly: RegisterGenericJobType(typeof(DotsTween.Tweens.TweenHDRPDetailSmoothnessScaleGenerateSystem.GenerateJob))]
[assembly: RegisterGenericJobType(typeof(DotsTween.Tweens.TweenHDRPDiffusionProfileHashGenerateSystem.GenerateJob))]
[assembly: RegisterGenericJobType(typeof(DotsTween.Tweens.TweenHDRPEmissiveColorGenerateSystem.GenerateJob))]
[assembly: RegisterGenericJobType(typeof(DotsTween.Tweens.TweenHDRPMetallicGenerateSystem.GenerateJob))]
[assembly: RegisterGenericJobType(typeof(DotsTween.Tweens.TweenHDRPSmoothnessGenerateSystem.GenerateJob))]
[assembly: RegisterGenericJobType(typeof(DotsTween.Tweens.TweenHDRPSmoothnessRemapMaxGenerateSystem.GenerateJob))]
[assembly: RegisterGenericJobType(typeof(DotsTween.Tweens.TweenHDRPSmoothnessRemapMinGenerateSystem.GenerateJob))]
[assembly: RegisterGenericJobType(typeof(DotsTween.Tweens.TweenHDRPSpecularColorGenerateSystem.GenerateJob))]
[assembly: RegisterGenericJobType(typeof(DotsTween.Tweens.TweenHDRPThicknessGenerateSystem.GenerateJob))]
[assembly: RegisterGenericJobType(typeof(DotsTween.Tweens.TweenHDRPThicknessRemapGenerateSystem.GenerateJob))]
[assembly: RegisterGenericJobType(typeof(DotsTween.Tweens.TweenHDRPTintGenerateSystem.GenerateJob))]
[assembly: RegisterGenericJobType(typeof(DotsTween.Tweens.TweenHDRPTintUnlitGenerateSystem.GenerateJob))]
[assembly: RegisterGenericJobType(typeof(DotsTween.Tweens.TweenHDRPFadeGenerateSystem.GenerateJob))]
[assembly: RegisterGenericJobType(typeof(DotsTween.Tweens.TweenHDRPFadeUnlitGenerateSystem.GenerateJob))]
#endif

namespace DotsTween.Tweens
{
    [BurstCompile]
    [UpdateInGroup(typeof(TweenGenerateSystemGroup))]
    internal abstract partial class TweenGenerateSystem<TTweenCommand, TTweenInfo, TTarget, TTweenInfoValue> : SystemBase
        where TTweenCommand : unmanaged, IComponentData, ITweenParams, ITweenInfo<TTweenInfoValue>
        where TTweenInfo : unmanaged, IComponentData, ITweenId, ITweenInfo<TTweenInfoValue>
        where TTarget : unmanaged, IComponentData
        where TTweenInfoValue : unmanaged
    {
        [BurstCompile]
        internal struct GenerateJob : IJobChunk
        {
            [ReadOnly] public EntityTypeHandle EntityType;
            [ReadOnly] public ComponentTypeHandle<TTweenCommand> TweenCommandType;
            [ReadOnly] public ComponentTypeHandle<TTarget> TargetType;
            [ReadOnly] public BufferTypeHandle<TweenState> TweenStateBufferType;
            [ReadOnly] public BufferTypeHandle<TweenPauseInfo> TweenPauseBufferType;
            [ReadOnly] public BufferTypeHandle<TweenResumeInfo> TweenResumeBufferType;

            public EntityCommandBuffer.ParallelWriter ParallelWriter;

            [BurstCompile]
            public void Execute(in ArchetypeChunk chunk, int chunkIndex, bool useEnabledMask, in v128 chunkEnabledMask)
            {
                bool hasTweenStateBuffer = chunk.Has(ref TweenStateBufferType);
                bool hasTweenPauseBuffer = chunk.Has(ref TweenPauseBufferType);
                bool hasTweenResumeBuffer = chunk.Has(ref TweenResumeBufferType);
                bool hasTargetType = chunk.Has(ref TargetType);

                NativeArray<Entity> entities = chunk.GetNativeArray(EntityType);
                NativeArray<TTweenCommand> commands = chunk.GetNativeArray(ref TweenCommandType);
                for (int i = 0; i < entities.Length; i++)
                {
                    Entity entity = entities[i];
                    TTweenCommand command = commands[i];

                    TryToAddBuffers(out var didAddBuffers, chunkIndex, ref entity, hasTweenStateBuffer, hasTweenPauseBuffer, hasTweenResumeBuffer);

                    if (didAddBuffers)
                    {
                        break;
                    }

                    if (!hasTargetType)
                    {
                        ParallelWriter.AddComponent<TTarget>(chunkIndex, entity);
                    }

                    var tweenParams = command.GetTweenParams();
                    TweenState tween = new TweenState(tweenParams);
                    ParallelWriter.AppendToBuffer(chunkIndex, entity, tween);
                    tweenParams.OnStart.Perform(ref ParallelWriter, chunkIndex, entity);

                    TTweenInfo info = default;
                    info.SetTweenId(tween.Id);
                    info.SetTweenInfo(command.GetTweenStart(), command.GetTweenEnd());
                    ParallelWriter.AddComponent(chunkIndex, entity, info);
                    ParallelWriter.RemoveComponent<TTweenCommand>(chunkIndex, entity);
                }
            }

            private void TryToAddBuffers(
                out bool buffersWereAdded,
                in int chunkIndex,
                ref Entity entity,
                [MarshalAs(UnmanagedType.U1)] bool hasTweenStateBuffer,
                [MarshalAs(UnmanagedType.U1)] bool hasTweenPauseBuffer,
                [MarshalAs(UnmanagedType.U1)] bool hasTweenResumeBuffer)
            {
                if (!hasTweenStateBuffer) ParallelWriter.AddBuffer<TweenState>(chunkIndex, entity);
                if (!hasTweenPauseBuffer) ParallelWriter.AddBuffer<TweenPauseInfo>(chunkIndex, entity);
                if (!hasTweenResumeBuffer) ParallelWriter.AddBuffer<TweenResumeInfo>(chunkIndex, entity);

                buffersWereAdded = !hasTweenStateBuffer
                    || !hasTweenPauseBuffer
                    || !hasTweenResumeBuffer;
            }
        }

        private EntityQuery tweenCommandQuery;

        [BurstCompile]
        protected override void OnCreate()
        {
            RequireForUpdate<TTweenCommand>();
            tweenCommandQuery = GetEntityQuery(ComponentType.ReadOnly<TTweenCommand>());
        }

        [BurstCompile]
        protected override void OnUpdate()
        {
            EndSimulationEntityCommandBufferSystem endSimECBSystem = World.GetOrCreateSystemManaged<EndSimulationEntityCommandBufferSystem>();

            GenerateJob job = new GenerateJob
            {
                EntityType = GetEntityTypeHandle(),
                TweenCommandType = GetComponentTypeHandle<TTweenCommand>(true),
                TargetType = GetComponentTypeHandle<TTarget>(true),
                TweenStateBufferType = GetBufferTypeHandle<TweenState>(true),
                TweenPauseBufferType = GetBufferTypeHandle<TweenPauseInfo>(true),
                TweenResumeBufferType = GetBufferTypeHandle<TweenResumeInfo>(true),
                ParallelWriter = endSimECBSystem.CreateCommandBuffer().AsParallelWriter(),
            };

            Dependency = job.ScheduleParallel(tweenCommandQuery, Dependency);
            endSimECBSystem.AddJobHandleForProducer(Dependency);
        }
    }

    [BurstCompile] internal partial class TweenTranslationGenerateSystem : TweenGenerateSystem<TweenTranslationCommand, TweenTranslation, LocalTransform, float3> { }
    [BurstCompile] internal partial class TweenRotationGenerateSystem : TweenGenerateSystem<TweenRotationCommand, TweenRotation, LocalTransform, quaternion> { }
    [BurstCompile] internal partial class TweenScaleGenerateSystem : TweenGenerateSystem<TweenScaleCommand, TweenScale, LocalTransform, float> { }
    [BurstCompile] internal partial class TweenNonUniformScaleGenerateSystem : TweenGenerateSystem<TweenNonUniformScaleCommand, TweenNonUniformScale, PostTransformMatrix, float3> { }
#if DOTS_TWEEN_URP
    [BurstCompile] internal partial class TweenURPTintGenerateSystem : TweenGenerateSystem<TweenURPTintCommand, TweenURPTint, URPMaterialPropertyBaseColor, float4> { }
    [BurstCompile] internal partial class TweenURPFadeGenerateSystem : TweenGenerateSystem<TweenURPFadeCommand, TweenURPFade, URPMaterialPropertyBaseColor, float> { }
    [BurstCompile] internal partial class TweenURPBumpScaleGenerateSystem : TweenGenerateSystem<TweenURPBumpScaleCommand, TweenURPBumpScale, URPMaterialPropertyBumpScale, float> { }
    [BurstCompile] internal partial class TweenURPCutoffGenerateSystem : TweenGenerateSystem<TweenURPCutoffCommand, TweenURPCutoff, URPMaterialPropertyCutoff, float> { }
    [BurstCompile] internal partial class TweenURPEmissionColorGenerateSystem : TweenGenerateSystem<TweenURPEmissionColorCommand, TweenURPEmissionColor, URPMaterialPropertyEmissionColor, float4> { }
    [BurstCompile] internal partial class TweenURPMetallicGenerateSystem : TweenGenerateSystem<TweenURPMetallicCommand, TweenURPMetallic, URPMaterialPropertyMetallic, float> { }
    [BurstCompile] internal partial class TweenURPOcclusionStrengthGenerateSystem : TweenGenerateSystem<TweenURPOcclusionStrengthCommand, TweenURPOcclusionStrength, URPMaterialPropertyOcclusionStrength, float> { }
    [BurstCompile] internal partial class TweenURPSmoothnessGenerateSystem : TweenGenerateSystem<TweenURPSmoothnessCommand, TweenURPSmoothness, URPMaterialPropertySmoothness, float> { }
    [BurstCompile] internal partial class TweenURPSpecularColorGenerateSystem : TweenGenerateSystem<TweenURPSpecularColorCommand, TweenURPSpecularColor, URPMaterialPropertySpecColor, float4> { }
#elif DOTS_TWEEN_HDRP
    [BurstCompile] internal partial class TweenHDRPAlphaCutoffGenerateSystem : TweenGenerateSystem<TweenHDRPAlphaCutoffCommand, TweenHDRPAlphaCutoff, HDRPMaterialPropertyAlphaCutoff, float> {}
    [BurstCompile] internal partial class TweenHDRPAmbientOcclusionRemapMaxGenerateSystem : TweenGenerateSystem<TweenHDRPAmbientOcclusionRemapMaxCommand, TweenHDRPAmbientOcclusionRemapMax, HDRPMaterialPropertyAORemapMax, float> {}
    [BurstCompile] internal partial class TweenHDRPAmbientOcclusionRemapMinGenerateSystem : TweenGenerateSystem<TweenHDRPAmbientOcclusionRemapMinCommand, TweenHDRPAmbientOcclusionRemapMin, HDRPMaterialPropertyAORemapMin, float> {}
    [BurstCompile] internal partial class TweenHDRPDetailAlbedoScaleGenerateSystem : TweenGenerateSystem<TweenHDRPDetailAlbedoScaleCommand, TweenHDRPDetailAlbedoScale, HDRPMaterialPropertyDetailAlbedoScale, float> {}
    [BurstCompile] internal partial class TweenHDRPDetailNormalScaleGenerateSystem : TweenGenerateSystem<TweenHDRPDetailNormalScaleCommand, TweenHDRPDetailNormalScale, HDRPMaterialPropertyDetailNormalScale, float> {}
    [BurstCompile] internal partial class TweenHDRPDetailSmoothnessScaleGenerateSystem : TweenGenerateSystem<TweenHDRPDetailSmoothnessScaleCommand, TweenHDRPDetailSmoothnessScale, HDRPMaterialPropertyDetailSmoothnessScale, float> {}
    [BurstCompile] internal partial class TweenHDRPDiffusionProfileHashGenerateSystem : TweenGenerateSystem<TweenHDRPDiffusionProfileHashCommand, TweenHDRPDiffusionProfileHash, HDRPMaterialPropertyDiffusionProfileHash, float> {}
    [BurstCompile] internal partial class TweenHDRPEmissiveColorGenerateSystem : TweenGenerateSystem<TweenHDRPEmissiveColorCommand, TweenHDRPEmissiveColor, HDRPMaterialPropertyEmissiveColor, float3> {}
    [BurstCompile] internal partial class TweenHDRPMetallicGenerateSystem : TweenGenerateSystem<TweenHDRPMetallicCommand, TweenHDRPMetallic, HDRPMaterialPropertyMetallic, float> {}
    [BurstCompile] internal partial class TweenHDRPSmoothnessGenerateSystem : TweenGenerateSystem<TweenHDRPSmoothnessCommand, TweenHDRPSmoothness, HDRPMaterialPropertySmoothness, float> {}
    [BurstCompile] internal partial class TweenHDRPSmoothnessRemapMaxGenerateSystem : TweenGenerateSystem<TweenHDRPSmoothnessRemapMaxCommand, TweenHDRPSmoothnessRemapMax, HDRPMaterialPropertySmoothnessRemapMax, float> {}
    [BurstCompile] internal partial class TweenHDRPSmoothnessRemapMinGenerateSystem : TweenGenerateSystem<TweenHDRPSmoothnessRemapMinCommand, TweenHDRPSmoothnessRemapMin, HDRPMaterialPropertySmoothnessRemapMin, float> {}
    [BurstCompile] internal partial class TweenHDRPSpecularColorGenerateSystem : TweenGenerateSystem<TweenHDRPSpecularColorCommand, TweenHDRPSpecularColor, HDRPMaterialPropertySpecularColor, float4> {}
    [BurstCompile] internal partial class TweenHDRPThicknessGenerateSystem : TweenGenerateSystem<TweenHDRPThicknessCommand, TweenHDRPThickness, HDRPMaterialPropertyThickness, float> {}
    [BurstCompile] internal partial class TweenHDRPThicknessRemapGenerateSystem : TweenGenerateSystem<TweenHDRPThicknessRemapCommand, TweenHDRPThicknessRemap, HDRPMaterialPropertyThicknessRemap, float4> {}
    [BurstCompile] internal partial class TweenHDRPTintGenerateSystem : TweenGenerateSystem<TweenHDRPTintCommand, TweenHDRPTint, HDRPMaterialPropertyBaseColor, float4> {}
    [BurstCompile] internal partial class TweenHDRPTintUnlitGenerateSystem : TweenGenerateSystem<TweenHDRPTintUnlitCommand, TweenHDRPTintUnlit, HDRPMaterialPropertyUnlitColor, float4> {}
    [BurstCompile] internal partial class TweenHDRPFadeGenerateSystem : TweenGenerateSystem<TweenHDRPFadeCommand, TweenHDRPFade, HDRPMaterialPropertyBaseColor, float> {}
    [BurstCompile] internal partial class TweenHDRPFadeUnlitGenerateSystem : TweenGenerateSystem<TweenHDRPFadeUnlitCommand, TweenHDRPFadeUnlit, HDRPMaterialPropertyUnlitColor, float> {}
#endif
}