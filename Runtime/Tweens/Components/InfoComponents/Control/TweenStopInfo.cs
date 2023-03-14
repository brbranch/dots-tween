using System.Runtime.InteropServices;
using Unity.Entities;

namespace DotsTween.Tweens
{
    public struct TweenStopInfo : IBufferElementData
    {
        internal int TweenId;
        [MarshalAs(UnmanagedType.U1)] internal bool SpecificTweenTarget;

        public TweenStopInfo(int tweenId)
        {
            TweenId = tweenId;
            SpecificTweenTarget = true;
        }
    }
}