using System.Runtime.InteropServices;
using Unity.Entities;

namespace DotsTween.Tweens
{
    public struct TweenStopInfo : IBufferElementData
    {
        internal readonly uint TweenId;
        [MarshalAs(UnmanagedType.U1)] internal readonly bool SpecificTweenTarget;

        public TweenStopInfo(uint tweenId)
        {
            TweenId = tweenId;
            SpecificTweenTarget = true;
        }
    }
}