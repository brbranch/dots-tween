using DotsTween.Math;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

namespace DotsTween.Tweens
{
    [BurstCompile]
    public static class Tween
    {
        [BurstCompile]
        public static class Controls
        {
            [BurstCompile]
            public static void Pause(in EntityManager entityManager, in Entity entity)
            {
                entityManager.AddComponent<TweenPause>(entity);
            }

            [BurstCompile]
            public static void Pause(in EntityCommandBuffer commandBuffer, in Entity entity)
            {
                commandBuffer.AddComponent<TweenPause>(entity);
            }

            [BurstCompile]
            public static void Pause(in EntityCommandBuffer.ParallelWriter parallelWriter, in int sortKey, in Entity entity)
            {
                parallelWriter.AddComponent<TweenPause>(sortKey, entity);
            }

            [BurstCompile]
            public static void Resume(in EntityManager entityManager, in Entity entity)
            {
                entityManager.AddComponent<TweenResumeCommand>(entity);
            }

            [BurstCompile]
            public static void Resume(in EntityCommandBuffer commandBuffer, in Entity entity)
            {
                commandBuffer.AddComponent<TweenResumeCommand>(entity);
            }

            [BurstCompile]
            public static void Resume(in EntityCommandBuffer.ParallelWriter parallelWriter, in int sortKey, in Entity entity)
            {
                parallelWriter.AddComponent<TweenResumeCommand>(sortKey, entity);
            }

            [BurstCompile]
            public static void Stop(in EntityManager entityManager, in Entity entity)
            {
                entityManager.AddComponent<TweenStopCommand>(entity);
            }

            [BurstCompile]
            public static void Stop(in EntityCommandBuffer commandBuffer, in Entity entity)
            {
                commandBuffer.AddComponent<TweenStopCommand>(entity);
            }

            [BurstCompile]
            public static void Stop(in EntityCommandBuffer.ParallelWriter parallelWriter, in int sortKey, in Entity entity)
            {
                parallelWriter.AddComponent<TweenStopCommand>(sortKey, entity);
            }
        }

        [BurstCompile]
        public static class Move
        {
            [BurstCompile]
            public static void FromTo(ref SystemState state, in Entity entity, in float3 start, in float3 end, in TweenParams tweenParams)
            {
                state.EntityManager.AddComponentData(entity, new TweenTranslationCommand(tweenParams, start, end));
            }

            [BurstCompile]
            public static void FromTo(ref EntityCommandBuffer ecb, in Entity entity, in float3 start, in float3 end, in TweenParams tweenParams)
            {
                ecb.AddComponent(entity, new TweenTranslationCommand(tweenParams, start, end));
            }

            [BurstCompile]
            public static void FromTo(ref EntityCommandBuffer.ParallelWriter ecb, in Entity entity, in int sortKey, in float3 start, in float3 end, in TweenParams tweenParams)
            {
                ecb.AddComponent(sortKey, entity, new TweenTranslationCommand(tweenParams, start, end));
            }

            [BurstCompile]
            public static void To(ref SystemState state, in Entity entity, in float3 end, in TweenParams tweenParams)
            {
                GetCurrentValue(out var start, ref state, entity);
                FromTo(ref state, entity, start, end, tweenParams);
            }

            [BurstCompile]
            public static void To(ref SystemState state, ref EntityCommandBuffer ecb, in Entity entity, in float3 end, in TweenParams tweenParams)
            {
                GetCurrentValue(out var start, ref state, entity);
                FromTo(ref ecb, entity, start, end, tweenParams);
            }

            [BurstCompile]
            public static void To(ref SystemState state, ref EntityCommandBuffer.ParallelWriter ecb, in Entity entity, in int sortKey, in float3 end, in TweenParams tweenParams)
            {
                GetCurrentValue(out var start, ref state, entity);
                FromTo(ref ecb, entity, sortKey, start, end, tweenParams);
            }

