using Unity.Mathematics;

namespace DotsTween.Math
{
    public static class EasingFunctions
    {
        private const float N1 = 7.5625f;
        private const float D1 = 2.75f;
        private const float C1 = 1.70158f;
        private const float C2 = C1 * 1.525f;
        private const float C3 = C1 + 1f;
        private const float C4 = (2f * math.PI) / 3f;
        private const float C5 = (2f * math.PI) / 4.5f;
        
        public static float Linear(float x) { return x; }
        public static float InQuad(float x) { return x * x; }
        public static float OutQuad(float x) { return 1f - (1f - x) * (1f - x); }
        public static float InOutQuad(float x) { return x < 0.5f ? 2f * x * x : 1f - math.pow(-2f * x + 2f, 2f) / 2f; }
        public static float InCubic(float x) { return x * x * x; }
        public static float OutCubic(float x) { return 1 - math.pow(1 - x, 3); }
        public static float InOutCubic(float x) { return x < 0.5 ? 4 * x * x * x : 1 - math.pow(-2 * x + 2, 3) / 2; }
        public static float InQuart(float x) { return x * x * x * x; }
        public static float OutQuart(float x) { return 1f - math.pow(1f - x, 4f); }
        public static float InOutQuart(float x) { return x < 0.5f ? 8f * x * x * x * x : 1f - math.pow(-2f * x + 2f, 4f) / 2f; }
        public static float InQuint(float x) { return x * x * x * x * x; }
        public static float OutQuint(float x) { return 1f - math.pow(1f - x, 5f); }
        public static float InOutQuint(float x) { return x < 0.5f ? 16f * x * x * x * x * x : 1 - math.pow(-2f * x + 2f, 5f) / 2f; }
        public static float InSine(float x) { return 1f - math.cos((x * math.PI) / 2f); }
        public static float OutSine(float x) { return math.sin((x * math.PI) / 2f); }
        public static float InOutSine(float x) { return -(math.cos(math.PI * x) - 1f) / 2f; }
        public static float InExpo(float x) { return x <= 0f ? 0f : math.pow(2f, 10f * x - 10f); }
        public static float OutExpo(float x) { return x >= 1f ? 1f : 1f - math.pow(2f, -10f * x); }
        public static float InOutExpo(float x) { return x <= 0f ? 0f : x >= 1f ? 1f : x < 0.5f ? math.pow(2f, 20f * x - 10f) / 2f : (2f - math.pow(2f, -20f * x + 10f)) / 2f; }
        public static float InCirc(float x) { return 1f - math.sqrt(1f - math.pow(x, 2f)); }
        public static float OutCirc(float x) { return math.sqrt(1f - math.pow(x - 1f, 2f)); }
        public static float InOutCirc(float x) { return x < 0.5f ? (1f - math.sqrt(1f - math.pow(2f * x, 2f))) / 2f : (math.sqrt(1f - math.pow(-2f * x + 2f, 2f)) + 1f) / 2f; }
        public static float InBack(float x) { return C3 * x * x * x - C1 * x * x; }
        public static float OutBack(float x) { return 1f + C3 * math.pow(x - 1f, 3f) + C1 * math.pow(x - 1f, 2f); }
        public static float InOutBack(float x) { return x < 0.5f ? (math.pow(2f * x, 2f) * ((C2 + 1f) * 2f * x - C2)) / 2f : (math.pow(2f * x - 2f, 2f) * ((C2 + 1f) * (x * 2f - 2f) + C2) + 2f) / 2f; }
        public static float InElastic(float x) { return x <= 0f ? 0f : x >= 1f ? 1f : -math.pow(2f, 10f * x - 10f) * math.sin((x * 10f - 10.75f) * C4); }
        public static float OutElastic(float x) { return x <= 0f ? 0f : x >= 1f ? 1f : math.pow(2f, -10f * x) * math.sin((x * 10f - 0.75f) * C4) + 1f; }
        public static float InOutElastic(float x) { return x <= 0f ? 0f : x >= 1f ? 1f : x < 0.5f ? -(math.pow(2f, 20f * x - 10f) * math.sin((20f * x - 11.125f) * C5)) / 2 : (math.pow(2f, -20f * x + 10f) * math.sin((20f * x - 11.125f) * C5)) / 2f + 1f; }
        public static float InBounce(float x) { return 1 - InterpolateOutBounceStatic(1 - x); }
        public static float OutBounce(float x) { return InterpolateOutBounceStatic(x); }
        public static float InOutBounce(float x) { return x < 0.5f ? (1f - InterpolateOutBounceStatic(1f - 2f * x)) / 2f : (1f + InterpolateOutBounceStatic(2f * x - 1f)) / 2f; }
        
        private static float InterpolateOutBounceStatic(float x)
        {
            switch (x)
            {
                case < 1f / D1: return N1 * x * x;
                case < 2f / D1:  
                {
                    float x1 = x - (1.5f / D1);
                    return N1 * x1 * x1 + 0.75f;
                }
                case < 2.5f / D1:
                {
                    float x1 = x - (2.25f / D1);
                    return N1 * x1 * x1 + 0.9375f;
                }
                default:
                {
                    float x1 = x - (2.625f / D1);
                    return N1 * x1 * x1 + 0.984375f;
                }
            }
        }
        
        public static float Ease(EaseType easeType, float t)
        {
            return easeType switch
            {
                EaseType.Linear => Linear(t),
                EaseType.QuadraticIn => InQuad(t),
                EaseType.QuadraticOut => OutQuad(t),
                EaseType.QuadraticInOut => InOutQuad(t),
                EaseType.CubicIn => InCubic(t),
                EaseType.CubicOut => OutCubic(t),
                EaseType.CubicInOut => InOutCubic(t),
                EaseType.QuarticIn => InQuart(t),
                EaseType.QuarticOut => OutQuart(t),
                EaseType.QuarticInOut => InOutQuart(t),
                EaseType.QuinticIn => InQuint(t),
                EaseType.QuinticOut => OutQuint(t),
                EaseType.QuinticInOut => InOutQuint(t),
                EaseType.SinusoidalIn => InSine(t),
                EaseType.SinusoidalOut => OutSine(t),
                EaseType.SinusoidalInOut => InOutSine(t),
                EaseType.ExponentialIn => InExpo(t),
                EaseType.ExponentialOut => OutExpo(t),
                EaseType.ExponentialInOut => InOutExpo(t),
                EaseType.CircularIn => InCirc(t),
                EaseType.CircularOut => OutCirc(t),
                EaseType.CircularInOut => InOutCirc(t),
                EaseType.ElasticIn => InBack(t),
                EaseType.ElasticOut => OutBack(t),
                EaseType.ElasticInOut => InOutBack(t),
                EaseType.BackIn => InElastic(t),
                EaseType.BackOut => OutElastic(t),
                EaseType.BackInOut => InOutElastic(t),
                EaseType.BounceIn => InBounce(t),
                EaseType.BounceOut => OutBounce(t),
                EaseType.BounceInOut => InOutBounce(t),
                _ => Linear(t),
            };
        }
    }
}