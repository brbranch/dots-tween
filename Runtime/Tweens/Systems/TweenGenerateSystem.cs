using Unity.Burst;
using Unity.Burst.Intrinsics;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

#if UNITY_TINY_ALL_0_31_0
using Unity.Tiny;
#elif UNITY_2D_ENTITIES
using Unity.U2D.Entities;
#endif

[assembly: RegisterGenericJobType(typeof(DotsTween.Tweens.TweenTranslationGenerateSystem.GenerateJob))]
[assembly: RegisterGenericJobType(typeof(DotsTween.Tweens.TweenRotationGenerateSystem.GenerateJob))]
[assembly: RegisterGenericJobType(typeof(DotsTween.Tweens.TweenScaleGenerateSystem.GenerateJob))]
[assembly: RegisterGenericJobType(typeof(DotsTween.Tweens.TweenNonUniformScaleGenerateSystem.GenerateJob))]

#if UNITY_TINY_ALL_0_31_0 || UNITY_2D_ENTITIES
[assembly: RegisterGenericJobType(typeof(Timespawn.EntityTween.Tweens.TweenTintGenerateSystem.GenerateJob))]
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
            [ReadOnly] public int TweenInfoTypeIndex;
            [ReadOnly] public double ElapsedTime;
            [ReadOnly] public EntityTypeHandle EntityType;
            [ReadOnly] public ComponentTypeHandle<TTweenCommand> TweenCommandType;
            [ReadOnly] public ComponentTypeHandle<TTarget> TargetType;
            [ReadOnly] public BufferTypeHandle<TweenState> TweenBufferType;

            public EntityCommandBuffer.ParallelWriter ParallelWriter;

            [BurstCompile]
            public void Execute(in ArchetypeChunk chunk, int chunkIndex, bool useEnabledMask, in v128 chunkEnabledMask)
            {
                bool hasTweenBuffer = chunk.Has(ref TweenBufferType);
                bool hasTargetType = chunk.Has(ref TargetType);

                NativeArray<Entity> entities = chunk.GetNativeArray(EntityType);
                NativeArray<TTweenCommand> commands = chunk.GetNativeArray(ref TweenCommandType);
                for (int i = 0; i < entities.Length; i++)
                {
                    Entity entity = entities[i];
                    TTweenCommand command = commands[i];

                    if (!hasTweenBuffer)
                    {
                        ParallelWriter.AddBuffer<TweenState>(chunkIndex, entity);
                        break;
                    }

                    if (!hasTargetType)
                    {
                        ParallelWriter.AddComponent<TTarget>(chunkIndex, entity);
                    }

                    TweenState tween = new TweenState(command.GetTweenParams(), ElapsedTime, chunkIndex, TweenInfoTypeIndex);
                    ParallelWriter.AppendToBuffer(chunkIndex, entity, tween);

                    TTweenInfo info = default;
                    info.SetTweenId(tween.Id);
                    info.SetTweenInfo(command.GetTweenStart(), command.GetTweenEnd());
                    ParallelWriter.AddComponent(chunkIndex, entity, info);

                    ParallelWriter.RemoveComponent<TTweenCommand>(chunkIndex, entity);
                }
            }
        }

        private EntityQuery tweenCommandQuery;

        [BurstCompile]
        protected override void OnCreate()
        {
            tweenCommandQuery = GetEntityQuery(ComponentType.ReadOnly<TTweenCommand>());
        }

        [BurstCompile]
        protected override void OnUpdate()
        {
            double elapsedTime = SystemAPI.Time.ElapsedTime;
            EndSimulationEntityCommandBufferSystem endSimECBSystem = World.GetOrCreateSystemManaged<EndSimulationEntityCommandBufferSystem>();

            GenerateJob job = new GenerateJob
            {
                TweenInfoTypeIndex = TypeManager.GetTypeIndex(typeof(TTweenInfo)),
                ElapsedTime = elapsedTime,
                EntityType = GetEntityTypeHandle(),
                TweenCommandType = GetComponentTypeHandle<TTweenCommand>(true),
                TargetType = GetComponentTypeHandle<TTarget>(true),
                TweenBufferType = GetBufferTypeHandle<TweenState>(true),
                ParallelWriter = endSimECBSystem.CreateCommandBuffer().AsParallelWriter(),
            };

            Dependency = job.ScheduleParallel(tweenCommandQuery, Dependency);
            endSimECBSystem.AddJobHandleForProducer(Dependency);
        }
    }

    [BurstCompile]
    internal partial class TweenTranslationGenerateSystem : TweenGenerateSystem<TweenTranslationCommand, TweenTranslation, LocalTransform, float3> { }

    [BurstCompile]
    internal partial class TweenRotationGenerateSystem : TweenGenerateSystem<TweenRotationCommand, TweenRotation, LocalTransform, quaternion> { }

    [BurstCompile]
    internal partial class TweenScaleGenerateSystem : TweenGenerateSystem<TweenScaleCommand, TweenScale, LocalTransform, float> { }
    [BurstCompile]
    internal partial class TweenNonUniformScaleGenerateSystem : TweenGenerateSystem<TweenNonUniformScaleCommand, TweenNonUniformScale, PostTransformScale, float3> { }

#if UNITY_TINY_ALL_0_31_0 || UNITY_2D_ENTITIES
    internal class TweenTintGenerateSystem : TweenGenerateSystem<TweenTintCommand, TweenTint, SpriteRenderer, float4> {}
#endif
}