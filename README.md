# Entity Tween

![GitHub release (latest SemVer)](https://img.shields.io/github/v/release/dyonng/dots-tween)
![GitHub package.json version (branch)](https://img.shields.io/github/package-json/v/dyonng/dots-tween/develop)  
![GitHub](https://img.shields.io/github/license/dyonng/dots-tween)
![GitHub repo size](https://img.shields.io/github/repo-size/dyonng/dots-tween)

Entity compatible tween library for Unity ECS/DOTS.
Now uses Entities Graphics library from Unity.

## Table of Contents

- [Demonstration](#demonstration)
- [Features](#features)
- [Tweening Support](#tweening-support)
- [Dependencies](#dependencies)
- [Installation](#installation)
- [Examples](#examples)
  - [Move the entity](#move-the-entity)
  - [Stop the entity](#stop-the-entity)
  - [Loop infinitely](#loop-infinitely)
  - [Perform Component Operations On Start/Complete](#perform-component-operations-on-startcomplete)
  - [Creating and Playing Timelines](#creating-and-playing-timelines)
  - [Check if the entity is tweening](#check-if-the-entity-is-tweening)
- [Workflow](#workflow)
  - [Command](#command)
  - [Generation](#generation)
  - [Easing](#easing)
  - [Applying](#applying)
  - [Checking State](#checking-state)
  - [Destroying](#destroying)
- [Donation](#donation)

## Demonstration

![](https://i.imgur.com/3GM0RCE.gif)

[Link to the larger gif](https://i.imgur.com/3oZnviK.gif)

### Configuration

- 70000 tweening entities
    - `Translation`
    - `Rotation`
    - `Scale`
- Burst
    - Leak Detection: Off
    - Safety Checks: Off
    - Synchronous Compilation: On
    - Jobs Debugger: Off
- GPU instancing

### Hardware

- Intel i7-8700 @ 3.2GHz
- NVIDIA GeForce GTX 1660 Ti

## Features

- Pause, resume and stop all or individual tweens on an entity
- Multiple types of active tweens on the same entity at the same time
- Ping-pong (on tweens)
- Loop
- Start delay
- Timelines
- Ease library (from [easings.net](https://easings.net))

## Tweening Support

### Transform
 - Translation
 - Rotation
 - Spline Translation (with optional Rotation)
 - Scale (Uniform)
 - NonUniformScale

### URP
 - Base Color Tinting
 - Base Color Alpha Fading
 - Specular Color
 - Emission Color
 - Metallic
 - Cutoff
 - Bump Scale
 - Occlusion Strength
 - Smoothness

### HDRP
 - Base Color Tinting
 - Base Color Alpha Fading
 - Unlit Color Tinting
 - Unlit Color Alpha Fading
 - AlphaCutoff
 - AmbientOcclusionRemapMax
 - AmbientOcclusionRemapMin
 - DetailAlbedoScale
 - DetailNormalScale
 - DetailSmoothnessScale
 - DiffusionProfileHash
 - EmissiveColor
 - Metallic
 - Smoothness
 - SmoothnessRemapMax
 - SmoothnessRemapMin
 - SpecularColor
 - Thickness
 - ThicknessRemap

## Dependencies

 - com.unity.collections: `2.1.0-pre.18`
 - com.unity.entities: `1.0.0-pre.65`
 - com.unity.burst: `1.8.4`
 - com.unity.mathematics: `1.2.6`
 - com.unity.entities.graphics: `1.0.0-pre.65`

### For Spline Support
 - "com.unity.splines" : "2.1.0"

## Installation

Entity Tween is a Unity package. You can [install it from the git URL](https://docs.unity3d.com/2022.1/Documentation/Manual/upm-ui-giturl.html) in Unity package manager.

Or, you can edit `Packages/manifest.json` manually, adding git URL as a dependency:

```json
{
    "dependencies": {
        "com.dyonng.dotstween": "https://github.com/dyonng/dots-tween.git"
    }
}
```

For more information, please visit [Unity documentation](https://docs.unity3d.com/2020.1/Documentation/Manual/upm-git.html).

## Examples

The main entry point of the library is the `Tween` class. All functionality have overloads to support `EntityManager`, `EntityCommandBuffer` and `EntityCommandBuffer.ParallelWriter`.

### Move the entity

```cs
float3 start = new float3(0.0f, 0.0f, 0.0f);
float3 end = new float3(1.0f, 1.0f, 1.0f);
float duration = 5.0f;

Tween.Move.FromTo(ref entityManager, entity, start, end, duration);
Tween.Move.FromTo(ref commandBuffer, entity, start, end, duration);
Tween.Move.FromTo(ref parallelWriter, entity, sortKey, start, end, duration);
```

### Stop the entity

This will stop all tweens on the entity.
```cs
Tween.Controls.StopAll(entityManager, entity);
```

To stop a specific tween on an entity, use its returned `tweenId`.
```csharp
var translationTweenId = Tween.Move.FromTo(ref entityManager, entity, start, end, duration);

Tween.Controls.Stop(ref ecb, entity, translationTweenId);
```

### Loop infinitely

When `LoopCount` is -1, it means loop the tween infinitely. It's recommended to use `Tween.Infinite` in case it changes in the future.

```cs
Tween.Move.FromTo(ref entityManager, entity, start, end, duration, new TweenParams { LoopCount = Tween.Infinite });
```

### Perform Component Operations On Start/Complete

Tweens with infinite LoopCounts do not support `OnComplete` component operations.

```csharp
Tween.Scale.FromTo(ref entityManager, entity, start, end, duration, new TweenParams
{
    LoopCount = 2,
    OnStart = new ComponentOperations
    {
        Add = ComponentType.ReadOnly<AnotherTag>(),
    },
    OnComplete = new ComponentOperations
    {
        Add = ComponentType.ReadOnly<ExampleTag>(),
        Remove = ComponentType.ReadOnly<AnotherTag>(),
    }
});
```

### Creating and Playing Timelines

Tweens with infinite LoopCounts are not supported within a timeline.

```csharp
var duration = 1.5f;

Tween.Timeline.Create(out var timeline);
timeline.Append(obj, new TweenScaleCommand(1f, 2.2f, duration))
timeline.Insert(2f, obj, new TweenTranslationCommand(float3.zero, new float3(1f, 2f, 3f), duration))
timeline.SetStartDelay(0.5f)
timeline.AddOnComplete(new ComponentOperations { Add = ComponentType.ReadOnly<ExampleTag>() })
timeline.Play(ref entityCommandBuffer);
```

### Check if the entity is tweening

Any entity with component `TweenState` is tweening; that is, to know if the entity is tweening, check if the entity has any `TweenState` component in any way.

```cs
if (EntityManager.HasComponent<TweenState>(entity))
{
    Debug.Log("It's tweening!");
}
```

## Workflow

### Command

When starting a tween by calling `Tween`'s functions (e.g. `Tween.Move.FromTo()`), it creates a tween command component of its kind (e.g. `TweenTranslationCommand`) containing the tweening data on the target entity.

If starting multiple tweens with the same type consequently, the command component will be overridden by the last one, which means only the last tween will be successfully triggered.

### Generation

`TweenGenerateSystem` is an abstract generic system, which will take the commands of its kind, then append a `TweenState` with an unique tween ID to the `DynamicBuffer` of the target entity. Also, it creates another component with the same tween ID, storing the start and end information of the tween.

Making `TweenState` an `IBufferElementData` allows multiple active tweens on the same entity at the same time, while the other part of the tween data, e.g. `TweenTranslation`, is an `IComponentData`, which ensures that there must be only one tween of the certain type.

For example, `TweenTranslationGenerateSystem`, which inherits `TweenGenerateSystem`, will take `TweenTranslationCommand` then create `TweenState` and `TweenTranslation` on the target entity.

### Easing

`TweenEaseSystem` iterates all `TweenState` components and update the elapsed time of the tween, then calculate the progress by `EasingFunctions.Ease(tween.EaseType, tween.GetNormalizedTime())` for later use.

### Applying

For every type of tweens, there is a system responsible for applying the value to the target component.

For example, `TweenTranslationSystem` takes `TweenState` and `TweenTranslation` and interpolate the value depending on the eased percentage, then apply it to `Translation`.

### Checking State

`TweenStateSystem` iterates all `TweenState` components checking if they're about to be looped, ping-ponged or destroyed. The tween will loop infinitely when its `TweenState.LoopCount < 0`; however, if it just ticked to -1 from 0, the system will mark it as pending destroy with `TweenDestroyCommand`;

### Destroying

`TweenDestroySystem` is also an abstract generic system for each type of tween to implement, which destroys `TweenState` marked by `TweenStateSystem` earlier and its corresponding tween data component.

For example, `TweenTranslationDestroySystem` will be responsible for destroying `TweenState` and `TweenTranslation`.

## Donation

### NagaChiang's Donation Link (Original Author):
[![ko-fi](https://www.ko-fi.com/img/githubbutton_sm.svg)](https://ko-fi.com/C0C12EHR2)