using Unity.Burst;

namespace DotsTween
{
    /// <summary>
    /// This file is just for providing burst compile
    /// </summary>
    
    [BurstCompile]
    public static partial class Tween
    {
        [BurstCompile]
        public static partial class URP
        {
        }
    }
}