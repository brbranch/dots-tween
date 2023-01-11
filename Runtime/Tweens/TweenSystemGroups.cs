﻿using Unity.Entities;

namespace DotsTween.Tweens
{
    // Update Order:
    // - TweenGenerateSystemGroup
    //   - TweenGenerateSystem(s)
    // - TweenEaseSystem
    // - TweenApplySystemGroup
    //   - TweenTranslationSystem
    //   - TweenRotationSystem
    //   - TweenScaleSystem
    //   - etc.
    // - TweenStateSystem
    // - TweenResumeSystem
    // - TweenStopSystem
    // - TweenDestroySystemGroup
    //   - TweenDestroySystem(s)

    [UpdateInGroup(typeof(SimulationSystemGroup))]
    internal partial class TweenSimulationSystemGroup : ComponentSystemGroup {}

    [UpdateInGroup(typeof(TweenSimulationSystemGroup))]
    [UpdateBefore(typeof(TweenEaseSystem))]
    internal partial class TweenGenerateSystemGroup : ComponentSystemGroup {}

    [UpdateInGroup(typeof(TweenSimulationSystemGroup))]
    [UpdateAfter(typeof(TweenEaseSystem))]
    internal partial class TweenApplySystemGroup : ComponentSystemGroup {}

    [UpdateInGroup(typeof(TweenSimulationSystemGroup))]
    [UpdateAfter(typeof(TweenApplySystemGroup))]
    internal partial class TweenDestroySystemGroup : ComponentSystemGroup {}
}