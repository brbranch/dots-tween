#if DOTS_TWEEN_HDRP
using DotsTween.Tweens;
using Unity.Burst;
using Unity.Entities;
using Unity.Rendering;
using Unity.Transforms;

namespace DotsTween
{
    public static partial class Tween
    {
        public static partial class HDRP
        {
            [BurstCompile]
            public static class Smoothness
            {
                [BurstCompile]
                public static uint FromTo(ref SystemState state, in Entity entity, in float start, in float end, in float duration, in TweenParams tweenParams = default)
                {
                    var command = new TweenHDRPSmoothnessCommand(entity, start, end, duration, tweenParams);
                    state.EntityManager.AddComponentData(entity, command);
                    return command.TweenParams.Id;
                }

                [BurstCompile]
                public static uint FromTo(ref EntityCommandBuffer ecb, in Entity entity, in float start, in float end, in float duration, in TweenParams tweenParams = default)
                {
                    var command = new TweenHDRPSmoothnessCommand(entity, start, end, duration, tweenParams);
                    ecb.AddComponent(entity, command);
                    return command.TweenParams.Id;
                }

                [BurstCompile]
                public static uint FromTo(ref EntityCommandBuffer.ParallelWriter ecb, in int sortKey, in Entity entity, in float start, in float end, in float duration, in TweenParams tweenParams = default)
                {
                    var command = new TweenHDRPSmoothnessCommand(entity, start, end, duration, tweenParams);
                    ecb.AddComponent(sortKey, entity, command);
                    return command.TweenParams.Id;
                }

                [BurstCompile]
                public static uint To(ref SystemState state, in Entity entity, in float end, in float duration, in TweenParams tweenParams = default)
                {
                    GetCurrentValue(out var start, ref state, entity);
                    return FromTo(ref state, entity, start, end, duration, tweenParams);
                }

                [BurstCompile]
                public static uint To(ref SystemState state, ref EntityCommandBuffer ecb, in Entity entity, in float end, in float duration, in TweenParams tweenParams = default)
                {
                    GetCurrentValue(out var start, ref state, entity);
                    return FromTo(ref ecb, entity, start, end, duration, tweenParams);
                }

                [BurstCompile]
                public static uint To(ref SystemState state, ref EntityCommandBuffer.ParallelWriter ecb, in int sortKey, in Entity entity, in float end, in float duration, in TweenParams tweenParams = default)
                {
                    GetCurrentValue(out var start, ref state, entity);
                    return FromTo(ref ecb, sortKey, entity, start, end, duration, tweenParams);
                }

                [BurstCompile]
                public static uint From(ref SystemState state, in Entity entity, in float start, in float duration, in TweenParams tweenParams = default)
                {
                    GetCurrentValue(out var end, ref state, entity);
                    return FromTo(ref state, entity, start, end, duration, tweenParams);
                }

                [BurstCompile]
                public static uint From(ref SystemState state, ref EntityCommandBuffer ecb, in Entity entity, in float start, in float duration, in TweenParams tweenParams = default)
                {
                    GetCurrentValue(out var end, ref state, entity);
                    return FromTo(ref ecb, entity, start, end, duration, tweenParams);
                }

                [BurstCompile]
                public static uint From(ref SystemState state, ref EntityCommandBuffer.ParallelWriter ecb, in int sortKey, in Entity entity, in float start, in float duration, in TweenParams tweenParams = default)
                {
                    GetCurrentValue(out var end, ref state, entity);
                    return FromTo(ref ecb, sortKey, entity, start, end, duration, tweenParams);
                }

                [BurstCompile]
                private static void GetCurrentValue(out float currentValue, ref SystemState state, in Entity entity)
                {
                    currentValue = state.EntityManager.GetComponentData<HDRPMaterialPropertySmoothness>(entity).Value;
                }
            }
        }
    }
}
#endif