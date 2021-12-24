using Leopotam.Ecs;

namespace Source
{
    public class LifeReplicationPrediction : IEcsRunSystem
    {
        private readonly IConfig _config = default;
        private readonly IRandomService _random = default;

        private readonly EcsFilter<Life, Replication, Alive>.Exclude<Replicating> _replicableLife = default;
        private readonly EcsFilter<Tick> _ticks = default;
        
        public void Run()
        {
            if (_ticks.IsEmpty()) return;
            if (_replicableLife.IsEmpty()) return;
            
            foreach (var i in _replicableLife)
            {
                var replication = _replicableLife.Get2(i);
                var life = _replicableLife.Get1(i);
                if (life.Energy > replication.EnergyCost * 2.5f)
                {
                    var entity = _replicableLife.GetEntity(i);
                    entity.Get<Replicating>().Progress = 0f;
                }
            }
        }
    }
}