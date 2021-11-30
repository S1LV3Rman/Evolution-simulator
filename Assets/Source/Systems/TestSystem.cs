using Leopotam.Ecs;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Source.Systems
{
    public class TestSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorld _world = default;
        private readonly IConfig _config = default;
        private readonly ISceneContext _scene = default;
        private readonly IRandomService _random = default;

        private readonly EcsFilter<Life, Motion, Alive> _movableLife = default;
        private readonly EcsFilter<Tick> _ticks = default;

        private int _radius;
        private Tilemap _map;
        private TileBase _dirt;
        private TileBase _grass;

        public void Init()
        {
            _map = _scene.Map;
            _dirt = _config.DirtTile;
            _grass = _config.GrassTile;
            
            PaintMath.Put(_map, _dirt, new Vector3Int(0, 0, 0));
        }

        public void Run()
        {
            if(Input.GetMouseButton(1))
                PaintMath.Put(_map, _grass, new Vector3Int(0, 0, 1));
            
            if(Input.GetMouseButton(0))
                PaintMath.Clear(_map, new Vector3Int(0, 0, 1));
        }
    }
}