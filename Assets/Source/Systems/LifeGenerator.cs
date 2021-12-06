using System.Collections.Generic;
using System.Linq;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Source
{
    public class LifeGenerator : IEcsInitSystem, IEcsRunSystem
    {
        private readonly IConfig _config = default;
        private readonly EcsWorld _world = default;
        private readonly IRandomService _random = default;
        
        private readonly EcsFilter<WorldMap> _worlds = default;
        private readonly EcsFilter<Life, Awake> _awakenedLife = default;

        public void Init()
        {
            for(var i = 0; i < _config.TestVariables[1]; ++i)
                CreateCell();
        }

        public void Run()
        {
            if (_awakenedLife.IsEmpty()) return;

            foreach (var i in _awakenedLife)
            {
                var entity = _awakenedLife.GetEntity(i);
                entity.Del<Awake>();
                entity.Get<Alive>();
            }
        }

        private void CreateCell()
        {
            var map = _worlds.Get1(0).Value;
            var freePlaces = map.Where(pair => pair.Value.Type == CellType.Empty).ToArray();
            var placeId = _random.Range(0, freePlaces.Length - 1);
            var coord = freePlaces.ElementAt(placeId).Key;
            
            var life = new Life
            {
                Name = "First Cell",
                Parent = "Creator"
            };

            var tile = _config.BlankTile;
            tile.color = new Color(
                _random.Range(0f, 1f),
                _random.Range(0f, 1f),
                _random.Range(0f, 1f));

            var speed = _random.Range(2, 3);
            var motion = new Motion
            {
                Weight = 1,
                Speed = speed
            };

            var lifeEntity = _world.NewEntity();
            lifeEntity.Get<Life>() = life;
            lifeEntity.Get<Coord>().Value = coord;
            lifeEntity.Get<LifeTile>().SetTile(tile);
            if (speed > 0) lifeEntity.Get<Motion>() = motion;
            lifeEntity.Get<Awake>();
            
            map[coord] = new Cell(CellType.Life, tile);
            var changesEntity = _world.NewEntity();
            changesEntity.Get<ChangedCells>().Value = new[] { coord };
        }
    }
}