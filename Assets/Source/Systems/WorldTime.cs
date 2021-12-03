using System;
using Leopotam.Ecs;
using UnityEngine;

namespace Source.Systems
{
    public class WorldTime : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorld _world = default;
        private readonly IConfig _config = default;

        private readonly EcsFilter<Tick> _ticks = default;

        private float _secPerFrame;
        private float _timePassed;
        private WorldDateTime _globalTime;

        public void Init()
        {
            _secPerFrame = _config.DefaultFrameTime / _config.WorldTimeSpeed;
            _timePassed = 0f;
            _globalTime = new WorldDateTime();
        }
        
        public void Run()
        {
            _timePassed += Time.deltaTime;

            _ticks.Clear();

            if (_timePassed >= _secPerFrame)
            {
                var framesCount = Mathf.FloorToInt(_timePassed / _secPerFrame);
                _timePassed %= _secPerFrame;

                var entity = _world.NewEntity();
                entity.Get<Tick>().Count = framesCount;
                _globalTime.AddSeconds(framesCount);
                
                Debug.Log($"World time: {_globalTime}");
            }
        }
    }
}