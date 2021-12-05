using Leopotam.Ecs;
using UnityEngine;

namespace Source
{
    public class WorldTime : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorld _world = default;
        private readonly IConfig _config = default;

        private readonly EcsFilter<Tick> _ticks = default;
        private readonly EcsFilter<SimulationTime> _globalTime = default;

        private float _timePassed;

        public void Init()
        {
            var entity = _world.NewEntity();
            entity.Get<SimulationTime>().Value = new WorldDateTime();
            
            _timePassed = 0f;
        }
        
        public void Run()
        {
            _timePassed += Time.deltaTime;

            _ticks.Clear();

            ref var dateTime = ref _globalTime.Get1(0).Value;
            var secPerFrame = dateTime.timeFormat.RealSecPerSec / _config.WorldTimeSpeed;
            if (_timePassed >= secPerFrame)
            {
                var framesCount = Mathf.FloorToInt(_timePassed / secPerFrame);
                _timePassed %= secPerFrame;

                var entity = _world.NewEntity();
                entity.Get<Tick>().Count = framesCount;
                
                dateTime.AddSeconds(framesCount);
            }
        }
    }
}