#if DOTS_TWEEN_HDRP
using DotsTween.Math;
using DotsTween.Tweens;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using UnityEngine;

namespace DotsTween
{
    public static partial class Tween
    {
        public static partial class HDRP
        {
            [BurstCompile]
            public static class EmissiveColor
            {
                #region float4
                [BurstCompile]
                public static uint FromTo(ref SystemState state, in Entity entity, in float4 start, in float4 end, in float duration, in TweenParams tweenParams = default)
                {
                    var command = new TweenHDRPEmissiveColorCommand(entity, start.xyz, end.xyz, duration, tweenParams);
                    state.EntityManager.AddComponentData(entity, command);
                    return command.TweenParams.Id;
                }

                [BurstCompile]
                public static uint FromTo(ref EntityCommandBuffer ecb, in Entity entity, in float4 start, in float4 end, in float duration, in TweenParams tweenParams = default)
                {
                    var command = new TweenHDRPEmissiveColorCommand(entity, start.xyz, end.xyz, duration, tweenParams);
                    ecb.AddComponent(entity, command);
                    return command.TweenParams.Id;
                }

                [BurstCompile]
                public static uint FromTo(ref EntityCommandBuffer.ParallelWriter ecb, in int sortKey, in Entity entity, in float4 start, in float4 end, in float duration, in TweenParams tweenParams = default)
                {
                    var command = new TweenHDRPEmissiveColorCommand(entity, start.xyz, end.xyz, duration, tweenParams);
                    ecb.AddComponent(sortKey, entity, command);
                    return command.TweenParams.Id;
                }

                [BurstCompile]
                public static uint To(ref SystemState state, in Entity entity, in float4 end, in float duration, in TweenParams tweenParams = default)
                {
                    GetCurrentValue(out var start, ref state, entity);
                    return FromTo(ref state, entity, start, end.xyz, duration, tweenParams);
                }

                [BurstCompile]
                public static uint To(ref SystemState state, ref EntityCommandBuffer ecb, in Entity entity, in float4 end, in float duration, in TweenParams tweenParams = default)
                {
                    GetCurrentValue(out var start, ref state, entity);
                    return FromTo(ref ecb, entity, start, end.xyz, duration, tweenParams);
                }

                [BurstCompile]
                public static uint To(ref SystemState state, ref EntityCommandBuffer.ParallelWriter ecb, in int sortKey, in Entity entity, in float4 end, in float duration, in TweenParams tweenParams = default)
                {
                    GetCurrentValue(out var start, ref state, entity);
                    return FromTo(ref ecb, sortKey, entity, start, end.xyz, duration, tweenParams);
                }

                [BurstCompile]
                public static uint From(ref SystemState state, in Entity entity, in float4 start, in float duration, in TweenParams tweenParams = default)
                {
                    GetCurrentValue(out var end, ref state, entity);
                    return FromTo(ref state, entity, start.xyz, end, duration, tweenParams);
                }

                [BurstCompile]
                public static uint From(ref SystemState state, ref EntityCommandBuffer ecb, in Entity entity, in float4 start, in float duration, in TweenParams tweenParams = default)
                {
                    GetCurrentValue(out var end, ref state, entity);
                    return FromTo(ref ecb, entity, start.xyz, end, duration, tweenParams);
                }

                [BurstCompile]
                public static uint From(ref SystemState state, ref EntityCommandBuffer.ParallelWriter ecb, in int sortKey, in Entity entity, in float4 start, in float duration, in TweenParams tweenParams = default)
                {
                    GetCurrentValue(out var end, ref state, entity);
                    return FromTo(ref ecb, sortKey, entity, start.xyz, end, duration, tweenParams);
                }
                #endregion
                
                #region float3
                [BurstCompile]
                public static uint FromTo(ref SystemState state, in Entity entity, in float3 start, in float3 end, in float duration, in TweenParams tweenParams = default)
                {
                    var command = new TweenHDRPEmissiveColorCommand(entity, start, end, duration, tweenParams);
                    state.EntityManager.AddComponentData(entity, command);
                    return command.TweenParams.Id;
                }

                [BurstCompile]
                public static uint FromTo(ref EntityCommandBuffer ecb, in Entity entity, in float3 start, in float3 end, in float duration, in TweenParams tweenParams = default)
                {
                    var command = new TweenHDRPEmissiveColorCommand(entity, start, end, duration, tweenParams);
                    ecb.AddComponent(entity, command);
                    return command.TweenParams.Id;
                }

                [BurstCompile]
                public static uint FromTo(ref EntityCommandBuffer.ParallelWriter ecb, in int sortKey, in Entity entity, in float3 start, in float3 end, in float duration, in TweenParams tweenParams = default)
                {
                    var command = new TweenHDRPEmissiveColorCommand(entity, start, end, duration, tweenParams);
                    ecb.AddComponent(sortKey, entity, command);
                    return command.TweenParams.Id;
                }

                [BurstCompile]
                public static uint To(ref SystemState state, in Entity entity, in float3 end, in float duration, in TweenParams tweenParams = default)
                {
                    GetCurrentValue(out var start, ref state, entity);
                    return FromTo(ref state, entity, start, end, duration, tweenParams);
                }

