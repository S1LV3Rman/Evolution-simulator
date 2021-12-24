using Leopotam.Ecs;
using UnityEngine;

namespace Source
{
    public class LifeReplicationRealization : IEcsRunSystem
    {
        private readonly IConfig _config = default;
        private readonly EcsWorld _world = default;
        private readonly ISceneContext _scene = default;
        private readonly IRandomService _random = default;

        private readonly EcsFilter<Life, Replicating, Alive> _replicatingLife = default;
        private readonly EcsFilter<SimulationMap> _maps = default;
        private readonly EcsFilter<Tick> _ticks = default;
        
        public void Run()
        {
            if (_ticks.IsEmpty()) return;
            if (_replicatingLife.IsEmpty()) return;

            var map = _maps.Get1(0).Value;
            var edge = _config.WorldSize;
            var ticksCount = _ticks.Get1(0).Count;
            foreach (var i in _replicatingLife)
            {
                var lifeEntity = _replicatingLife.GetEntity(i);

                var replication = lifeEntity.Get<Replication>();
                ref var replicating = ref _replicatingLife.Get2(i);
                var absorption = _config.WorldAbsorptionPower * replication.EnergyCost / replication.IncubationTime;
                for (var t = 0; t < ticksCount; ++t)
                {
                    if (replicating.Progress < replication.IncubationTime)
                    {
                        ref var life = ref _replicatingLife.Get1(i);
                        life.Energy -= absorption;
                        replicating.Progress++;
                    }
                    else
                    {
                        var coord = lifeEntity.Get<Coord>().Value;
                        var freeCoords = map.GetFreeNeighborCoords(coord, edge);
                        var stepId = _random.Range(0, freeCoords.Count - 1);
                        
                    }
                }
            }
        }
    }
}