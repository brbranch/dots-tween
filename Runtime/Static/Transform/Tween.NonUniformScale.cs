using System.Runtime.CompilerServices;
using DotsTween.Tweens;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace DotsTween
{
    public static partial class Tween
    {
        [BurstCompile]
        public static class NonUniformScale
        {
            [BurstCompile]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static uint FromTo(ref SystemState state, in Entity entity, in float3 start, in float3 end, in float duration, in TweenParams tweenParams = default)
            {
                var command = new TweenNonUniformScaleCommand(entity, start, end, duration, tweenParams);
                state.EntityManager.AddComponentData(entity, command);
                return command.TweenParams.Id;
            }

            [BurstCompile]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static uint FromTo(ref EntityCommandBuffer ecb, in Entity entity, in float3 start, in float3 end, in float duration, in TweenParams tweenParams = default)
            {
                var command = new TweenNonUniformScaleCommand(entity, start, end, duration, tweenParams);
                ecb.AddComponent(entity, command);
                return command.TweenParams.Id;
            }

            [BurstCompile]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static uint FromTo(ref EntityCommandBuffer.ParallelWriter ecb, in int sortKey, in Entity entity, in float3 start, in float3 end, in float duration, in TweenParams tweenParams = default)
            {
                var command = new TweenNonUniformScaleCommand(entity, start, end, duration, tweenParams);
                ecb.AddComponent(sortKey, entity, command);
                return command.TweenParams.Id;
            }

            [BurstCompile]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static uint To(ref SystemState state, in Entity entity, in float3 end, in float duration, in TweenParams tweenParams = default)
            {
                GetCurrentValue(out var start, ref state, entity);
                return FromTo(ref state, entity, start, end, duration, tweenParams);
            }

            [BurstCompile]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static uint To(ref SystemState state, ref EntityCommandBuffer ecb, in Entity entity, in float3 end, in float duration, in TweenParams tweenParams = default)
            {
                GetCurrentValue(out var start, ref state, entity);
                return FromTo(ref ecb, entity, start, end, duration, tweenParams);
            }

            [BurstCompile]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static uint To(ref SystemState state, ref EntityCommandBuffer.ParallelWriter ecb, in int sortKey, in Entity entity, in float3 end, in float duration, in TweenParams tweenParams = default)
            {
                GetCurrentValue(out var start, ref state, entity);
                return FromTo(ref ecb, sortKey, entity, start, end, duration, tweenParams);
            }

            [BurstCompile]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static uint From(ref SystemState state, in Entity entity, in float3 start, in float duration, in TweenParams tweenParams = default)
            {
                GetCurrentValue(out var end, ref state, entity);
                return FromTo(ref state, entity, start, end, duration, tweenParams);
            }

            [BurstCompile]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static uint From(ref SystemState state, ref EntityCommandBuffer ecb, in Entity entity, in float3 start, in float duration, in TweenParams tweenParams = default)
            {
                GetCurrentValue(out var end, ref state, entity);
                return FromTo(ref ecb, entity, start, end, duration, tweenParams);
            }

            [BurstCompile]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static uint From(ref SystemState state, ref EntityCommandBuffer.ParallelWriter ecb, in int sortKey, in Entity entity, in float3 start, in float duration, in TweenParams tweenParams = default)
            {
                GetCurrentValue(out var end, ref state, entity);
                return FromTo(ref ecb, sortKey, entity, start, end, duration, tweenParams);
            }

            [BurstCompile]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static void GetCurrentValue(out float3 currentValue, ref SystemState state, in Entity entity)
            {
                if (!state.EntityManager.HasComponent<PostTransformMatrix>(entity))
                {
                    currentValue = new float3(1f, 1f, 1f);
                    return;
                }

                var matrix = state.EntityManager.GetComponentData<PostTransformMatrix>(entity).Value;
                currentValue = new float3(matrix.c0.x, matrix.c1.y, matrix.c2.z);
            }
        }
    }
}