                [BurstCompile]
                public static uint To(ref SystemState state, ref EntityCommandBuffer ecb, in Entity entity, in float3 end, in float duration, in TweenParams tweenParams = default)
                {
                    GetCurrentValue(out var start, ref state, entity);
                    return FromTo(ref ecb, entity, start, end, duration, tweenParams);
                }

                [BurstCompile]
                public static uint To(ref SystemState state, ref EntityCommandBuffer.ParallelWriter ecb, in int sortKey, in Entity entity, in float3 end, in float duration, in TweenParams tweenParams = default)
                {
                    GetCurrentValue(out var start, ref state, entity);
                    return FromTo(ref ecb, sortKey, entity, start, end, duration, tweenParams);
                }

                [BurstCompile]
                public static uint From(ref SystemState state, in Entity entity, in float3 start, in float duration, in TweenParams tweenParams = default)
                {
                    GetCurrentValue(out var end, ref state, entity);
                    return FromTo(ref state, entity, start, end, duration, tweenParams);
                }

                [BurstCompile]
                public static uint From(ref SystemState state, ref EntityCommandBuffer ecb, in Entity entity, in float3 start, in float duration, in TweenParams tweenParams = default)
                {
                    GetCurrentValue(out var end, ref state, entity);
                    return FromTo(ref ecb, entity, start, end, duration, tweenParams);
                }

                [BurstCompile]
                public static uint From(ref SystemState state, ref EntityCommandBuffer.ParallelWriter ecb, in int sortKey, in Entity entity, in float3 start, in float duration, in TweenParams tweenParams = default)
                {
                    GetCurrentValue(out var end, ref state, entity);
                    return FromTo(ref ecb, sortKey, entity, start, end, duration, tweenParams);
                }
                #endregion

                #region Color
                [BurstCompile]
                public static uint FromTo(ref SystemState state, in Entity entity, in Color start, in Color end, in float duration, in TweenParams tweenParams = default)
                {
                    var command = new TweenHDRPEmissiveColorCommand(entity, start.ToFloat3(), end.ToFloat3(), duration, tweenParams);
                    state.EntityManager.AddComponentData(entity, command);
                    return command.TweenParams.Id;
                }

                [BurstCompile]
                public static uint FromTo(ref EntityCommandBuffer ecb, in Entity entity, in Color start, in Color end, in float duration, in TweenParams tweenParams = default)
                {
                    var command = new TweenHDRPEmissiveColorCommand(entity, start.ToFloat3(), end.ToFloat3(), duration, tweenParams);
                    ecb.AddComponent(entity, command);
                    return command.TweenParams.Id;
                }

                [BurstCompile]
                public static uint FromTo(ref EntityCommandBuffer.ParallelWriter ecb, in int sortKey, in Entity entity, in Color start, in Color end, in float duration, in TweenParams tweenParams = default)
                {
                    var command = new TweenHDRPEmissiveColorCommand(entity, start.ToFloat3(), end.ToFloat3(), duration, tweenParams);
                    ecb.AddComponent(sortKey, entity, command);
                    return command.TweenParams.Id;
                }

                [BurstCompile]
                public static uint To(ref SystemState state, in Entity entity, in Color end, in float duration, in TweenParams tweenParams = default)
                {
                    GetCurrentValue(out var start, ref state, entity);
                    return FromTo(ref state, entity, start, end.ToFloat3(), duration, tweenParams);
                }

                [BurstCompile]
                public static uint To(ref SystemState state, ref EntityCommandBuffer ecb, in Entity entity, in Color end, in float duration, in TweenParams tweenParams = default)
                {
                    GetCurrentValue(out var start, ref state, entity);
                    return FromTo(ref ecb, entity, start, end.ToFloat3(), duration, tweenParams);
                }

                [BurstCompile]
                public static uint To(ref SystemState state, ref EntityCommandBuffer.ParallelWriter ecb, in int sortKey, in Entity entity, in Color end, in float duration, in TweenParams tweenParams = default)
                {
                    GetCurrentValue(out var start, ref state, entity);
                    return FromTo(ref ecb, sortKey, entity, start, end.ToFloat3(), duration, tweenParams);
                }

                [BurstCompile]
                public static uint From(ref SystemState state, in Entity entity, in Color start, in float duration, in TweenParams tweenParams = default)
                {
                    GetCurrentValue(out var end, ref state, entity);
                    return FromTo(ref state, entity, start.ToFloat3(), end, duration, tweenParams);
                }

                [BurstCompile]
                public static uint From(ref SystemState state, ref EntityCommandBuffer ecb, in Entity entity, in Color start, in float duration, in TweenParams tweenParams = default)
                {
                    GetCurrentValue(out var end, ref state, entity);
                    return FromTo(ref ecb, entity, start.ToFloat3(), end, duration, tweenParams);
                }

                [BurstCompile]
                public static uint From(ref SystemState state, ref EntityCommandBuffer.ParallelWriter ecb, in int sortKey, in Entity entity, in Color start, in float duration, in TweenParams tweenParams = default)
                {
                    GetCurrentValue(out var end, ref state, entity);
                    return FromTo(ref ecb, sortKey, entity, start.ToFloat3(), end, duration, tweenParams);
                }
                #endregion

                [BurstCompile]
                private static void GetCurrentValue(out float3 currentValue, ref SystemState state, in Entity entity)
                {
                    currentValue = state.EntityManager.GetComponentData<HDRPMaterialPropertyEmissiveColor>(entity).Value;
                }
            }
        }
    }
}
#endif