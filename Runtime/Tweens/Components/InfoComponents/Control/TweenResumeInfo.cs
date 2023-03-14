using System.Runtime.InteropServices;
using Unity.Entities;

namespace DotsTween.Tweens
{
    public struct TweenResumeInfo : IBufferElementData
    {
        internal int TweenId;
        [MarshalAs(UnmanagedType.U1)] internal bool SpecificTweenTarget;

        public TweenResumeInfo(int tweenId)
        {
            TweenId = tweenId;
            SpecificTweenTarget = true;
        }
    }
}