            [BurstCompile]
            public static void From(ref SystemState state, in Entity entity, in float3 start, in TweenParams tweenParams)
            {
                GetCurrentValue(out var end, ref state, entity);
                FromTo(ref state, entity, start, end, tweenParams);
            }

            [BurstCompile]
            public static void From(ref SystemState state, ref EntityCommandBuffer ecb, in Entity entity, in float3 start, in TweenParams tweenParams)
            {
                GetCurrentValue(out var end, ref state, entity);
                FromTo(ref ecb, entity, start, end, tweenParams);
            }

            [BurstCompile]
            public static void From(ref SystemState state, ref EntityCommandBuffer.ParallelWriter ecb, in Entity entity, in int sortKey, in float3 start, in TweenParams tweenParams)
            {
                GetCurrentValue(out var end, ref state, entity);
                FromTo(ref ecb, entity, sortKey, start, end, tweenParams);
            }

            [BurstCompile]
            private static void GetCurrentValue(out float3 currentValue, ref SystemState state, in Entity entity)
            {
                currentValue = state.EntityManager.GetComponentData<LocalTransform>(entity).Position;
            }
        }

        [BurstCompile]
        public static class Rotate
        {
            [BurstCompile]
            public static void FromTo(ref SystemState state, in Entity entity, in quaternion start, in quaternion end, in TweenParams tweenParams)
            {
                state.EntityManager.AddComponentData(entity, new TweenRotationCommand(tweenParams, start, end));
            }

            [BurstCompile]
            public static void FromTo(ref EntityCommandBuffer ecb, in Entity entity, in quaternion start, in quaternion end, in TweenParams tweenParams)
            {
                ecb.AddComponent(entity, new TweenRotationCommand(tweenParams, start, end));
            }

            [BurstCompile]
            public static void FromTo(ref EntityCommandBuffer.ParallelWriter ecb, in Entity entity, in int sortKey, in quaternion start, in quaternion end, in TweenParams tweenParams)
            {
                ecb.AddComponent(sortKey, entity, new TweenRotationCommand(tweenParams, start, end));
            }

            [BurstCompile]
            public static void To(ref SystemState state, in Entity entity, in quaternion end, in TweenParams tweenParams)
            {
                GetCurrentValue(out var start, ref state, entity);
                FromTo(ref state, entity, start, end, tweenParams);
            }

            [BurstCompile]
            public static void To(ref SystemState state, ref EntityCommandBuffer ecb, in Entity entity, in quaternion end, in TweenParams tweenParams)
            {
                GetCurrentValue(out var start, ref state, entity);
                FromTo(ref ecb, entity, start, end, tweenParams);
            }

            [BurstCompile]
            public static void To(ref SystemState state, ref EntityCommandBuffer.ParallelWriter ecb, in Entity entity, in int sortKey, in quaternion end, in TweenParams tweenParams)
            {
                GetCurrentValue(out var start, ref state, entity);
                FromTo(ref ecb, entity, sortKey, start, end, tweenParams);
            }

            [BurstCompile]
            public static void From(ref SystemState state, in Entity entity, in quaternion start, in TweenParams tweenParams)
            {
                GetCurrentValue(out var end, ref state, entity);
                FromTo(ref state, entity, start, end, tweenParams);
            }

            [BurstCompile]
            public static void From(ref SystemState state, ref EntityCommandBuffer ecb, in Entity entity, in quaternion start, in TweenParams tweenParams)
            {
                GetCurrentValue(out var end, ref state, entity);
                FromTo(ref ecb, entity, start, end, tweenParams);
            }

            [BurstCompile]
            public static void From(ref SystemState state, ref EntityCommandBuffer.ParallelWriter ecb, in Entity entity, in int sortKey, in quaternion start, in TweenParams tweenParams)
            {
                GetCurrentValue(out var end, ref state, entity);
                FromTo(ref ecb, entity, sortKey, start, end, tweenParams);
            }

            [BurstCompile]
            private static void GetCurrentValue(out quaternion currentValue, ref SystemState state, in Entity entity)
            {
                currentValue = state.EntityManager.GetComponentData<LocalTransform>(entity).Rotation;
            }
        }

