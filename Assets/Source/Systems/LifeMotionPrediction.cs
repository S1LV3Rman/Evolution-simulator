using System.Collections.Generic;
using System.Linq;
using Leopotam.Ecs;

namespace Source
{
    public class LifeMotionPrediction : IEcsRunSystem
    {
        private readonly IConfig _config = default;
        private readonly IRandomService _random = default;

        private readonly EcsFilter<Life, Motion, Alive> _movableLife = default;
        private readonly EcsFilter<Tick> _ticks = default;
        private readonly EcsFilter<WorldMap> _worlds = default;
        
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
                    var possiblePaths = GetPossiblePaths(coord, distance);
                    var randomPath = _random.Range(0, possiblePaths.Length - 1);

                    var path = possiblePaths[randomPath];
                    entity.Get<Moved>().Path.AddRange(path);
                }
            }
        }

        private MapCoord[][] GetPossiblePaths(MapCoord startCoord, int distance)
        {
            var paths = new List<MapCoord[]>();
            var passed = new List<MapCoord> { startCoord };

            var activePaths = new Queue<List<MapCoord>>();
            activePaths.Enqueue(new List<MapCoord> { startCoord });

            var map = _worlds.Get1(0).Value;
            var edge = _config.WorldSize;

            do
            {
                var path = activePaths.Dequeue();
                var lastStep = path.Last();
                for (var d = Direction.TopRight; d <= Direction.TopLeft; ++d)
                {
                    var coord = lastStep.GetNeighbourCoord(d);
                    coord.InvertOverEdge(edge);

                    if (map[coord].Type != CellType.Empty &&
                        map[coord].Type != CellType.Trail)
                        passed.Add(coord);

                    if (!passed.Contains(coord))
                    {
                        var newPath = new List<MapCoord>(path) { coord };
                        paths.Add(newPath.ToArray());

                        if (newPath.Count <= distance)
                            activePaths.Enqueue(newPath);

                        passed.Add(coord);
                    }
                }
            } while (activePaths.Count > 0);

            return paths.ToArray();
        } 
    }
}