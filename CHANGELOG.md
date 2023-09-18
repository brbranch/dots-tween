# Changelog

## [0.10.1] 2023.09.18

### Fixed
- URP static tween methods no longer `private` (don't use copy-paste friends)

## [0.10.0] 2023.09.18

### Added
- Aggressively inline `static` methods for minor performance boost.

### Changed
- Updated dependencies:
  - `"com.unity.entities": "1.0.16"`
  - `"com.unity.burst": "1.8.8"`
  - `"com.unity.entities.graphics": "1.0.16"`

## [0.9.1] 2023.07.31

### Fixed

- Elastic and Back eases no longer mapped to each other.

## [0.9.0] 2023.07.27

### Added

- Added ability to perform multiple component operations by using a `ComponentTypeSet`. (breaking change)

### Changed

- Updated Unity to 2022.3 LTS
- Updated dependencies:
  - `"com.unity.collections": "2.1.4"`
  - `"com.unity.entities": "1.0.11"`
  - `"com.unity.burst": "1.8.7"`
  - `"com.unity.entities.graphics": "1.0.11"`

## [0.8.17] 2023.04.28

### Added
- Added additional spline alignment settings:
  - can specify the axis of alignment; useful for keeping orientation, eg: 2d splines in 3d space.
  - can specify a rotational offset for the new axis alignment modes.

## [0.8.16] 2023.04.17

### Fixed

- Spline Movement wasn't working due to premature cleanup

## [0.8.15] 2023.04.03

### Fixed

- Fixed an issue where tweens were prematurely destroyed if you tried to stop and play tweens on the same frame.
- `TweenState.LOOP_COUNT_INFINITE` is actually `public` now. Use this for denoting that your tween/timeline is infinite looping.

## [0.8.14] 2023.03.30

### Changed

- Slightly optimized `Tween.Controls.Stop` by skipping the step of creating a stop command. We now directly create a destroy command. This should allow the tween to stop earlier than the flow before.
- `Tween.Controls.StopAll(ref EntityManager, in Entity)` now uses this more optimized stop flow as well.
- Timeline `OnComplete` component operations are now also performed when a timeline is stopped prematurely.

## [0.8.13] 2023.03.28

### Fixed

- Fixed an issue where the new timeline id generation was not burst compatible. (remember kids, always test your code.)

## [0.8.12] 2023.03.28

### Fixed

- Timelines should now properly wait for previous tweens (of the same type) to be cleaned up before trying to play the new one.
- Timeline Id generation now using more unique hashcodes.

## [0.8.11] 2023.03.23

### Changed

- Updated Unity DOTS
  - com.unity.collections: `2.1.0-pre.11` to `2.1.0-pre.18`
  - com.unity.entities: `1.0.0-pre.47` to `1.0.0-pre.65`
  - com.unity.burst: `1.8.3` to `1.8.4`
  - com.unity.entities.graphics: `1.0.0-pre.44` to `1.0.0-pre.65`

## [0.8.10] 2023.03.23

### Fixed

- Jobless tween generation now properly cleans up commands.
- TweenDestroySystem no longer continuously runs when no destroy commands are present.

## [0.8.9] 2023.03.22

### Fixed

- TweenCommands should now generate more unique ids. Ids are now `uint` instead of `int`.

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
