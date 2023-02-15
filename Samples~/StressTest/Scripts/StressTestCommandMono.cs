using DotsTween.Math;
using Unity.Entities;
using UnityEngine;

namespace DotsTween.Samples.StressTest
{
    public class StressTestCommandMono : MonoBehaviour
    {
        public GameObject Prefab;
        public uint Count = 10000;
        
        [Header("Move")]
        public float MoveDuration = 5;
        public EaseType MoveEaseType = EaseType.QuadraticInOut;
        public bool MoveIsPingPong = true;
        public short MoveLoopCount = -1;
        public float StartMoveRadius = 2;
        public float EndMoveRadius = 5;
        
        [Header("Rotate")]
        public float RotateDuration = 5;
        public EaseType RotateEaseType = EaseType.QuadraticInOut;
        public bool RotateIsPingPong = true;
        public short RotateLoopCount = -1;
        public float MinRotateDegree = 160;
        public float MaxRotateDegree = 200;
        
        [Header("Scale")]
        public float ScaleDuration = 5;
        public EaseType ScaleEaseType = EaseType.QuadraticInOut;
        public bool ScaleIsPingPong = true;
        public short ScaleLoopCount = -1;
        public float MinStartScale = 0.1f;
        public float MaxStartScale = 0.15f;
        public float MinEndScale = 0.2f;
        public float MaxEndScale = 0.25f;

        public partial class StressTestBaker : Baker<StressTestCommandMono>
        {
            public override void Bake(StressTestCommandMono authoring)
            {
                AddComponent(new StressTestCommand
                {
                    Prefab = GetEntity(authoring.Prefab),
                    Count = authoring.Count, 
                    MoveDuration = authoring.MoveDuration,  
                    MoveEaseType = authoring.MoveEaseType,
                    MoveIsPingPong = authoring.MoveIsPingPong,
                    MoveLoopCount = authoring.MoveLoopCount, 
                    StartMoveRadius = authoring.StartMoveRadius,   
                    EndMoveRadius = authoring.EndMoveRadius, 
                    RotateDuration = authoring.RotateDuration,
                    RotateEaseType = authoring.RotateEaseType,
                    RotateIsPingPong = authoring.RotateIsPingPong,  
                    RotateLoopCount = authoring.RotateLoopCount,   
                    MinRotateDegree = authoring.MinRotateDegree,   
                    MaxRotateDegree = authoring.MaxRotateDegree,   
                    ScaleDuration = authoring.ScaleDuration, 
                    ScaleEaseType = authoring.ScaleEaseType,
                    ScaleIsPingPong = authoring.ScaleIsPingPong,   
                    ScaleLoopCount = authoring.ScaleLoopCount,
                    MinStartScale = authoring.MinStartScale, 
                    MaxStartScale = authoring.MaxStartScale, 
                    MinEndScale = authoring.MinEndScale,   
                    MaxEndScale = authoring.MaxEndScale,   
                });
            }
        }
    }
    
    public struct StressTestCommand : IComponentData
    {
        public Entity Prefab;
        public uint Count;

        public float MoveDuration;
        public EaseType MoveEaseType;
        public bool MoveIsPingPong;
        public short MoveLoopCount;
        public float StartMoveRadius;
        public float EndMoveRadius;

        public float RotateDuration;
        public EaseType RotateEaseType;
        public bool RotateIsPingPong;
        public short RotateLoopCount;
        public float MinRotateDegree;
        public float MaxRotateDegree;

        public float ScaleDuration;
        public EaseType ScaleEaseType;
        public bool ScaleIsPingPong;
        public short ScaleLoopCount;
        public float MinStartScale;
        public float MaxStartScale;
        public float MinEndScale;
        public float MaxEndScale;
    }
}