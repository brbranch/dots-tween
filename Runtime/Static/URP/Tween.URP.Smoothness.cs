﻿#if DOTS_TWEEN_URP
using DotsTween.Tweens;
using Unity.Burst;
using Unity.Entities;
using Unity.Rendering;
using Unity.Transforms;

namespace DotsTween
{
    public static partial class Tween
    {
        public static partial class URP
        {
            [BurstCompile]
            public static class Smoothness
            {
                [BurstCompile]
                public static void FromTo(ref SystemState state, in Entity entity, in float start, in float end, in float duration, in TweenParams tweenParams = default)
                {
                    state.EntityManager.AddComponentData(entity, new TweenURPSmoothnessCommand(start, end, duration, tweenParams));
                }

                [BurstCompile]
                public static void FromTo(ref EntityCommandBuffer ecb, in Entity entity, in float start, in float end, in float duration, in TweenParams tweenParams = default)
                {
                    ecb.AddComponent(entity, new TweenURPSmoothnessCommand(start, end, duration, tweenParams));
                }

                [BurstCompile]
                public static void FromTo(ref EntityCommandBuffer.ParallelWriter ecb, in int sortKey, in Entity entity, in float start, in float end, in float duration, in TweenParams tweenParams = default)
                {
                    ecb.AddComponent(sortKey, entity, new TweenURPSmoothnessCommand(start, end, duration, tweenParams));
                }

                [BurstCompile]
                public static void To(ref SystemState state, in Entity entity, in float end, in float duration, in TweenParams tweenParams = default)
                {
                    GetCurrentValue(out var start, ref state, entity);
                    FromTo(ref state, entity, start, end, duration, tweenParams);
                }

                [BurstCompile]
                public static void To(ref SystemState state, ref EntityCommandBuffer ecb, in Entity entity, in float end, in float duration, in TweenParams tweenParams = default)
                {
                    GetCurrentValue(out var start, ref state, entity);
                    FromTo(ref ecb, entity, start, end, duration, tweenParams);
                }

                [BurstCompile]
                public static void To(ref SystemState state, ref EntityCommandBuffer.ParallelWriter ecb, in int sortKey, in Entity entity, in float end, in float duration, in TweenParams tweenParams = default)
                {
                    GetCurrentValue(out var start, ref state, entity);
                    FromTo(ref ecb, sortKey, entity, start, end, duration, tweenParams);
                }

                [BurstCompile]
                public static void From(ref SystemState state, in Entity entity, in float start, in float duration, in TweenParams tweenParams = default)
                {
                    GetCurrentValue(out var end, ref state, entity);
                    FromTo(ref state, entity, start, end, duration, tweenParams);
                }

                [BurstCompile]
                public static void From(ref SystemState state, ref EntityCommandBuffer ecb, in Entity entity, in float start, in float duration, in TweenParams tweenParams = default)
                {
                    GetCurrentValue(out var end, ref state, entity);
                    FromTo(ref ecb, entity, start, end, duration, tweenParams);
                }

                [BurstCompile]
                public static void From(ref SystemState state, ref EntityCommandBuffer.ParallelWriter ecb, in int sortKey, in Entity entity, in float start, in float duration, in TweenParams tweenParams = default)
                {
                    GetCurrentValue(out var end, ref state, entity);
                    FromTo(ref ecb, sortKey, entity, start, end, duration, tweenParams);
                }

                [BurstCompile]
                private static void GetCurrentValue(out float currentValue, ref SystemState state, in Entity entity)
                {
                    currentValue = state.EntityManager.GetComponentData<URPMaterialPropertySmoothness>(entity).Value;
                }
            }
        }
    }
}
#endif