        [BurstCompile]
        public static class Scale
        {
            [BurstCompile]
            public static void FromTo(ref SystemState state, in Entity entity, in float start, in float end, in TweenParams tweenParams)
            {
                state.EntityManager.AddComponentData(entity, new TweenScaleCommand(tweenParams, start, end));
            }

            [BurstCompile]
            public static void FromTo(ref EntityCommandBuffer ecb, in Entity entity, in float start, in float end, in TweenParams tweenParams)
            {
                ecb.AddComponent(entity, new TweenScaleCommand(tweenParams, start, end));
            }

            [BurstCompile]
            public static void FromTo(ref EntityCommandBuffer.ParallelWriter ecb, in Entity entity, in int sortKey, in float start, in float end, in TweenParams tweenParams)
            {
                ecb.AddComponent(sortKey, entity, new TweenScaleCommand(tweenParams, start, end));
            }

            [BurstCompile]
            public static void To(ref SystemState state, in Entity entity, in float end, in TweenParams tweenParams)
            {
                GetCurrentValue(out var start, ref state, entity);
                FromTo(ref state, entity, start, end, tweenParams);
            }

            [BurstCompile]
            public static void To(ref SystemState state, ref EntityCommandBuffer ecb, in Entity entity, in float end, in TweenParams tweenParams)
            {
                GetCurrentValue(out var start, ref state, entity);
                FromTo(ref ecb, entity, start, end, tweenParams);
            }

            [BurstCompile]
            public static void To(ref SystemState state, ref EntityCommandBuffer.ParallelWriter ecb, in Entity entity, in int sortKey, in float end, in TweenParams tweenParams)
            {
                GetCurrentValue(out var start, ref state, entity);
                FromTo(ref ecb, entity, sortKey, start, end, tweenParams);
            }

            [BurstCompile]
            public static void From(ref SystemState state, in Entity entity, in float start, in TweenParams tweenParams)
            {
                GetCurrentValue(out var end, ref state, entity);
                FromTo(ref state, entity, start, end, tweenParams);
            }

            [BurstCompile]
            public static void From(ref SystemState state, ref EntityCommandBuffer ecb, in Entity entity, in float start, in TweenParams tweenParams)
            {
                GetCurrentValue(out var end, ref state, entity);
                FromTo(ref ecb, entity, start, end, tweenParams);
            }

            [BurstCompile]
            public static void From(ref SystemState state, ref EntityCommandBuffer.ParallelWriter ecb, in Entity entity, in int sortKey, in float start, in TweenParams tweenParams)
            {
                GetCurrentValue(out var end, ref state, entity);
                FromTo(ref ecb, entity, sortKey, start, end, tweenParams);
            }

            [BurstCompile]
            private static void GetCurrentValue(out float currentValue, ref SystemState state, in Entity entity)
            {
                currentValue = state.EntityManager.GetComponentData<LocalTransform>(entity).Scale;
            }
        }

        [BurstCompile]
        public static class NonUniformScale
        {
            [BurstCompile]
            public static void FromTo(ref SystemState state, in Entity entity, in float3 start, in float3 end, in TweenParams tweenParams)
            {
                state.EntityManager.AddComponentData(entity, new TweenNonUniformScaleCommand(tweenParams, start, end));
            }

            [BurstCompile]
            public static void FromTo(ref EntityCommandBuffer ecb, in Entity entity, in float3 start, in float3 end, in TweenParams tweenParams)
            {
                ecb.AddComponent(entity, new TweenNonUniformScaleCommand(tweenParams, start, end));
            }

            [BurstCompile]
            public static void FromTo(ref EntityCommandBuffer.ParallelWriter ecb, in Entity entity, in int sortKey, in float3 start, in float3 end, in TweenParams tweenParams)
            {
                ecb.AddComponent(sortKey, entity, new TweenNonUniformScaleCommand(tweenParams, start, end));
            }

