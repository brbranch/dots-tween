using Unity.Burst;
using Unity.Entities;

namespace DotsTween.Tweens
{
    [BurstCompile]
    [UpdateInGroup(typeof(TweenDestroySystemGroup))]
     internal partial class JoblessTweenDestroySystem<TTweenInfo, TTweenInfoValue> : SystemBase
         where TTweenInfo : unmanaged, IComponentData, ITweenId, ITweenInfo<TTweenInfoValue>
     {
         [BurstCompile]
         protected override void OnCreate()
         {
             RequireForUpdate<TweenDestroyCommand>();
             RequireForUpdate<TTweenInfo>();
             RequireForUpdate<TweenState>();
         }

         [BurstCompile]
         protected override void OnUpdate()
         {
             var ecb = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(World.Unmanaged);
             var tweenInfoTypeIndex = TypeManager.GetTypeIndex(typeof(TTweenInfo));
             
             foreach (var (infoRef, destroyBuffer, tweenBuffer, entity) in SystemAPI.Query<RefRW<TTweenInfo>, DynamicBuffer<TweenDestroyCommand>, DynamicBuffer<TweenState>>().WithEntityAccess())
             {
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
                         ecb.RemoveComponent<TTweenInfo>(entity);
                         break;
                     }
                 }

                 if (tweenBuffer.IsEmpty)
                 {
                     ecb.RemoveComponent<TweenState>(entity);
                 }

                 if (destroyBuffer.IsEmpty)
                 {
                     ecb.RemoveComponent<TweenDestroyCommand>(entity);
                 }
             }
         }
     }
     
#if DOTS_TWEEN_SPLINES
    [BurstCompile] internal partial class TweenSplineMovementDestroySystem : JoblessTweenDestroySystem<TweenSplineMovement, SplineTweenInfo> { }
#endif
}