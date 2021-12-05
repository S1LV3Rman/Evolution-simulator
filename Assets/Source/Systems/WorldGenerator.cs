﻿using System;
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

        public void Init()
        {
            var groundPoints = CreateLayer(0);
            var landPoints = CreateLayer(1);
            
            var worldEntity = _world.NewEntity();
            ref var map = ref worldEntity.Get<WorldMap>().Value;
            map = groundPoints.ToDictionary(point => point, point => _config.GrassTile);
            foreach (var point in landPoints)
                map.Add(point, null);
            
            var changesEntity = _world.NewEntity();
            ref var changedCells = ref changesEntity.Get<ChangedCells>().Value;
            changedCells = groundPoints.ToList();
        }

        private MapCoord[] CreateLayer(int layer)
        {
            return new MapCoord(layer).GetRhomb(_config.WorldSize);
        }
    }
}