using System.Runtime.InteropServices;
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
            var ecb = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(World.Unmanaged);

            foreach (var (commandRef, entity) in SystemAPI.Query<RefRW<TTweenCommand>>().WithEntityAccess())
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

                if (!chunk.Has<TTarget>())
                {
                    ecb.AddComponent<TTarget>(entity);
                }

                var tweenParams = commandRef.ValueRO.GetTweenParams();
                TweenState tween = new TweenState(tweenParams);
                ecb.AppendToBuffer(entity, tween);
                tweenParams.OnStart.Perform(ref ecb, entity);

                TTweenInfo info = default;
                info.SetTweenId(tween.Id);
                info.SetTweenInfo(commandRef.ValueRO.GetTweenStart(), commandRef.ValueRO.GetTweenEnd());

                commandRef.ValueRW.Cleanup();
                ecb.AddComponent(entity, info);
                ecb.RemoveComponent<TTweenCommand>(entity);
            }
        }

        [BurstCompile]
        private void TryToAddBuffers(
            out bool buffersWereAdded,
            ref EntityCommandBuffer ecb,
            in Entity entity,
            [MarshalAs(UnmanagedType.U1)] bool hasTweenStateBuffer,
            [MarshalAs(UnmanagedType.U1)] bool hasTweenPauseBuffer,
            [MarshalAs(UnmanagedType.U1)] bool hasTweenResumeBuffer)
        {
            if (!hasTweenStateBuffer) ecb.AddBuffer<TweenState>(entity);
            if (!hasTweenPauseBuffer) ecb.AddBuffer<TweenPauseInfo>(entity);
            if (!hasTweenResumeBuffer) ecb.AddBuffer<TweenResumeInfo>(entity);

            buffersWereAdded = !hasTweenStateBuffer
                || !hasTweenPauseBuffer
                || !hasTweenResumeBuffer;
        }
    }
#if DOTS_TWEEN_SPLINES
    [BurstCompile] internal partial class TweenSplineMovementGenerateSystem : JoblessTweenGenerateSystem<TweenSplineMovementCommand, TweenSplineMovement, LocalTransform, SplineTweenInfo> { }
#endif
}