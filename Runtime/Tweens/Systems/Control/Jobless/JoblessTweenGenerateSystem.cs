using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Burst;
using Unity.Entities;

namespace DotsTween.Tweens
{
    [BurstCompile]
    [UpdateInGroup(typeof(TweenGenerateSystemGroup))]
    internal abstract partial class JoblessTweenGenerateSystem : SystemBase
    {
        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void TryToAddBuffers(
            out bool buffersWereAdded,
            ref EntityCommandBuffer ecb,
            in Entity entity,
            [MarshalAs(UnmanagedType.U1)] bool hasTweenStateBuffer,
            [MarshalAs(UnmanagedType.U1)] bool hasTweenPauseBuffer,
            [MarshalAs(UnmanagedType.U1)] bool hasTweenResumeBuffer)
        {
            if (!hasTweenStateBuffer) ecb.AddBuffer<TweenState>(entity);
            if (!hasTweenPauseBuffer) ecb.AddBuffer<TweenPauseInfo>(entity);
            if (!hasTweenResumeBuffer) ecb.AddBuffer<TweenResumeInfo>(entity);

            buffersWereAdded = !hasTweenStateBuffer
                || !hasTweenPauseBuffer
                || !hasTweenResumeBuffer;
        }
    }
}