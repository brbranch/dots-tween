using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace DotsTween.Tweens
{
    [UpdateInGroup(typeof(TweenGenerateSystemGroup))]
    [BurstCompile]
    internal abstract partial class JoblessTweenGenerateSystem<TTweenCommand, TTweenInfo, TTarget, TTweenInfoValue> : SystemBase
        where TTweenCommand : unmanaged, IComponentData, ITweenParams, ITweenInfo<TTweenInfoValue>
        where TTweenInfo : unmanaged, IComponentData, ITweenId, ITweenInfo<TTweenInfoValue>
        where TTarget : unmanaged, IComponentData
        where TTweenInfoValue : unmanaged
    {
        [BurstCompile]
        protected override void OnCreate()
        {
            RequireForUpdate<TTweenCommand>();
        }

        [BurstCompile]
        protected override void OnUpdate()
        {
            double elapsedTime = SystemAPI.Time.ElapsedTime;
            var ecb = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(World.Unmanaged);
            var tweenInfoTypeIndex = TypeManager.GetTypeIndex(typeof(TTweenCommand));

            foreach (var (commandRef, entity) in SystemAPI.Query<RefRO<TTweenCommand>>().WithEntityAccess())
            {
                var chunk = EntityManager.GetChunk(entity);
                if (!chunk.Has<TweenState>())
                {
                    ecb.AddBuffer<TweenState>(entity);
                }

                if (!chunk.Has<TTarget>())
                {
                    ecb.AddComponent<TTarget>(entity);
                }

                var tweenParams = commandRef.ValueRO.GetTweenParams();
                TweenState tween = new TweenState(tweenParams, elapsedTime, entity.Index, tweenInfoTypeIndex);
                ecb.AppendToBuffer(entity, tween);
                tweenParams.OnStart.Perform(ref ecb, entity);

                TTweenInfo info = default;
                info.SetTweenId(tween.Id);
                info.SetTweenInfo(commandRef.ValueRO.GetTweenStart(), commandRef.ValueRO.GetTweenEnd());
                ecb.AddComponent(entity, info);
                ecb.RemoveComponent<TTweenCommand>(entity);
            }
        }
    }
#if DOTS_TWEEN_SPLINES
    [BurstCompile] internal partial class TweenSplineMovementGenerateSystem : JoblessTweenGenerateSystem<TweenSplineMovementCommand, TweenSplineMovement, LocalTransform, SplineTweenInfo> { }
#endif
}

