using DotsTween.Tweens;
using Unity.Entities;

namespace DotsTween.Timelines
{
    internal struct TimelineComponentOperationTuple
    {
        internal Entity Target;
        internal ComponentOperations Operations;
    }
}