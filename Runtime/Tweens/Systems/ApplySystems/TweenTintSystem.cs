﻿using DotsTween.Tweens;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;

namespace DotsTween
{
    [UpdateInGroup(typeof(TweenApplySystemGroup))]
    internal partial class TweenTintSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            Entities
                .WithNone<TweenPause>()
                .ForEach((ref URPMaterialPropertyBaseColor baseColor, in DynamicBuffer<TweenState> tweenBuffer, in TweenTint tweenInfo) =>
                {
                    foreach (var tween in tweenBuffer)
                    {
                        if (tween.Id != tweenInfo.Id) continue;
                        baseColor.Value = math.lerp(tweenInfo.Start, tweenInfo.End, tween.EasePercentage);
                    }
                }).WithoutBurst().Run();
        }
    }
}