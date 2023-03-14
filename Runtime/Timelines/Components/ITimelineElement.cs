using Unity.Entities;

namespace DotsTween.Timelines
{
    internal interface ITimelineElement
    {
        public int GetId();
        public Entity GetTargetEntity();
        public float GetStartTime();
        public float GetEndTime();
        public IComponentData GetCommand();
        public int GetTweenId();
    }
}