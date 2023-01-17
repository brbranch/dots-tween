using Unity.Entities;
using UnityEngine;

namespace DotsTween.Tweens
{
    public struct TweenTint : IComponentData, ITweenId, ITweenInfo<Color>
    {
        public int Id;
        public Color Start;
        public Color End;

        public TweenTint(in int id, in Color start, in Color end)
        {
            Id = id;
            Start = start;
            End = end;
        }

        public void SetTweenId(in int id)
        {
            Id = id;
        }

        public int GetTweenId()
        {
            return Id;
        }

        public void SetTweenInfo(in Color start, in Color end)
        {
            Start = start;
            End = end;
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