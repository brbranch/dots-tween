namespace DotsTween.Tweens
{
    public interface ITweenInfo<T>
    {
        void SetTweenInfo(in T start, in T end);
        T GetTweenStart();
        T GetTweenEnd();
    }
}