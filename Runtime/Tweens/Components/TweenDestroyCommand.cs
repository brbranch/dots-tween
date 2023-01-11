using Unity.Entities;

namespace DotsTween.Tweens
{
    public struct TweenDestroyCommand : IBufferElementData
    {
        public TweenDestroyCommand(int id)
        {
            Id = id;
        }

        public int Id;
    }
}