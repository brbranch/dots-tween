// ReSharper disable UnassignedField.Global

using Unity.Burst;
using Unity.Entities;

namespace DotsTween.Tweens
{
    [BurstCompile]
    public struct ComponentOperations
    {
        public ComponentTypeSet Add;
        public ComponentTypeSet Remove;
        public ComponentTypeSet Enable;
        public ComponentTypeSet Disable;
        
        [BurstCompile]
        public void Perform(ref EntityManager entityManager, in Entity target)
        {
            if (Add.Length > 0) entityManager.AddComponent(target, Add);
            if (Remove.Length > 0) entityManager.RemoveComponent(target, Remove);

            for (int index = 0; index < Enable.Length; ++index)
            {
                entityManager.SetComponentEnabled(target, Enable.GetComponentType(index), true);
            }
            
            for (int index = 0; index < Disable.Length; ++index)
            {
                entityManager.SetComponentEnabled(target, Disable.GetComponentType(index), false);
            }
        }

        [BurstCompile]
        public void Perform(ref EntityCommandBuffer ecb, in Entity target)
        {
            if (Add.Length > 0) ecb.AddComponent(target, Add);
            if (Remove.Length > 0) ecb.RemoveComponent(target, Remove);

            for (int index = 0; index < Enable.Length; ++index)
            {
                ecb.SetComponentEnabled(target, Enable.GetComponentType(index), true);
            }
            
            for (int index = 0; index < Disable.Length; ++index)
            {
                ecb.SetComponentEnabled(target, Disable.GetComponentType(index), false);
            }
        }

        [BurstCompile]
        public void Perform(ref EntityCommandBuffer.ParallelWriter parallelWriter, in int sortKey, in Entity target)
        {
            if (Add.Length > 0) parallelWriter.AddComponent(sortKey, target, Add);
            if (Remove.Length > 0) parallelWriter.RemoveComponent(sortKey, target, Remove);

            for (int index = 0; index < Enable.Length; ++index)
            {
                parallelWriter.SetComponentEnabled(sortKey, target, Enable.GetComponentType(index), true);
            }
            
            for (int index = 0; index < Disable.Length; ++index)
            {
                parallelWriter.SetComponentEnabled(sortKey, target, Disable.GetComponentType(index), false);
            }
        }

        [BurstCompile]
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = GetHashCodeFromComponentTypes(Add);
                hashCode = (hashCode * 1261) ^ GetHashCodeFromComponentTypes(Remove);
                hashCode = (hashCode * 1261) ^ GetHashCodeFromComponentTypes(Enable);
                hashCode = (hashCode * 1261) ^ GetHashCodeFromComponentTypes(Disable);

                return hashCode;
            }
        }
        
        [BurstCompile]
        private static int GetHashCodeFromComponentTypes(in ComponentTypeSet set)
        {
            var code = 0;

            for (int x = 0; x < set.Length; ++x)
            {
                code = (code * 5657) ^ set.GetComponentType(x).GetHashCode();
            }

            return code;
        }
    }
}