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
        public static partial class URP
        {
            [BurstCompile]
            public static class Tint
            {
                [BurstCompile]
                public static void FromTo(ref SystemState state, in Entity entity, in float4 start, in float4 end, in float duration, in TweenParams tweenParams = default)
                {
                    state.EntityManager.AddComponentData(entity, new TweenURPTintCommand(start, end, duration, tweenParams));
                }

                [BurstCompile]
                public static void FromTo(ref EntityCommandBuffer ecb, in Entity entity, in float4 start, in float4 end, in float duration, in TweenParams tweenParams = default)
                {
                    ecb.AddComponent(entity, new TweenURPTintCommand(start, end, duration, tweenParams));
                }

                [BurstCompile]
                public static void FromTo(ref EntityCommandBuffer.ParallelWriter ecb, in int sortKey, in Entity entity, in float4 start, in float4 end, in float duration, in TweenParams tweenParams = default)
                {
                    ecb.AddComponent(sortKey, entity, new TweenURPTintCommand(start, end, duration, tweenParams));
                }

                [BurstCompile]
                public static void To(ref SystemState state, in Entity entity, in float4 end, in float duration, in TweenParams tweenParams = default)
                {
                    GetCurrentValue(out var start, ref state, entity);
                    FromTo(ref state, entity, start, end, duration, tweenParams);
                }

                [BurstCompile]
                public static void To(ref SystemState state, ref EntityCommandBuffer ecb, in Entity entity, in float4 end, in float duration, in TweenParams tweenParams = default)
                {
                    GetCurrentValue(out var start, ref state, entity);
                    FromTo(ref ecb, entity, start, end, duration, tweenParams);
                }

                [BurstCompile]
                public static void To(ref SystemState state, ref EntityCommandBuffer.ParallelWriter ecb, in int sortKey, in Entity entity, in float4 end, in float duration, in TweenParams tweenParams = default)
                {
                    GetCurrentValue(out var start, ref state, entity);
                    FromTo(ref ecb, sortKey, entity, start, end, duration, tweenParams);
                }

                [BurstCompile]
                public static void From(ref SystemState state, in Entity entity, in float4 start, in float duration, in TweenParams tweenParams = default)
                {
                    GetCurrentValue(out var end, ref state, entity);
                    FromTo(ref state, entity, start, end, duration, tweenParams);
                }

                [BurstCompile]
                public static void From(ref SystemState state, ref EntityCommandBuffer ecb, in Entity entity, in float4 start, in float duration, in TweenParams tweenParams = default)
                {
                    GetCurrentValue(out var end, ref state, entity);
                    FromTo(ref ecb, entity, start, end, duration, tweenParams);
                }

                [BurstCompile]
                public static void From(ref SystemState state, ref EntityCommandBuffer.ParallelWriter ecb, in int sortKey, in Entity entity, in float4 start, in float duration, in TweenParams tweenParams = default)
                {
                    GetCurrentValue(out var end, ref state, entity);
                    FromTo(ref ecb, sortKey, entity, start, end, duration, tweenParams);
                }

                [BurstCompile]
                public static void FromTo(ref SystemState state, in Entity entity, in Color start, in Color end, in float duration, in TweenParams tweenParams = default)
                {
                    state.EntityManager.AddComponentData(entity, new TweenURPTintCommand(start.ToFloat4(), end.ToFloat4(), duration, tweenParams));
                }

                [BurstCompile]
                public static void FromTo(ref EntityCommandBuffer ecb, in Entity entity, in Color start, in Color end, in float duration, in TweenParams tweenParams = default)
                {
                    ecb.AddComponent(entity, new TweenURPTintCommand(start.ToFloat4(), end.ToFloat4(), duration, tweenParams));
                }

                [BurstCompile]
                public static void FromTo(ref EntityCommandBuffer.ParallelWriter ecb, in int sortKey, in Entity entity, in Color start, in Color end, in float duration, in TweenParams tweenParams = default)
                {
                    ecb.AddComponent(sortKey, entity, new TweenURPTintCommand(start.ToFloat4(), end.ToFloat4(), duration, tweenParams));
                }

                [BurstCompile]
                public static void To(ref SystemState state, in Entity entity, in Color end, in float duration, in TweenParams tweenParams = default)
                {
                    GetCurrentValue(out var start, ref state, entity);
                    FromTo(ref state, entity, start, end.ToFloat4(), duration, tweenParams);
                }

                [BurstCompile]
                public static void To(ref SystemState state, ref EntityCommandBuffer ecb, in Entity entity, in Color end, in float duration, in TweenParams tweenParams = default)
                {
                    GetCurrentValue(out var start, ref state, entity);
                    FromTo(ref ecb, entity, start, end.ToFloat4(), duration, tweenParams);
                }

                [BurstCompile]
                public static void To(ref SystemState state, ref EntityCommandBuffer.ParallelWriter ecb, in int sortKey, in Entity entity, in Color end, in float duration, in TweenParams tweenParams = default)
                {
                    GetCurrentValue(out var start, ref state, entity);
                    FromTo(ref ecb, sortKey, entity, start, end.ToFloat4(), duration, tweenParams);
                }

                [BurstCompile]
                public static void From(ref SystemState state, in Entity entity, in Color start, in float duration, in TweenParams tweenParams = default)
                {
                    GetCurrentValue(out var end, ref state, entity);
                    FromTo(ref state, entity, start.ToFloat4(), end, duration, tweenParams);
                }

                [BurstCompile]
                public static void From(ref SystemState state, ref EntityCommandBuffer ecb, in Entity entity, in Color start, in float duration, in TweenParams tweenParams = default)
                {
                    GetCurrentValue(out var end, ref state, entity);
                    FromTo(ref ecb, entity, start.ToFloat4(), end, duration, tweenParams);
                }

                [BurstCompile]
                public static void From(ref SystemState state, ref EntityCommandBuffer.ParallelWriter ecb, in int sortKey, in Entity entity, in Color start, in float duration, in TweenParams tweenParams = default)
                {
                    GetCurrentValue(out var end, ref state, entity);
                    FromTo(ref ecb, sortKey, entity, start.ToFloat4(), end, duration, tweenParams);
                }

                [BurstCompile]
                private static void GetCurrentValue(out float4 currentValue, ref SystemState state, in Entity entity)
                {
                    currentValue = state.EntityManager.GetComponentData<URPMaterialPropertyBaseColor>(entity).Value;
                }
            }
        }
    }
}