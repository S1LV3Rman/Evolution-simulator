using Leopotam.Ecs;
using UnityEngine;

namespace Source
{
    public class LifeMaintainer : IEcsRunSystem
    {
        private readonly IConfig _config = default;
        private readonly IRandomService _random = default;
        
        private readonly EcsFilter<Life, Awake> _awakenedLife = default;
        private readonly EcsFilter<Life, Alive> _aliveLife = default;
        
        private readonly EcsFilter<SimulationTime> _globalTime = default;

        public void Run()
        {
            if (!_awakenedLife.IsEmpty())
            {
                foreach (var i in _awakenedLife)
                {
                    Wake(ref _awakenedLife.GetEntity(i));
                }
            }
            
            if (!_aliveLife.IsEmpty())
            {
                foreach (var i in _aliveLife)
                {
                    var energy = _aliveLife.Get1(i).Energy / _aliveLife.Get1(i).Form.Volume;
                    if(energy <= 0)
                    {
                        Kill(ref _aliveLife.GetEntity(i));
                    }
                    else if (energy <= _config.DeathEnergyThreshold)
                    {
                        var requiredEnergy = _random.Range(0f, _config.DeathEnergyThreshold);
                        if (energy <= requiredEnergy)
                        {
                            Kill(ref _aliveLife.GetEntity(i));
                        }
                    }
                }
            }
        }

        private void Wake(ref EcsEntity entity)
        {
            entity.Del<Awake>();
            entity.Get<Alive>();

            var life = entity.Get<Life>();
            var speed = entity.Has<Movement>() ? entity.Get<Movement>().MaxSpeed : 0;
            Debug.Log($"[{_globalTime.Get1(0).Value.TimeToString()}] "
                      + $"{life.Name} {speed}x{life.Form.Size} woke up with {life.Energy} energy");
        }

        private void Kill(ref EcsEntity entity)
        {
            entity.Del<Alive>();
            entity.Get<Dead>();

            var life = entity.Get<Life>();
            var speed = entity.Has<Movement>() ? entity.Get<Movement>().MaxSpeed : 0;
            Debug.Log($"[{_globalTime.Get1(0).Value.TimeToString()}] "
                      + $"{life.Name} {speed}x{life.Form.Size} died with {life.Energy} energy");
        }
    }
}