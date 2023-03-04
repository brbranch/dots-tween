using Unity.Burst;
using Unity.Burst.Intrinsics;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;

[assembly: RegisterGenericJobType(typeof(DotsTween.Tweens.TweenTranslationDestroySystem.DestroyJob))]
[assembly: RegisterGenericJobType(typeof(DotsTween.Tweens.TweenRotationDestroySystem.DestroyJob))]
[assembly: RegisterGenericJobType(typeof(DotsTween.Tweens.TweenScaleDestroySystem.DestroyJob))]
[assembly: RegisterGenericJobType(typeof(DotsTween.Tweens.TweenNonUniformScaleDestroySystem.DestroyJob))]

#if DOTS_TWEEN_URP
[assembly: RegisterGenericJobType(typeof(DotsTween.Tweens.TweenURPTintDestroySystem.DestroyJob))]
[assembly: RegisterGenericJobType(typeof(DotsTween.Tweens.TweenURPFadeDestroySystem.DestroyJob))]
[assembly: RegisterGenericJobType(typeof(DotsTween.Tweens.TweenURPBumpScaleDestroySystem.DestroyJob))]
[assembly: RegisterGenericJobType(typeof(DotsTween.Tweens.TweenURPCutoffDestroySystem.DestroyJob))]
[assembly: RegisterGenericJobType(typeof(DotsTween.Tweens.TweenURPEmissionColorDestroySystem.DestroyJob))]
[assembly: RegisterGenericJobType(typeof(DotsTween.Tweens.TweenURPMetallicDestroySystem.DestroyJob))]
[assembly: RegisterGenericJobType(typeof(DotsTween.Tweens.TweenURPOcclusionStrengthDestroySystem.DestroyJob))]
[assembly: RegisterGenericJobType(typeof(DotsTween.Tweens.TweenURPSmoothnessDestroySystem.DestroyJob))]
[assembly: RegisterGenericJobType(typeof(DotsTween.Tweens.TweenURPSpecularColorDestroySystem.DestroyJob))]
#elif DOTS_TWEEN_HDRP
[assembly: RegisterGenericJobType(typeof(DotsTween.Tweens.TweenHDRPAlphaCutoffDestroySystem.DestroyJob))]
[assembly: RegisterGenericJobType(typeof(DotsTween.Tweens.TweenHDRPAmbientOcclusionRemapMaxDestroySystem.DestroyJob))]
[assembly: RegisterGenericJobType(typeof(DotsTween.Tweens.TweenHDRPAmbientOcclusionRemapMinDestroySystem.DestroyJob))]
[assembly: RegisterGenericJobType(typeof(DotsTween.Tweens.TweenHDRPDetailAlbedoScaleDestroySystem.DestroyJob))]
[assembly: RegisterGenericJobType(typeof(DotsTween.Tweens.TweenHDRPDetailNormalScaleDestroySystem.DestroyJob))]
[assembly: RegisterGenericJobType(typeof(DotsTween.Tweens.TweenHDRPDetailSmoothnessScaleDestroySystem.DestroyJob))]
[assembly: RegisterGenericJobType(typeof(DotsTween.Tweens.TweenHDRPDiffusionProfileHashDestroySystem.DestroyJob))]
[assembly: RegisterGenericJobType(typeof(DotsTween.Tweens.TweenHDRPEmissiveColorDestroySystem.DestroyJob))]
[assembly: RegisterGenericJobType(typeof(DotsTween.Tweens.TweenHDRPMetallicDestroySystem.DestroyJob))]
[assembly: RegisterGenericJobType(typeof(DotsTween.Tweens.TweenHDRPSmoothnessDestroySystem.DestroyJob))]
[assembly: RegisterGenericJobType(typeof(DotsTween.Tweens.TweenHDRPSmoothnessRemapMaxDestroySystem.DestroyJob))]
[assembly: RegisterGenericJobType(typeof(DotsTween.Tweens.TweenHDRPSmoothnessRemapMinDestroySystem.DestroyJob))]
[assembly: RegisterGenericJobType(typeof(DotsTween.Tweens.TweenHDRPSpecularColorDestroySystem.DestroyJob))]
[assembly: RegisterGenericJobType(typeof(DotsTween.Tweens.TweenHDRPThicknessDestroySystem.DestroyJob))]
[assembly: RegisterGenericJobType(typeof(DotsTween.Tweens.TweenHDRPThicknessRemapDestroySystem.DestroyJob))]
[assembly: RegisterGenericJobType(typeof(DotsTween.Tweens.TweenHDRPTintDestroySystem.DestroyJob))]
[assembly: RegisterGenericJobType(typeof(DotsTween.Tweens.TweenHDRPTintUnlitDestroySystem.DestroyJob))]
[assembly: RegisterGenericJobType(typeof(DotsTween.Tweens.TweenHDRPFadeDestroySystem.DestroyJob))]
[assembly: RegisterGenericJobType(typeof(DotsTween.Tweens.TweenHDRPFadeUnlitDestroySystem.DestroyJob))]
#endif

