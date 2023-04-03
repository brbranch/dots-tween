using DotsTween.Math;
using Unity.Burst;
using Unity.Entities;

namespace DotsTween.Tweens
{
    [BurstCompile]
    [UpdateInGroup(typeof(TweenSimulationSystemGroup))]
    internal partial class TweenEaseSystem : SystemBase
    {
        [BurstCompile]
        protected override void OnCreate()
        {
            RequireForUpdate<TweenState>();
        }

        [BurstCompile]
        protected override void OnUpdate()
        {
            Entities.ForEach((ref DynamicBuffer<TweenState> tweenBuffer) =>
            {
                if (tweenBuffer.IsEmpty) return;
                
                var deltaTime = SystemAPI.Time.DeltaTime;
                for (int i = 0; i < tweenBuffer.Length; i++)
                {
                    TweenState tween = tweenBuffer[i];
                    if (tween.IsPaused) continue;
                    tween.CurrentTime += tween.IsReverting ? -deltaTime : deltaTime;
                    tween.EasePercentage = EasingFunctions.Ease(tween.EaseType, tween.GetNormalizedTime());
                    tweenBuffer[i] = tween;
                }
            }).ScheduleParallel();
        }
    }
}