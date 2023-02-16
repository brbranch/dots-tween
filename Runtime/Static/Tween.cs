using Unity.Burst;

namespace DotsTween
{
    /// <summary>
    /// This file is just for providing burst compile
    /// </summary>
    
    [BurstCompile]
    public static partial class Tween
    {
#if DOTS_TWEEN_URP
        [BurstCompile]
        public static partial class URP
        {
        }
#endif
    }
}