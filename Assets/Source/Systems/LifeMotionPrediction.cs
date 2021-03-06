using System;
using System.Collections.Generic;
using System.Linq;
using Leopotam.Ecs;
using UnityEngine;

namespace Source
{
    public class LifeMotionPrediction : IEcsRunSystem
    {
        private readonly IConfig _config = default;
        private readonly IRandomService _random = default;

        private readonly EcsFilter<Life, Movement, Alive> _movableLife = default;
        private readonly EcsFilter<Tick> _ticks = default;
        private readonly EcsFilter<SimulationMap> _maps = default;
        
        public void Run()
        {
            if (_ticks.IsEmpty()) return;
            if (_movableLife.IsEmpty()) return;

            ref var worldMap = ref _maps.Get1(0);
            var friction = _config.WorldGravitation * .2f;
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

                    ref var movement = ref entity.Get<Movement>();
                    var acceleration = movement.Acceleration.Clamp(-movement.Speed, movement.MaxSpeed - movement.Speed);
                    var effort = Mathf.Abs(friction + acceleration);
                    movement.EnergySpentPerMass += effort;
                    movement.Speed += acceleration;
                    var distance = movement.Speed;
                    var path = GetRandomPath(ref worldMap, coord, distance);
                    entity.Get<Moved>().Path.AddRange(path);
                }
            }
        }

        private MapCoord[] GetRandomPath(ref SimulationMap simulationMap, MapCoord startCoord, int distance)
        {
            var path = new List<MapCoord> { startCoord };
            
            var map = simulationMap.Value;
            var edge = _config.WorldSize;

            var lastStep = startCoord;
            for (var i = 0; i < distance; ++i)
            {
                var possibleSteps = map.GetFreeNeighborCoords(lastStep, edge);
                if (possibleSteps.Count <= 0) break;
                
                var stepId = _random.Range(0, possibleSteps.Count - 1);
                path.Add(possibleSteps[stepId]);
                lastStep = path.Last();
            }
            
            return path.ToArray();
        }

        private MapCoord[] GetRandomShortestPath(MapCoord startCoord, int distance)
        {
            var possiblePaths = GetPossiblePaths(startCoord, distance);
            var pathId = _random.Range(0, possiblePaths.Length - 1);
            
            return possiblePaths[pathId];
        }

        private MapCoord[][] GetPossiblePaths(MapCoord startCoord, int distance)
        {
            var paths = new List<MapCoord[]>();
            var passed = new List<MapCoord> { startCoord };

            var activePaths = new Queue<List<MapCoord>>();

            var map = _maps.Get1(0).Value;
            var edge = _config.WorldSize;

            var path = new List<MapCoord>();
            var lastStep = startCoord;
            do
            {
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
                path = activePaths.Dequeue();
                lastStep = path.Last();
            } while (activePaths.Count > 0);

            return paths.ToArray();
        } 
    }
}