using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Source.Systems
{
    public class LifeViewCreator : IEcsInitSystem, IEcsRunSystem
    {
        private readonly IConfig _config = default;
        private readonly ISceneContext _scene = default;

        private readonly EcsFilter<Life, Awake> _awakenedLife = default;

        private TileBase _lifeTile;
        private Tilemap _map;
        
        public void Init()
        {
            _lifeTile = _config.LifeTile;
            _map = _scene.LandMap;
        }
        
        public void Run()
        {
            if (_awakenedLife.IsEmpty()) return;

            foreach (var i in _awakenedLife)
            {
                var entity = _awakenedLife.GetEntity(i);
                
                var coord = entity.Get<Coord>().Value;
                var point = GridMath.GetPos(coord);
                PaintMath.Put(_map, _lifeTile, point);
                
                var life = _awakenedLife.GetEntity(i);
                life.Get<Tile>().Value = _lifeTile;
                life.Del<Awake>();
                life.Get<Alive>();
            }
        }
    }
}