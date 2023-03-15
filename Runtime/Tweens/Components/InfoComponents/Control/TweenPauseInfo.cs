using System.Runtime.InteropServices;
using Unity.Entities;

namespace DotsTween.Tweens
{
    public struct TweenPauseInfo : IBufferElementData
    {
        internal readonly int TweenId;
        [MarshalAs(UnmanagedType.U1)] internal readonly bool SpecificTweenTarget;

        public TweenPauseInfo(int tweenId)
        {
            TweenId = tweenId;
            SpecificTweenTarget = true;
        }
    }
}