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
        
        private readonly EcsFilter<PlayButton> _play = default;
        private readonly EcsFilter<PauseButton> _pause = default;
        private readonly EcsFilter<SpeedDownButton> _speedDown = default;
        private readonly EcsFilter<SpeedUpButton> _speedUp = default;
        private readonly EcsFilter<SpeedField> _speedField = default;
        
        private readonly EcsFilter<SpeedChange> _speedChange = default;

        private float _timePassed;

        public void Init()
        {
            var entity = _world.NewEntity();
            ref var time = ref entity.Get<SimulationTime>();
            time.Value = new WorldDateTime();
            time.Speed = _config.WorldStartSpeed;
            time.IsPaused = time.Speed <= 0f;
            
            _timePassed = 0f;
        }
        
        public void Run()
        {
            _ticks.Clear();
            
            HandleSpeedChanges();
            
            ref var time = ref _globalTime.Get1(0);

            if (time.IsPaused && !_play.IsEmpty())
                time.IsPaused = false;

            if (!time.IsPaused && !_pause.IsEmpty())
                time.IsPaused = true;
            
            if (time.IsPaused) return;
            
            _timePassed += Time.deltaTime;

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

        private void HandleSpeedChanges()
        {
            _speedChange.Clear();
            
            if (_speedField.IsEmpty() && _speedUp.IsEmpty() && _speedDown.IsEmpty()) return;
            
            ref var time = ref _globalTime.Get1(0);

            var speed = time.Speed;
            if (!_speedField.IsEmpty())
                speed = _speedField.Get1(0).Value;
            
            if (!_speedUp.IsEmpty())
                foreach (var i in _speedUp)
                    speed *= 2;
            
            if (!_speedDown.IsEmpty())
                foreach (var i in _speedDown)
                    speed /= 2;

            time.Speed = speed;
            
            var entity = _world.NewEntity();
            entity.Get<SpeedChange>().Value = speed;
        }
    }
}