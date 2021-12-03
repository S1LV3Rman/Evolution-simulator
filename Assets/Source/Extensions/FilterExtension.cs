using Leopotam.Ecs;

namespace Source.Systems
{
    public static class FilterExtension
    {
        public static void Clear(this EcsFilter filter)
        {
            if (!filter.IsEmpty())
                foreach (var i in filter)
                    filter.GetEntity(i).Destroy();
        }
    }
}