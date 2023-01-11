﻿using Unity.Entities;

namespace DotsTween.Tweens
{
    [UpdateInGroup(typeof(TweenSimulationSystemGroup))]
    [UpdateAfter(typeof(TweenApplySystemGroup))]
    internal partial class TweenStateSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var destroyBufferFromEntity = GetBufferLookup<TweenDestroyCommand>(true);

            EndSimulationEntityCommandBufferSystem endSimEcbSystem = World.GetOrCreateSystemManaged<EndSimulationEntityCommandBufferSystem>();
            EntityCommandBuffer.ParallelWriter parallelWriter = endSimEcbSystem.CreateCommandBuffer().AsParallelWriter();

            Entities
                .WithReadOnly(destroyBufferFromEntity)
                .WithNone<TweenPause>()
                .ForEach((Entity entity, int entityInQueryIndex, ref DynamicBuffer<TweenState> tweenBuffer) =>
                {
                    DynamicBuffer<TweenDestroyCommand> newDestroyCommandBuffer = default;
                    if (!destroyBufferFromEntity.HasBuffer(entity))
                    {
                        newDestroyCommandBuffer = parallelWriter.AddBuffer<TweenDestroyCommand>(entityInQueryIndex, entity);
                    }

                    for (int i = tweenBuffer.Length - 1; i >= 0; i--)
                    {
                        TweenState tween = tweenBuffer[i];

                        bool isInfiniteLoop = tween.LoopCount == TweenState.LOOP_COUNT_INFINITE;
                        float normalizedTime = tween.GetNormalizedTime();
                        if (tween.IsReverting && normalizedTime <= 0.0f)
                        {
                            if (!isInfiniteLoop)
                            {
                                tween.LoopCount--;
                            }

                            tween.IsReverting = false;
                            tween.Time = 0.0f;
                        }
                        else if (!tween.IsReverting && normalizedTime >= 1.0f)
                        {
                            if (tween.IsPingPong)
                            {
                                tween.IsReverting = true;
                                tween.Time = tween.Duration / 2.0f;
                            }
                            else
                            {
                                if (!isInfiniteLoop)
                                {
                                    tween.LoopCount--;
                                }

                                if (isInfiniteLoop || tween.LoopCount > 0)
                                {
                                    tween.Time = 0.0f;
                                }
                            }
                        }

                        if (!isInfiniteLoop && tween.LoopCount == 0)
                        {
                            if (newDestroyCommandBuffer.IsCreated)
                            {
                                newDestroyCommandBuffer.Add(new TweenDestroyCommand(tween.Id));
                            }
                            else
                            {
                                parallelWriter.AppendToBuffer(entityInQueryIndex, entity, new TweenDestroyCommand(tween.Id));
                            }
                        }

                        tweenBuffer[i] = tween;
                    }
                }).ScheduleParallel();

            endSimEcbSystem.AddJobHandleForProducer(Dependency);
        }
    }
}