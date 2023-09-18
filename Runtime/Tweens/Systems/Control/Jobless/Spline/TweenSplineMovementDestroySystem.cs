#if DOTS_TWEEN_SPLINES
using Unity.Burst;
using Unity.Entities;

namespace DotsTween.Tweens.Spline
{
    [BurstCompile]
    [UpdateInGroup(typeof(TweenDestroySystemGroup))]
    internal partial class TweenSplineMovementDestroySystem : SystemBase
    {
        [BurstCompile]
        protected override void OnCreate()
        {
            RequireForUpdate<TweenDestroyCommand>();
            RequireForUpdate<TweenSplineMovement>();
            RequireForUpdate<TweenState>();
        }

        [BurstCompile]
        protected override void OnUpdate()
        {
            var ecb = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(World.Unmanaged);
             
            foreach (var (infoRef, destroyBuffer, tweenBuffer, entity) in SystemAPI.Query<RefRW<TweenSplineMovement>, DynamicBuffer<TweenDestroyCommand>, DynamicBuffer<TweenState>>().WithEntityAccess())
            {
                if (destroyBuffer.IsEmpty) continue;
                
                bool shouldDestroy = false;
                for (int j = destroyBuffer.Length - 1; j >= 0; j--)
                {
                    TweenDestroyCommand command = destroyBuffer[j];
                    if (infoRef.ValueRO.GetTweenId() == command.TweenId)
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

                for (int j = tweenBuffer.Length - 1; j >= 0; j--)
                {
                    TweenState tween = tweenBuffer[j];
                    if (infoRef.ValueRO.GetTweenId() == tween.Id)
                    {
                        tweenBuffer[j].Settings.OnComplete.Perform(ref ecb, entity);
                        tweenBuffer.RemoveAt(j);
                        infoRef.ValueRW.Cleanup();
                        ecb.RemoveComponent<TweenSplineMovement>(entity);
                        break;
                    }
                }
            }
        }
    }
}
#endif