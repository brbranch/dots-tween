using Unity.Entities;
using UnityEngine;

namespace DotsTween.Tweens
{
    internal struct TweenTintCommand : IComponentData, ITweenParams, ITweenInfo<Color>
    {
        public TweenParams TweenParams;
        public Color Start;
        public Color End;

        public TweenTintCommand(in TweenParams tweenParams, in Color start, in Color end)
        {
            TweenParams = tweenParams;
            Start = start;
            End = end;
        }

        public void SetTweenInfo(in Color start, in Color end)
        {
            Start = start;
            End = end;
        }

        public void SetTweenParams(in TweenParams tweenParams)
        {
            TweenParams = tweenParams;
        }

        public TweenParams GetTweenParams()
        {
            return TweenParams;
        }

        public Color GetTweenStart()
        {
            return Start;
        }

        public Color GetTweenEnd()
        {
            return End;
        }
    }
}