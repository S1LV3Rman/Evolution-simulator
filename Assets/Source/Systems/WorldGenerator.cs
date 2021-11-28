﻿using Leopotam.Ecs;
using UnityEngine;

namespace Source.Systems
{
    public class WorldGenerator : IEcsInitSystem
    {
        private readonly IConfig _config = default;
        private readonly ISceneContext _scene = default;
        
        public void Init()
        {
            var dirtTile = _config.DirtTile;
            var grassTile = _config.GrassTile;
            var radius = _config.WorldRadius;
            var map = _scene.GroundMap;
            
            PaintMath.FullCircle(map, grassTile, Vector3Int.zero, radius);
        }
    }
}