            [BurstCompile]
            public static void To(ref SystemState state, in Entity entity, in float3 end, in TweenParams tweenParams)
            {
                GetCurrentValue(out var start, ref state, entity);
                FromTo(ref state, entity, start, end, tweenParams);
            }

            [BurstCompile]
            public static void To(ref SystemState state, ref EntityCommandBuffer ecb, in Entity entity, in float3 end, in TweenParams tweenParams)
            {
                GetCurrentValue(out var start, ref state, entity);
                FromTo(ref ecb, entity, start, end, tweenParams);
            }

            [BurstCompile]
            public static void To(ref SystemState state, ref EntityCommandBuffer.ParallelWriter ecb, in Entity entity, in int sortKey, in float3 end, in TweenParams tweenParams)
            {
                GetCurrentValue(out var start, ref state, entity);
                FromTo(ref ecb, entity, sortKey, start, end, tweenParams);
            }

            [BurstCompile]
            public static void From(ref SystemState state, in Entity entity, in float3 start, in TweenParams tweenParams)
            {
                GetCurrentValue(out var end, ref state, entity);
                FromTo(ref state, entity, start, end, tweenParams);
            }

            [BurstCompile]
            public static void From(ref SystemState state, ref EntityCommandBuffer ecb, in Entity entity, in float3 start, in TweenParams tweenParams)
            {
                GetCurrentValue(out var end, ref state, entity);
                FromTo(ref ecb, entity, start, end, tweenParams);
            }

            [BurstCompile]
            public static void From(ref SystemState state, ref EntityCommandBuffer.ParallelWriter ecb, in Entity entity, in int sortKey, in float3 start, in TweenParams tweenParams)
            {
                GetCurrentValue(out var end, ref state, entity);
                FromTo(ref ecb, entity, sortKey, start, end, tweenParams);
            }

            [BurstCompile]
            private static void GetCurrentValue(out float3 currentValue, ref SystemState state, in Entity entity)
            {
                if (!state.EntityManager.HasComponent<PostTransformScale>(entity))
                {
                    currentValue = new float3(1f, 1f, 1f);
                    return;
                }

                var matrix = state.EntityManager.GetComponentData<PostTransformScale>(entity).Value;
                currentValue = new float3(matrix.c0.x, matrix.c1.y, matrix.c2.z);
            }
        }

        [BurstCompile]
        public static class URP
        {
            [BurstCompile]
            public static class Tint
            {
                [BurstCompile]
                public static void FromTo(ref SystemState state, in Entity entity, in float4 start, in float4 end, in TweenParams tweenParams)
                {
                    state.EntityManager.AddComponentData(entity, new TweenURPTintCommand(tweenParams, start, end));
                }

                [BurstCompile]
                public static void FromTo(ref EntityCommandBuffer ecb, in Entity entity, in float4 start, in float4 end, in TweenParams tweenParams)
                {
                    ecb.AddComponent(entity, new TweenURPTintCommand(tweenParams, start, end));
                }

                [BurstCompile]
                public static void FromTo(ref EntityCommandBuffer.ParallelWriter ecb, in Entity entity, in int sortKey, in float4 start, in float4 end, in TweenParams tweenParams)
                {
                    ecb.AddComponent(sortKey, entity, new TweenURPTintCommand(tweenParams, start, end));
                }

                [BurstCompile]
                public static void To(ref SystemState state, in Entity entity, in float4 end, in TweenParams tweenParams)
                {
                    GetCurrentValue(out var start, ref state, entity);
                    FromTo(ref state, entity, start, end, tweenParams);
                }

                [BurstCompile]
                public static void To(ref SystemState state, ref EntityCommandBuffer ecb, in Entity entity, in float4 end, in TweenParams tweenParams)
                {
                    GetCurrentValue(out var start, ref state, entity);
                    FromTo(ref ecb, entity, start, end, tweenParams);
                }

                [BurstCompile]
                public static void To(ref SystemState state, ref EntityCommandBuffer.ParallelWriter ecb, in Entity entity, in int sortKey, in float4 end, in TweenParams tweenParams)
                {
                    GetCurrentValue(out var start, ref state, entity);
                    FromTo(ref ecb, entity, sortKey, start, end, tweenParams);
                }

