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
                        tween.Time += tween.IsReverting ? -deltaTime : deltaTime;

                        float normalizedTime = tween.GetNormalizedTime();
                        tween.EasePercentage = Ease.CalculatePercentage(normalizedTime, tween.EaseType, tween.EaseExponent);

                        tweenBuffer[i] = tween;
                    }
                }).ScheduleParallel();
        }
    }
}