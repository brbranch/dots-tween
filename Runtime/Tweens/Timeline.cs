using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace DotsTween.Tweens
{
    public class Timeline : IComponentData
    {
        public float Duration;
        
        internal List<ITweenCommand> Commands;
        
        public Timeline()
        {
            Commands = new List<ITweenCommand>();
            Duration = 0f;
        }
        
        public Timeline Insert(float atPosition, ITweenCommand command)
        {
            TweenParams tweenParams = command.GetTweenParams();
            atPosition += tweenParams.StartDelay;

            if (tweenParams.LoopCount < 0)
            {
                tweenParams.LoopCount = short.MaxValue - 1;
                Debug.LogError("Infinite Loops aren't allowed in Timelines. Changing to short.MaxValue - 1.");
            }

            float totalDuration = tweenParams.Duration * (tweenParams.LoopCount + 1);
            tweenParams.StartDelay = 0;
            tweenParams.SetTimelinePositions(atPosition, totalDuration);

            if (tweenParams.TimelineEndPosition > Duration)
            {
                Duration = tweenParams.TimelineEndPosition;
            }

            command.SetTweenParams(tweenParams);
            Commands.Add(command);
            return this;
        }

        public Timeline Append(ITweenCommand command)
        {
            return Insert(Duration, command);
        }

        public Timeline AddComponentTagsOnComplete(params ComponentType[] tags)
        {
            return this;
        }

        public Timeline RemoveComponentTagsOnComplete(params ComponentType[] tags)
        {
            return this;
        }

        public Timeline EnableComponentTagsOnComplete(params ComponentType[] tags)
        {
            return this;
        }

        public Timeline DisableComponentTagsOnComplete(EntityCommandBuffer.ParallelWriter ecb, params ComponentType[] tags)
        {
            var x = new ComponentType(typeof(TweenState));
            ecb.AddComponent(0, Entity.Null, x);
            return this;
        }
    }
}