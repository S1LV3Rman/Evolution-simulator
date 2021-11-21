using Leopotam.Ecs;
using UnityEngine;

namespace Source.Systems
{
    public class TestGenerator : IEcsInitSystem
    {
        private readonly IConfig _config = default;
        private readonly ISceneContext _scene = default;
        private readonly IRandomService _random = default;
        
        public void Init()
        {
            var dirtTile = _config.DirtTile;
            var map = _scene.GroundMap;
            var radius = _config.WorldRadius;
            
            var samplesCount = _config.TestVariables[0];
            
            
            for (var i = 0; i < samplesCount; ++i)
            {
                var top = _random.Range(-radius, radius + 1);
                var bot = top > 0 ? 
                    _random.Range(-radius + top, radius + 1) :
                    _random.Range(-radius, radius + top + 1);
                var coord = new MapCoord(top, bot);
                var point = GridMath.GetPos(coord);
                PaintMath.Put(map, point, dirtTile);
            }


            // for (var i = 0; i < radius; ++i)
            // {
            //     for (var j = 0; j < radius; ++j)
            //     {
            //         var coord = new MapCoord(i, j);
            //         var point = GridMath.GetCoord(coord);
            //         PaintMath.Put(map, point, dirtTile);
            //     }
            // }
        }
    }
}