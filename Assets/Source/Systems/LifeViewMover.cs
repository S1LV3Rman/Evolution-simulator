using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine.Tilemaps;

namespace Source.Systems
{
    public class LifeViewMover : IEcsInitSystem, IEcsRunSystem
    {
        private readonly IConfig _config = default;
        private readonly ISceneContext _scene = default;

        private readonly EcsFilter<Life, Moved> _movedLife = default;

        private Tilemap _map;
        
        public void Init()
        {
            _map = _scene.Map;
        }
        
        public void Run()
        {
            if (_movedLife.IsEmpty()) return;

            foreach (var i in _movedLife)
            {
                var entity = _movedLife.GetEntity(i);
                
                var oldCoord = entity.Get<Coord>().Value;
                var oldPoint = GridMath.GetPos(oldCoord);
                
                PaintMath.Clear(_map, oldPoint);
            }

            foreach (var i in _movedLife)
            {
                var entity = _movedLife.GetEntity(i);

                var newCoord = entity.Get<Moved>().Coord;
                var newPoint = GridMath.GetPos(newCoord);

                var tile = entity.Get<Tile>().Value;
                PaintMath.Put(_map, tile, newPoint);

                entity.Get<Coord>().Value = newCoord;
                entity.Del<Moved>();
            }
        }
    }
}