# Changelog

## [0.8.8] 2023.03.15

### Added
- Tween Commands now return `TweenIds` so that you can control them individually.
- Added ability to pause/resume/stop tweens individually using their `tweenId`;

### Fixed
- Generate and Destroy systems optimized to no longer try to schedule jobs without commands, reducing CPU overhead.

## [0.8.7] 2023.03.06

### Fixed
- Fixed an issue with one of the Timeline Play methods being not burst compatible.

## [0.8.6] 2023.03.06

### Changed
- Timelines are now unmanaged and burst compatible.

## [0.8.5] 2023.03.04

### Added
- Unity Spline movement support

## [0.8.4] 2023.02.17

### Changed
- reverted previous ignore of meta files

## [0.8.3] - 2023.02.17

### Added
- Added conditional compilation for HDRP `#if DOTS_TWEEN_HDRP`
- A bunch of HDRP Tweens

### Fixed
- Fixed current value checking with URP Tween functions

## [0.8.2] - 2023.02.16

### Changed
- Added conditional compilation for URP `#if DOTS_TWEEN_URP`

## [0.8.1] - 2023.02.16

### Breaking Changes

- Updated ParallelWriter overloads to have `sortKey` before `entity`
- `TweenParams` is now the last function parameter of Tween Commands
- `duration` is now a required parameter when creating a tween

### Added

- A bunch of URP tweens
  - Base Color Alpha Fading
  - Specular Color
  - Emission Color
  - Metallic
  - Cutoff
  - Bump Scale
  - Occlusion Strength
  - Smoothness

### Internal Changes
- Cleaned up and organized everything into a better folder structure (in my opinion at least)
- Now using `static partial class` for the tween shortcuts so we can separate them into different files. 

## [0.8.0] - 2023.02.15

### Breaking Changes

- Updated Namespaces and tween function naming.
- The `Tween` static class is now seperated into categories. `Tween.Move()` -> `Tween.Move.FromTo()`

### Added

- Timelines (called Sequences in DOTween)
  - This also introduces a new SystemGroup `TimelineSimulationSystemGroup`; runs before `TweenSimulationSystemGroup`
  - Can be accessed using `Tween.Timelines`
- Ability to tween `From` and `To`. Example: `Tween.Move.To()`
  - The original function is now called `FromTo`. Example: `Tween.Rotate.FromTo()`
- Ability to perform "Component Operations" when starting and/or completing a tween.

## [0.7.2] - 2023.01.26

### Changed

- Added URP Material Base Colour Tinting

## [0.7.1] - 2023.01.15

### Changed

- Add Eases from [easings.net](https://easings.net/)
- Removed `EaseDesc`
- Removed `EaseExponent`
- Add `[BurstCompile]` to everything (prayge)

## [0.7.0] - 2023.01.11

### Changed

- Start of Dyon's Fork
- Namespace is now `DotsTween`
  - Runtime reference is now `DotsTween.Runtime`
- Dependency upgrade
  - "com.unity.collections": "2.1.0-pre.6",
  - "com.unity.entities": "1.0.0-pre.15",
  - "com.unity.burst": "1.8.2",
  - "com.unity.mathematics": "1.2.6"
- Stress Test sample now using subscene

## [0.6.0] - 2021.08.08

### Changed

- Dependency upgrade
  - Entities 0.17.0-preview.42 (from 0.14.0-preview.18)
  - Burst 1.4.9 (from 1.3.6)

### Fixed

- Reflection data of generic jobs has not automatically initialized ([#1](https://github.com/NagaChiang/entity-tween/issues/1))

## [0.5.1] - 2021.08.02

### Fixed

- Ended tweening entity is set back to start value ([#2](https://github.com/NagaChiang/entity-tween/issues/2))
- Multiple TweenStates are not destroyed properly at the same time

## [0.5.0] - 2021.01.21

### Changed

- `TweenDestroySystem.DestroyJob` now uses `BufferTypeHandle` instead of `BufferFromEntity`
- Improve performance by marking tweens to be destroyed with `TweenDestroyCommand` instead of `TweenState.LoopCount == 255`

### Fixed

- An exception in Tiny builds when `new TTweenInfo()` in `TweenGenerateSystem` invokes `Activator.CreateInstance()`

## [0.4.1] - 2020.11.17

### Changed

- Add `in` modifiers

### Fixed

- Fix an ambiguous reference for `SpriteRenderer` when Tiny 0.31.0 and 2D Entities both installed

## [0.4.0] - 2020.11.11

### Added

- Tween parameter: Start delay
- Tween support for `SpriteRenderer.Color` in Tiny (`Tween.Tint()`)
- Support for Tiny 0.29.0 and above
- Unit tests
    - `Ease_Delayed`
    - Generation, application and destruction of `Tween.Tint()`
- `EaseDesc` shortcuts (with exponent = 2)
    - `EaseDesc.Linear`
    - `EaseDesc.SmoothStart`
    - `EaseDesc.SmoothStop`
    - `EaseDesc.SmoothStep`

### Changed

- Set WriteGroup for `Translation`, `Rotation` and `Scale`
- Replace `TweenState.LOOP_COUNT_INFINITE` with `Tween.Infinite`

### Fixed

- Parallel writing with `[NativeDisableContainerSafetyRestriction]` in `TweenDestroySystem`

## [0.3.0] - 2020.11.03

### Added

- `Tween` functions overloads taking `TweenParams`
- `TweenParams` default values in constructor
- `TweenParams` overrides `ToString()`
- Unit tests
    - Ease
    - Pause
    - Resume
    - Stop
    - Ping-pong
    - Loop
    - Generate, apply and destroy
        - `Translation`
        - `Rotation`
        - `NonUniformScale`

### Changed

- `TweenParams` parameters order in constructor
- `TweenStopSystem` schedules structural changes to `EndSimulationEntityCommandBufferSystem` (was `BeginSimulationEntityCommandBufferSystem`)
- Systems use `World` (was `World.DefaultGameObjectInjectionWorld`)

## [0.2.0] - 2020.10.23

### Added

- GitHub Actions to build the default scene for Win64
- `Editor.BuildUtils` for building the default scene with GitHub Actions
- Sample: Stress Test

### Changed

- Rename buffer element `Tween` to `TweenState`
- Rename static class `EntityTween` to `Tween`
- Replace assertions with if-else checks to be compatible to Burst

### Fixed

- Typo "Crossfade"

## [0.1.0] - 2020.10.08

### Added

- `Ease` utilities for interpolation
- `Translation`, `Rotation` and `Scale` tween support
- Pause, resume and stop tweens on the entity