                [BurstCompile]
                public static void From(ref SystemState state, in Entity entity, in float4 start, in TweenParams tweenParams)
                {
                    GetCurrentValue(out var end, ref state, entity);
                    FromTo(ref state, entity, start, end, tweenParams);
                }

                [BurstCompile]
                public static void From(ref SystemState state, ref EntityCommandBuffer ecb, in Entity entity, in float4 start, in TweenParams tweenParams)
                {
                    GetCurrentValue(out var end, ref state, entity);
                    FromTo(ref ecb, entity, start, end, tweenParams);
                }

                [BurstCompile]
                public static void From(ref SystemState state, ref EntityCommandBuffer.ParallelWriter ecb, in Entity entity, in int sortKey, in float4 start, in TweenParams tweenParams)
                {
                    GetCurrentValue(out var end, ref state, entity);
                    FromTo(ref ecb, entity, sortKey, start, end, tweenParams);
                }
                
                [BurstCompile]
                public static void FromTo(ref SystemState state, in Entity entity, in Color start, in Color end, in TweenParams tweenParams)
                {
                    state.EntityManager.AddComponentData(entity, new TweenURPTintCommand(tweenParams, start.ToFloat4(), end.ToFloat4()));
                }

                [BurstCompile]
                public static void FromTo(ref EntityCommandBuffer ecb, in Entity entity, in Color start, in Color end, in TweenParams tweenParams)
                {
                    ecb.AddComponent(entity, new TweenURPTintCommand(tweenParams, start.ToFloat4(), end.ToFloat4()));
                }

                [BurstCompile]
                public static void FromTo(ref EntityCommandBuffer.ParallelWriter ecb, in Entity entity, in int sortKey, in Color start, in Color end, in TweenParams tweenParams)
                {
                    ecb.AddComponent(sortKey, entity, new TweenURPTintCommand(tweenParams, start.ToFloat4(), end.ToFloat4()));
                }

                [BurstCompile]
                public static void To(ref SystemState state, in Entity entity, in Color end, in TweenParams tweenParams)
                {
                    GetCurrentValue(out var start, ref state, entity);
                    FromTo(ref state, entity, start, end.ToFloat4(), tweenParams);
                }

                [BurstCompile]
                public static void To(ref SystemState state, ref EntityCommandBuffer ecb, in Entity entity, in Color end, in TweenParams tweenParams)
                {
                    GetCurrentValue(out var start, ref state, entity);
                    FromTo(ref ecb, entity, start, end.ToFloat4(), tweenParams);
                }

                [BurstCompile]
                public static void To(ref SystemState state, ref EntityCommandBuffer.ParallelWriter ecb, in Entity entity, in int sortKey, in Color end, in TweenParams tweenParams)
                {
                    GetCurrentValue(out var start, ref state, entity);
                    FromTo(ref ecb, entity, sortKey, start, end.ToFloat4(), tweenParams);
                }

                [BurstCompile]
                public static void From(ref SystemState state, in Entity entity, in Color start, in TweenParams tweenParams)
                {
                    GetCurrentValue(out var end, ref state, entity);
                    FromTo(ref state, entity, start.ToFloat4(), end, tweenParams);
                }

                [BurstCompile]
                public static void From(ref SystemState state, ref EntityCommandBuffer ecb, in Entity entity, in Color start, in TweenParams tweenParams)
                {
                    GetCurrentValue(out var end, ref state, entity);
                    FromTo(ref ecb, entity, start.ToFloat4(), end, tweenParams);
                }

                [BurstCompile]
                public static void From(ref SystemState state, ref EntityCommandBuffer.ParallelWriter ecb, in Entity entity, in int sortKey, in Color start, in TweenParams tweenParams)
                {
                    GetCurrentValue(out var end, ref state, entity);
                    FromTo(ref ecb, entity, sortKey, start.ToFloat4(), end, tweenParams);
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