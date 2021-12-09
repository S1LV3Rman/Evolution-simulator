using System.Collections.Generic;
using System.Linq;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Source
{
    public class LifeGenerator : IEcsInitSystem
    {
        private readonly IConfig _config = default;
        private readonly EcsWorld _world = default;
        private readonly IRandomService _random = default;
        
        private readonly EcsFilter<WorldMap> _worlds = default;

        private int lifeCount;

        public void Init()
        {
            for(var i = 0; i < _config.TestVariables[1]; ++i)
                CreateCell();
        }

        private void CreateCell()
        {
            lifeCount++;
            
            var map = _worlds.Get1(0).Value;
            var freePlaces = map.Where(pair => pair.Value.Type == CellType.Empty).ToArray();
            var placeId = _random.Range(0, freePlaces.Length - 1);
            var coord = freePlaces.ElementAt(placeId).Key;

            //var speed = _random.Range(2, 3);
            var maxSpeed = 1;
            
            var form = CubeForm.FromVolume(_random.Around(10f, 2f));
            //var power = _random.Around(_config.TestVariables[2], _config.TestVariables[3]) * form.Volume;
            var power = _config.TestVariables[2] * form.Volume;
            
            var life = new Life
            {
                Name = lifeCount % 2 == 0 ? "Red Cell" : "Blue Cell",
                Parent = "Creator",
                Energy = power, 
                Form = form
            };

            var tile = _config.BlankTile;
            tile.color = lifeCount % 2 == 0 ? Color.red : Color.blue;
            // tile.color = new Color(
            //     _random.Range(0f, 1f),
            //     _random.Range(0f, 1f),
            //     _random.Range(0f, 1f));

            var motion = new Motion
            {
                Weight = 1,
                MaxSpeed = maxSpeed,
                Speed = 0,
                Acceleration = 1,
                EnergySpentPerMass = 0
            };

            var lifeEntity = _world.NewEntity();
            lifeEntity.Get<Life>() = life;
            lifeEntity.Get<Coord>().Value = coord;
            lifeEntity.Get<LifeTile>().SetTile(tile);
            if (maxSpeed > 0) lifeEntity.Get<Motion>() = motion;
            lifeEntity.Get<Awake>();
            
            map[coord] = new Cell(CellType.Life, tile);
            var changesEntity = _world.NewEntity();
            changesEntity.Get<ChangedCells>().Value = new[] { coord };
        }
    }
}