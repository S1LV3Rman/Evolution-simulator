using Leopotam.Ecs;
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
            var radius = _config.WorldSize;
            var map = _scene.Map;
            
            PaintMath.Rhomb(map, grassTile, Vector3Int.zero, radius);

            // var range = radius * 2 + 1;
            // PaintMath.Rhomb(map, dirtTile, GridMath.GetPos(new MapCoord(range, 0)), radius);
            // PaintMath.Rhomb(map, dirtTile, GridMath.GetPos(new MapCoord(-range, 0)), radius);
            // PaintMath.Rhomb(map, dirtTile, GridMath.GetPos(new MapCoord(0, range)), radius);
            // PaintMath.Rhomb(map, dirtTile, GridMath.GetPos(new MapCoord(0, -range)), radius);
            //
            // PaintMath.Rhomb(map, dirtTile, GridMath.GetPos(new MapCoord(range, range)), radius);
            // PaintMath.Rhomb(map, dirtTile, GridMath.GetPos(new MapCoord(-range, -range)), radius);
            // PaintMath.Rhomb(map, dirtTile, GridMath.GetPos(new MapCoord(-range, range)), radius);
            // PaintMath.Rhomb(map, dirtTile, GridMath.GetPos(new MapCoord(range, -range)), radius);
        }
    }
}