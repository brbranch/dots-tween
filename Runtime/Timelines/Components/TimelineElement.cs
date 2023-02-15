using Unity.Entities;

namespace DotsTween.Timelines
{
    public struct TimelineElement
    {
        public Entity Target;
        public float StartTime;
        public float EndTime;
        public IComponentData Command;
    }
}