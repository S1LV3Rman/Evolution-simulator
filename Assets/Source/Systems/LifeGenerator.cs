using Leopotam.Ecs;
using UnityEngine;

namespace Source.Systems
{
    public class LifeGenerator : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorld _world = default;
        private readonly IRandomService _random = default;
        private readonly IConfig _config = default;

        private int _radius;
        
        
        public void Init()
        {
            _radius = _config.WorldRadius;
            
            CreateCell();
            CreateCell();
            CreateCell();
            CreateCell();
            CreateCell();
        }
        
        public void Run()
        {
            
        }
        
        private void CreateCell()
        {
            var top = _random.Range(-_radius, _radius);
            var bot = top > 0 ? 
                _random.Range(-_radius + top, _radius) : 
                _random.Range(-_radius, _radius + top);

            var coord = new MapCoord(top, bot);
            
            var life = new Life
            {
                Name = "First Cell",
                Parent = "Creator"
            };

            var speed = _random.Range(0, 2);
            var motion = new Motion
            {
                Weight = 1,
                Speed = speed
            };

            var entity = _world.NewEntity();
            entity.Get<Life>() = life;
            entity.Get<Coord>().Value = coord;
            if (speed > 0) entity.Get<Motion>() = motion;
            entity.Get<Awake>();
        }
    }
}