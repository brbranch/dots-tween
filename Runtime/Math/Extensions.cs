using Unity.Mathematics;
using UnityEngine;

namespace DotsTween.Math
{
    internal static class Extensions
    {
        public static float4 ToFloat4(this Color color)
        {
            return new float4(color.r, color.g, color.b, color.a);
        }

        /// <summary>
        /// Truncates alpha channel.
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static float3 ToFloat3(this Color color)
        {
            return new float3(color.r, color.g, color.b);
        }
    }
}