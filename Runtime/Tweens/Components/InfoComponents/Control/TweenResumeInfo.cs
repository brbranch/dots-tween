using System.Runtime.InteropServices;
using Unity.Entities;

namespace DotsTween.Tweens
{
    public struct TweenResumeInfo : IBufferElementData
    {
        internal readonly uint TweenId;
        [MarshalAs(UnmanagedType.U1)] internal readonly bool SpecificTweenTarget;

        public TweenResumeInfo(uint tweenId)
        {
            TweenId = tweenId;
            SpecificTweenTarget = true;
        }
    }
}