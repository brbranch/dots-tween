using Unity.Burst;
using Unity.Entities;

namespace DotsTween.Tweens
{
    public struct ComponentOperations
    {
        public ComponentType Add;
        public ComponentType Remove;
        public ComponentType Enable;
        public ComponentType Disable;

        [BurstCompile]
        public void Perform(ref EntityManager entityManager, in Entity target)
        {
            if (Add != default)
                entityManager.AddComponent(target, Add);
                
            if (Remove != default)
                entityManager.RemoveComponent(target, Remove);
                
            if (Enable != default)
                entityManager.SetComponentEnabled(target, Enable, true);
                
            if (Disable != default)
                entityManager.SetComponentEnabled(target, Disable, false);
        }

        [BurstCompile]
        public void Perform(ref EntityCommandBuffer ecb, in Entity target)
        {
            if (Add != default)
                ecb.AddComponent(target, Add);
                
            if (Remove != default)
                ecb.RemoveComponent(target, Remove);
                
            if (Enable != default)
                ecb.SetComponentEnabled(target, Enable, true);
                
            if (Disable != default)
                ecb.SetComponentEnabled(target, Disable, false);
        }

        [BurstCompile]
        public void Perform(ref EntityCommandBuffer.ParallelWriter parallelWriter, in int sortKey, in Entity target)
        {
            if (Add != default)
                parallelWriter.AddComponent(sortKey, target, Add);
                
            if (Remove != default)
                parallelWriter.RemoveComponent(sortKey, target, Remove);
                
            if (Enable != default)
                parallelWriter.SetComponentEnabled(sortKey, target, Enable, true);
                
            if (Disable != default)
                parallelWriter.SetComponentEnabled(sortKey, target, Disable, false);
        }

        [BurstCompile]
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = Add.GetHashCode();
                hashCode = (hashCode * 1261) ^ Remove.GetHashCode();
                hashCode = (hashCode * 1261) ^ Enable.GetHashCode();
                hashCode = (hashCode * 1261) ^ Disable.GetHashCode();

                return hashCode;
            }
        }
    }
}