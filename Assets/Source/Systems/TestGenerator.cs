using Leopotam.Ecs;
using UnityEngine;

namespace Source.Systems
{
    public class TestGenerator : IEcsInitSystem
    {
        private readonly IConfig _config = default;
        private readonly ISceneContext _scene = default;
        
        public void Init()
        {
            var dirtTile = _config.DirtTile;
            var radius = _config.WorldRadius;
            var map = _scene.GroundMap;

            var center = Vector3Int.zero;
            
            var pattern = PaintMath.GenerateFullCircle(center, radius);
            PaintMath.Fill(map, pattern, dirtTile);
        }
    }
}