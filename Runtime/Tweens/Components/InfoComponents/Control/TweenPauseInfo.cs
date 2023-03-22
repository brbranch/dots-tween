using System.Runtime.InteropServices;
using Unity.Entities;

namespace DotsTween.Tweens
{
    public struct TweenPauseInfo : IBufferElementData
    {
        internal readonly uint TweenId;
        [MarshalAs(UnmanagedType.U1)] internal readonly bool SpecificTweenTarget;

        public TweenPauseInfo(uint tweenId)
        {
            TweenId = tweenId;
            SpecificTweenTarget = true;
        }
    }
}