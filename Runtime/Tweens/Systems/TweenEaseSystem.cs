using DotsTween.Math;
using DotsTween.Tweens;
using Unity.Entities;

namespace DotsTween
{
    [UpdateInGroup(typeof(TweenSimulationSystemGroup))]
    internal partial class TweenEaseSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            Entities
                .WithNone<TweenPause>()
                .ForEach((ref DynamicBuffer<TweenState> tweenBuffer) =>
                {
                    var deltaTime = SystemAPI.Time.DeltaTime;
                    
                    for (int i = 0; i < tweenBuffer.Length; i++)
                    {
                        TweenState tween = tweenBuffer[i];
                        tween.CurrentTime += tween.IsReverting ? -deltaTime : deltaTime;
                        tween.EasePercentage = EasingFunctions.Ease(tween.EaseType, tween.GetNormalizedTime());
                        tweenBuffer[i] = tween;
                    }
                }).ScheduleParallel();
        }
    }
}