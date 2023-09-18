#if DOTS_TWEEN_SPLINES
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace DotsTween.Tweens.Spline
{
    [BurstCompile]
    internal partial class TweenSplineMovementGenerateSystem : JoblessTweenGenerateSystem
    {
        [BurstCompile]
        protected override void OnCreate()
        {
            RequireForUpdate<TweenSplineMovementCommand>();
        }

        [BurstCompile]
        protected override void OnUpdate()
        {
            var ecb = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(World.Unmanaged);

            foreach (var (commandRef, entity) in SystemAPI.Query<RefRW<TweenSplineMovementCommand>>().WithEntityAccess())
            {
                var chunk = EntityManager.GetChunk(entity);

                bool hasTweenStateBuffer = chunk.Has<TweenState>();
                bool hasTweenPauseBuffer = chunk.Has<TweenPauseInfo>();
                bool hasTweenResumeBuffer = chunk.Has<TweenResumeInfo>();

                TryToAddBuffers(out var buffersWereAdded, ref ecb, entity, hasTweenStateBuffer, hasTweenPauseBuffer, hasTweenResumeBuffer);

                if (buffersWereAdded)
                {
                    break;
                }

                if (!chunk.Has<LocalTransform>())
                {
                    ecb.AddComponent<LocalTransform>(entity);
                }

                var tweenParams = commandRef.ValueRO.GetTweenParams();
                TweenState tween = new TweenState(tweenParams);
                ecb.AppendToBuffer(entity, tween);
                tweenParams.OnStart.Perform(ref ecb, entity);

                TweenSplineMovement info = default;
                info.SetTweenId(tween.Id);
                info.SetTweenInfo(commandRef.ValueRO.GetTweenStart(), commandRef.ValueRO.GetTweenEnd());

                ecb.AddComponent(entity, info);
                ecb.RemoveComponent<TweenSplineMovementCommand>(entity);
            }
        }
    }
}
#endif