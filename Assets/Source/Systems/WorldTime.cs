using Leopotam.Ecs;
using UnityEngine;

namespace Source
{
    public class WorldTime : IEcsInitSystem, IEcsRunSystem
    {
        private readonly IConfig _config = default;
        private readonly EcsWorld _world = default;

        private readonly EcsFilter<Tick> _ticks = default;
        private readonly EcsFilter<SimulationTime> _globalTime = default;

        private float _timePassed;

        public void Init()
        {
            var entity = _world.NewEntity();
            ref var time = ref entity.Get<SimulationTime>();
            time.Value = new WorldDateTime();
            time.Speed = _config.WorldStartSpeed;
            
            _timePassed = 0f;
        }
        
        public void Run()
        {
            _timePassed += Time.deltaTime;

            _ticks.Clear();

            ref var time = ref _globalTime.Get1(0);
            var secPerFrame = time.Value.timeFormat.RealSecPerSec / time.Speed;
            if (_timePassed >= secPerFrame)
            {
                var framesCount = Mathf.FloorToInt(_timePassed / secPerFrame);
                _timePassed %= secPerFrame;

                var entity = _world.NewEntity();
                entity.Get<Tick>().Count = framesCount;
                
                time.Value.AddSeconds(framesCount);
            }
        }
    }
}