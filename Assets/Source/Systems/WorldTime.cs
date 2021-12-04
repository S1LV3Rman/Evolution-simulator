using System;
using Leopotam.Ecs;
using TMPro;
using UnityEngine;

namespace Source.Systems
{
    public class WorldTime : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorld _world = default;
        private readonly IConfig _config = default;
        private readonly ISceneContext _scene = default;

        private readonly EcsFilter<Tick> _ticks = default;

        private float _timePassed;
        private WorldDateTime _globalTime;

        private TextMeshProUGUI ClockDate;
        private TextMeshProUGUI ClockTime;

        public void Init()
        {
            _globalTime = new WorldDateTime();
            _timePassed = 0f;

            ClockDate = _scene.Canvas.TopToolbar.ClockDate;
            ClockTime = _scene.Canvas.TopToolbar.ClockTime;
        }
        
        public void Run()
        {
            _timePassed += Time.deltaTime;

            _ticks.Clear();

            var secPerFrame = _globalTime.timeFormat.RealSecPerSec / _config.WorldTimeSpeed;
            if (_timePassed >= secPerFrame)
            {
                var framesCount = Mathf.FloorToInt(_timePassed / secPerFrame);
                _timePassed %= secPerFrame;

                var entity = _world.NewEntity();
                entity.Get<Tick>().Count = framesCount;
                
                _globalTime.AddSeconds(framesCount);

                ClockDate.text = _globalTime.DateToString();
                ClockTime.text = _globalTime.TimeToString();
            }
        }
    }
}