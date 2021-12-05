using System.Collections.Generic;
using System.Linq;
using Leopotam.Ecs;

namespace Source
{
    public class LifeMotion : IEcsInitSystem, IEcsRunSystem
    {
        private readonly IConfig _config = default;
        private readonly IRandomService _random = default;

        private readonly EcsFilter<Life, Motion, Alive> _movableLife = default;
        private readonly EcsFilter<Tick> _ticks = default;
        private readonly EcsFilter<WorldMap> _worlds = default;

        private int _worldRadius;

        public void Init()
        {
            _worldRadius = _config.WorldSize;
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
                        entity.Get<Moved>().Path.Last() : 
                        entity.Get<Coord>().Value;
                    
                    if(!entity.Has<Moved>())
                        entity.Get<Moved>().Path = new List<MapCoord>();
                    
                    var distance = entity.Get<Motion>().Speed;
                    var possibleDestinations = coord.GetCircle(distance);
                    
                    for (var j = 0; j < distance; ++j)
                    {
                        var direction = (GridMath.Direction) _random.Range(0, 5);
                        coord.Move(direction);
                        coord.InvertOverEdge(_worldRadius);
                        
                        entity.Get<Moved>().Path.Add(coord);
                    }
                }
            }
        }
    }
}