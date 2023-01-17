using DotsTween.Tweens;
using Unity.Entities;
using UnityEngine;

namespace DotsTween
{
    [UpdateInGroup(typeof(TweenApplySystemGroup))]
    internal partial class TweenTintSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            Entities
                .WithNone<TweenPause>()
                .ForEach((SpriteRenderer spriteRenderer, in DynamicBuffer<TweenState> tweenBuffer, in TweenTint tweenInfo) =>
                {
                    foreach (var tween in tweenBuffer)
                    {
                        if (tween.Id != tweenInfo.Id) continue;
                        spriteRenderer.color = Color.Lerp(tweenInfo.Start, tweenInfo.End, tween.EasePercentage);
                    }
                }).WithoutBurst().Run();
        }
    }
}