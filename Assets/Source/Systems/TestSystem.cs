using Leopotam.Ecs;
using UnityEngine;

namespace Source.Systems
{
    public class TestSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorld _world = default;
        private readonly IConfig _config = default;
        private readonly ISceneContext _scene = default;
        private readonly IRandomService _random = default;

        private readonly EcsFilter<Life, Motion, Alive> _movableLife = default;
        private readonly EcsFilter<Tick> _ticks = default;

        private int _radius;

        public void Init()
        {
        }

        public void Run()
        {
            if (_ticks.IsEmpty()) return;
            if (_movableLife.IsEmpty()) return;

            var ticksCount = _ticks.Get1(0).Count;
            for (var t = 0; t < ticksCount; ++t)
            {
                foreach (var i in _movableLife)
                {
                    var entity = _movableLife.GetEntity(i);

                    var coord = entity.Has<Moved>() ? 
                        entity.Get<Moved>().Coord : 
                        entity.Get<Coord>().Value;
                    
                    var distance = entity.Get<Motion>().Speed;
                    for (var j = 0; j < distance; ++j)
                    {
                        var direction = (GridMath.Direction) _random.Range(0, 5);
                        coord.Move(direction, distance);
                    }
                    
                    entity.Get<Moved>().Coord = coord;
                }
            }
        }
    }
}