namespace DotsTween.Tweens
{
    [BurstCompile]
    [UpdateInGroup(typeof(TweenDestroySystemGroup))]
    internal abstract partial class TweenDestroySystem<TTweenInfo, TTweenInfoValue> : SystemBase
        where TTweenInfo : unmanaged, IComponentData, ITweenId, ITweenInfo<TTweenInfoValue>
    {
        [BurstCompile]
        internal struct DestroyJob : IJobChunk
        {
            [ReadOnly] public EntityTypeHandle EntityType;
            [ReadOnly] public ComponentTypeHandle<TTweenInfo> InfoType;

            [NativeDisableContainerSafetyRestriction] public BufferTypeHandle<TweenState> TweenBufferType;
            [NativeDisableContainerSafetyRestriction] public BufferTypeHandle<TweenDestroyCommand> DestroyCommandType;

            public EntityCommandBuffer.ParallelWriter ParallelWriter;

            [BurstCompile]
            public void Execute(in ArchetypeChunk chunk, int chunkIndex, bool useEnabledMask, in v128 chunkEnabledMask)
            {
                NativeArray<Entity> entities = chunk.GetNativeArray(EntityType);
                NativeArray<TTweenInfo> infos = chunk.GetNativeArray(ref InfoType);
                BufferAccessor<TweenState> tweenBuffers = chunk.GetBufferAccessor(ref TweenBufferType);
                BufferAccessor<TweenDestroyCommand> destroyBuffers = chunk.GetBufferAccessor(ref DestroyCommandType);
                for (int i = 0; i < entities.Length; i++)
                {
                    Entity entity = entities[i];
                    
                    bool shouldDestroy = false;
                    DynamicBuffer<TweenDestroyCommand> destroyBuffer = destroyBuffers[i];
                    for (int j = destroyBuffer.Length - 1; j >= 0; j--)
                    {
                        TweenDestroyCommand command = destroyBuffer[j];
                        if (infos[i].GetTweenId() == command.Id)
                        {
                            shouldDestroy = true;
                            destroyBuffer.RemoveAt(j);
                        }
                    }

                    if (!shouldDestroy)
                    {
                        // Shouldn't go here
                        continue;
                    }

                    DynamicBuffer<TweenState> tweenBuffer = tweenBuffers[i];
                    for (int j = tweenBuffer.Length - 1; j >= 0; j--)
                    {
                        TweenState tween = tweenBuffer[j];
                        if (infos[i].GetTweenId() == tween.Id)
                        {
                            tweenBuffer[j].Settings.OnComplete.Perform(ref ParallelWriter, chunkIndex, entity);
                            tweenBuffer.RemoveAt(j);
                            infos[i].Cleanup();
                            ParallelWriter.RemoveComponent<TTweenInfo>(chunkIndex, entity);
                            break;
                        }
                    }

                    if (tweenBuffer.IsEmpty)
                    {
                        ParallelWriter.RemoveComponent<TweenState>(chunkIndex, entity);
                    }

                    if (destroyBuffer.IsEmpty)
                    {
                        ParallelWriter.RemoveComponent<TweenDestroyCommand>(chunkIndex, entity);
                    }
                }
            }
        }

        private EntityQuery tweenInfoQuery;

        [BurstCompile]
        protected override void OnCreate()
        {
            tweenInfoQuery = GetEntityQuery(
                ComponentType.ReadOnly<TTweenInfo>(),
                ComponentType.ReadOnly<TweenState>(),
                ComponentType.ReadOnly<TweenDestroyCommand>());
        }

        [BurstCompile]
        protected override void OnUpdate()
        {
            EndSimulationEntityCommandBufferSystem endSimECBSystem = World.GetOrCreateSystemManaged<EndSimulationEntityCommandBufferSystem>();

            DestroyJob job = new DestroyJob
            {
                EntityType = GetEntityTypeHandle(),
                InfoType = GetComponentTypeHandle<TTweenInfo>(true),
                TweenBufferType = GetBufferTypeHandle<TweenState>(),
                DestroyCommandType = GetBufferTypeHandle<TweenDestroyCommand>(),
                ParallelWriter = endSimECBSystem.CreateCommandBuffer().AsParallelWriter(),
            };

            Dependency = job.ScheduleParallel(tweenInfoQuery, Dependency);
            endSimECBSystem.AddJobHandleForProducer(Dependency);
        }
    }

