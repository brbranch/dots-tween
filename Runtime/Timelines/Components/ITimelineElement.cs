using Unity.Entities;

namespace DotsTween.Timelines
{
    internal interface ITimelineElement
    {
        public uint GetId();
        public Entity GetTargetEntity();
        public float GetStartTime();
        public float GetEndTime();
        public IComponentData GetCommand();
        public uint GetTweenId();
    }
}