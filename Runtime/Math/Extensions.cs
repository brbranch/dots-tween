using System.Runtime.CompilerServices;
using Unity.Burst;
using Unity.Mathematics;
using UnityEngine;

namespace DotsTween.Math
{
    [BurstCompile]
    internal static class Extensions
    {
        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToFloat4(in this Color color, out float4 f4)
        {
            f4 = new float4(color.r, color.g, color.b, color.a);
        }

        /// <summary>
        /// Truncates alpha channel.
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToFloat3(in this Color color, out float3 f3)
        {
            f3 = new float3(color.r, color.g, color.b);
        }
    }
}