    [BurstCompile] internal partial class TweenTranslationDestroySystem : TweenDestroySystem<TweenTranslation, float3> { }
    [BurstCompile] internal partial class TweenRotationDestroySystem : TweenDestroySystem<TweenRotation, quaternion> { }
    [BurstCompile] internal partial class TweenScaleDestroySystem : TweenDestroySystem<TweenScale, float> { }
    [BurstCompile] internal partial class TweenNonUniformScaleDestroySystem : TweenDestroySystem<TweenNonUniformScale, float3> { }

#if DOTS_TWEEN_URP
    [BurstCompile] internal partial class TweenURPTintDestroySystem : TweenDestroySystem<TweenURPTint, float4> {}
    [BurstCompile] internal partial class TweenURPFadeDestroySystem : TweenDestroySystem<TweenURPFade, float> {}
    [BurstCompile] internal partial class TweenURPBumpScaleDestroySystem : TweenDestroySystem<TweenURPBumpScale, float> {}
    [BurstCompile] internal partial class TweenURPCutoffDestroySystem : TweenDestroySystem<TweenURPCutoff, float> {}
    [BurstCompile] internal partial class TweenURPEmissionColorDestroySystem : TweenDestroySystem<TweenURPEmissionColor, float4> {}
    [BurstCompile] internal partial class TweenURPMetallicDestroySystem : TweenDestroySystem<TweenURPMetallic, float> {}
    [BurstCompile] internal partial class TweenURPOcclusionStrengthDestroySystem : TweenDestroySystem<TweenURPOcclusionStrength, float> {}
    [BurstCompile] internal partial class TweenURPSmoothnessDestroySystem : TweenDestroySystem<TweenURPSmoothness, float> {}
    [BurstCompile] internal partial class TweenURPSpecularColorDestroySystem : TweenDestroySystem<TweenURPSpecularColor, float4> {}
#elif DOTS_TWEEN_HDRP
    [BurstCompile] internal partial class TweenHDRPAlphaCutoffDestroySystem : TweenDestroySystem<TweenHDRPAlphaCutoff, float> {}
    [BurstCompile] internal partial class TweenHDRPAmbientOcclusionRemapMaxDestroySystem : TweenDestroySystem<TweenHDRPAmbientOcclusionRemapMax, float> {}
    [BurstCompile] internal partial class TweenHDRPAmbientOcclusionRemapMinDestroySystem : TweenDestroySystem<TweenHDRPAmbientOcclusionRemapMin, float> {}
    [BurstCompile] internal partial class TweenHDRPDetailAlbedoScaleDestroySystem : TweenDestroySystem<TweenHDRPDetailAlbedoScale, float> {}
    [BurstCompile] internal partial class TweenHDRPDetailNormalScaleDestroySystem : TweenDestroySystem<TweenHDRPDetailNormalScale, float> {}
    [BurstCompile] internal partial class TweenHDRPDetailSmoothnessScaleDestroySystem : TweenDestroySystem<TweenHDRPDetailSmoothnessScale, float> {}
    [BurstCompile] internal partial class TweenHDRPDiffusionProfileHashDestroySystem : TweenDestroySystem<TweenHDRPDiffusionProfileHash, float> {}
    [BurstCompile] internal partial class TweenHDRPEmissiveColorDestroySystem : TweenDestroySystem<TweenHDRPEmissiveColor, float3> {}
    [BurstCompile] internal partial class TweenHDRPMetallicDestroySystem : TweenDestroySystem<TweenHDRPMetallic, float> {}
    [BurstCompile] internal partial class TweenHDRPSmoothnessDestroySystem : TweenDestroySystem<TweenHDRPSmoothness, float> {}
    [BurstCompile] internal partial class TweenHDRPSmoothnessRemapMaxDestroySystem : TweenDestroySystem<TweenHDRPSmoothnessRemapMax, float> {}
    [BurstCompile] internal partial class TweenHDRPSmoothnessRemapMinDestroySystem : TweenDestroySystem<TweenHDRPSmoothnessRemapMin, float> {}
    [BurstCompile] internal partial class TweenHDRPSpecularColorDestroySystem : TweenDestroySystem<TweenHDRPSpecularColor, float4> {}
    [BurstCompile] internal partial class TweenHDRPThicknessDestroySystem : TweenDestroySystem<TweenHDRPThickness, float> {}
    [BurstCompile] internal partial class TweenHDRPThicknessRemapDestroySystem : TweenDestroySystem<TweenHDRPThicknessRemap, float4> {}
    [BurstCompile] internal partial class TweenHDRPTintDestroySystem : TweenDestroySystem<TweenHDRPTint, float4> {}
    [BurstCompile] internal partial class TweenHDRPTintUnlitDestroySystem : TweenDestroySystem<TweenHDRPTintUnlit, float4> {}
    [BurstCompile] internal partial class TweenHDRPFadeDestroySystem : TweenDestroySystem<TweenHDRPFade, float> {}
    [BurstCompile] internal partial class TweenHDRPFadeUnlitDestroySystem : TweenDestroySystem<TweenHDRPFadeUnlit, float> {}
#endif
}