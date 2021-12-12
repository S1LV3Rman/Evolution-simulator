using System;
using System.Collections.Generic;
using System.Linq;
using Leopotam.Ecs;
using Unity.VisualScripting;
using UnityEngine;

namespace Source
{
    public class WorldGenerator : IEcsInitSystem
    {
        private readonly EcsWorld _world = default;
        private readonly IConfig _config = default;
        private readonly IDesignConfig _designConfig = default;

        public void Init()
        {
            var groundPoints = CreateLayer(0);
            var landPoints = CreateLayer(1);
            
            var mapEntity = _world.NewEntity();
            var map = mapEntity.Get<SimulationMap>().Value;
            map = groundPoints.ToDictionary(point => point,
                point => new Cell(CellType.Grass, _designConfig.GrassTile));
            foreach (var point in landPoints)
                map.Add(point, new Cell());
            
            var changesEntity = _world.NewEntity();
            ref var changedCells = ref changesEntity.Get<ChangedCells>().Value;
            changedCells = groundPoints;
        }

        private MapCoord[] CreateLayer(int layer)
        {
            return new MapCoord(layer).GetRhomb(_config.WorldSize);
        }
    }
}