#if DOTS_TWEEN_URP
using DotsTween.Math;
using DotsTween.Tweens;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using UnityEngine;
using System.Runtime.CompilerServices;

namespace DotsTween
{
    public static partial class Tween
    {
        public static partial class URP
        {
            [BurstCompile]
            public static class SpecularColor
            {
                [BurstCompile]
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static uint FromTo(ref SystemState state, in Entity entity, in float4 start, in float4 end, in float duration, in TweenParams tweenParams = default)
                {
                    var command = new TweenURPSpecularColorCommand(entity, start, end, duration, tweenParams);
                    state.EntityManager.AddComponentData(entity, command);
                    return command.TweenParams.Id;
                }

                [BurstCompile]
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static uint FromTo(ref EntityCommandBuffer ecb, in Entity entity, in float4 start, in float4 end, in float duration, in TweenParams tweenParams = default)
                {
                    var command = new TweenURPSpecularColorCommand(entity, start, end, duration, tweenParams);
                    ecb.AddComponent(entity, command);
                    return command.TweenParams.Id;
                }

                [BurstCompile]
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static uint FromTo(ref EntityCommandBuffer.ParallelWriter ecb, in int sortKey, in Entity entity, in float4 start, in float4 end, in float duration, in TweenParams tweenParams = default)
                {
                    var command = new TweenURPSpecularColorCommand(entity, start, end, duration, tweenParams);
                    ecb.AddComponent(sortKey, entity, command);
                    return command.TweenParams.Id;
                }

                [BurstCompile]
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static uint To(ref SystemState state, in Entity entity, in float4 end, in float duration, in TweenParams tweenParams = default)
                {
                    GetCurrentValue(out var start, ref state, entity);
                    return FromTo(ref state, entity, start, end, duration, tweenParams);
                }

                [BurstCompile]
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static uint To(ref SystemState state, ref EntityCommandBuffer ecb, in Entity entity, in float4 end, in float duration, in TweenParams tweenParams = default)
                {
                    GetCurrentValue(out var start, ref state, entity);
                    return FromTo(ref ecb, entity, start, end, duration, tweenParams);
                }

                [BurstCompile]
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static uint To(ref SystemState state, ref EntityCommandBuffer.ParallelWriter ecb, in int sortKey, in Entity entity, in float4 end, in float duration, in TweenParams tweenParams = default)
                {
                    GetCurrentValue(out var start, ref state, entity);
                    return FromTo(ref ecb, sortKey, entity, start, end, duration, tweenParams);
                }

                [BurstCompile]
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static uint From(ref SystemState state, in Entity entity, in float4 start, in float duration, in TweenParams tweenParams = default)
                {
                    GetCurrentValue(out var end, ref state, entity);
                    return FromTo(ref state, entity, start, end, duration, tweenParams);
                }

                [BurstCompile]
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static uint From(ref SystemState state, ref EntityCommandBuffer ecb, in Entity entity, in float4 start, in float duration, in TweenParams tweenParams = default)
                {
                    GetCurrentValue(out var end, ref state, entity);
                    return FromTo(ref ecb, entity, start, end, duration, tweenParams);
                }

                [BurstCompile]
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static uint From(ref SystemState state, ref EntityCommandBuffer.ParallelWriter ecb, in int sortKey, in Entity entity, in float4 start, in float duration, in TweenParams tweenParams = default)
                {
                    GetCurrentValue(out var end, ref state, entity);
                    return FromTo(ref ecb, sortKey, entity, start, end, duration, tweenParams);
                }

                [BurstCompile]
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static uint FromTo(ref SystemState state, in Entity entity, in Color start, in Color end, in float duration, in TweenParams tweenParams = default)
                {
                    start.ToFloat4(out var startColour);
                    end.ToFloat4(out var endColour);
                    var command = new TweenURPSpecularColorCommand(entity, startColour, endColour, duration, tweenParams);
                    state.EntityManager.AddComponentData(entity, command);
                    return command.TweenParams.Id;
                }

                [BurstCompile]
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static uint FromTo(ref EntityCommandBuffer ecb, in Entity entity, in Color start, in Color end, in float duration, in TweenParams tweenParams = default)
                {
                    start.ToFloat4(out var startColour);
                    end.ToFloat4(out var endColour);
                    var command = new TweenURPSpecularColorCommand(entity, startColour, endColour, duration, tweenParams);
                    ecb.AddComponent(entity, command);
                    return command.TweenParams.Id;
                }

                [BurstCompile]
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static uint FromTo(ref EntityCommandBuffer.ParallelWriter ecb, in int sortKey, in Entity entity, in Color start, in Color end, in float duration, in TweenParams tweenParams = default)
                {
                    start.ToFloat4(out var startColour);
                    end.ToFloat4(out var endColour);
                    var command = new TweenURPSpecularColorCommand(entity, startColour, endColour, duration, tweenParams);
                    ecb.AddComponent(sortKey, entity, command);
                    return command.TweenParams.Id;
                }

                [BurstCompile]
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static uint To(ref SystemState state, in Entity entity, in Color end, in float duration, in TweenParams tweenParams = default)
                {
                    end.ToFloat4(out var endColour);
                    GetCurrentValue(out var start, ref state, entity);
                    return FromTo(ref state, entity, start, endColour, duration, tweenParams);
                }

                [BurstCompile]
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static uint To(ref SystemState state, ref EntityCommandBuffer ecb, in Entity entity, in Color end, in float duration, in TweenParams tweenParams = default)
                {
                    end.ToFloat4(out var endColour);
                    GetCurrentValue(out var start, ref state, entity);
                    return FromTo(ref ecb, entity, start, endColour, duration, tweenParams);
                }

                [BurstCompile]
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static uint To(ref SystemState state, ref EntityCommandBuffer.ParallelWriter ecb, in int sortKey, in Entity entity, in Color end, in float duration, in TweenParams tweenParams = default)
                {
                    end.ToFloat4(out var endColour);
                    GetCurrentValue(out var start, ref state, entity);
                    return FromTo(ref ecb, sortKey, entity, start, endColour, duration, tweenParams);
                }

                [BurstCompile]
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static uint From(ref SystemState state, in Entity entity, in Color start, in float duration, in TweenParams tweenParams = default)
                {
                    start.ToFloat4(out var startColour);
                    GetCurrentValue(out var end, ref state, entity);
                    return FromTo(ref state, entity, startColour, end, duration, tweenParams);
                }

                [BurstCompile]
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static uint From(ref SystemState state, ref EntityCommandBuffer ecb, in Entity entity, in Color start, in float duration, in TweenParams tweenParams = default)
                {
                    start.ToFloat4(out var startColour);
                    GetCurrentValue(out var end, ref state, entity);
                    return FromTo(ref ecb, entity, startColour, end, duration, tweenParams);
                }

                [BurstCompile]
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static uint From(ref SystemState state, ref EntityCommandBuffer.ParallelWriter ecb, in int sortKey, in Entity entity, in Color start, in float duration, in TweenParams tweenParams = default)
                {
                    start.ToFloat4(out var startColour);
                    GetCurrentValue(out var end, ref state, entity);
                    return FromTo(ref ecb, sortKey, entity, startColour, end, duration, tweenParams);
                }

                [BurstCompile]
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                private static void GetCurrentValue(out float4 currentValue, ref SystemState state, in Entity entity)
                {
                    currentValue = state.EntityManager.GetComponentData<URPMaterialPropertySpecColor>(entity).Value;
                }
            }
        }
    }
}
#endif