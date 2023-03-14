using System.Runtime.InteropServices;
using Unity.Entities;

namespace DotsTween.Tweens
{
    public struct TweenPauseInfo : IBufferElementData
    {
        internal int TweenId;
        [MarshalAs(UnmanagedType.U1)] internal bool SpecificTweenTarget;

        public TweenPauseInfo(int tweenId)
        {
            TweenId = tweenId;
            SpecificTweenTarget = true;
        }
    }
}