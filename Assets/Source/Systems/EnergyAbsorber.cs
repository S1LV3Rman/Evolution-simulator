using Leopotam.Ecs;

namespace Source
{
    public class EnergyAbsorber : IEcsRunSystem
    {
        private readonly IConfig _config = default;
        
        private readonly EcsFilter<Life, Alive> _aliveLife = default;
        private readonly EcsFilter<Tick> _ticks = default;
        
        public void Run()
        {
            if (_ticks.IsEmpty()) return;
            if (_aliveLife.IsEmpty()) return;

            var absorptionPower = _config.WorldAbsorptionPower * _config.PassiveAbsorptionPower;
            foreach (var i in _aliveLife)
            {
                ref var life = ref _aliveLife.Get1(i);

                var absorption = life.Form.Area * absorptionPower;
                life.Energy -= absorption;
            }
        }
    }
}