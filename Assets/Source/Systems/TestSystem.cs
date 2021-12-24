using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Source
{
    public class TestSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorld _world = default;
        private readonly IConfig _config = default;
        private readonly ISceneContext _scene = default;
        private readonly IRandomService _random = default;

        private readonly EcsFilter<Life, Movement, Alive> _movableLife = default;
        private readonly EcsFilter<Tick> _ticks = default;

        public void Init()
        {
        }

        public void Run()
        {
        }
    }
}