using System.Linq;
using Leopotam.Ecs;

namespace Source
{
    public class LifeMotionRealization : IEcsRunSystem
    {
        private readonly IConfig _config = default;
        private readonly EcsWorld _world = default;
        private readonly ISceneContext _scene = default;

        private readonly EcsFilter<Life, Moved> _movedLife = default;
        private readonly EcsFilter<WorldMap> _worlds = default;
        
        public void Run()
        {
            if (_movedLife.IsEmpty()) return;

            var absorptionPower = _config.WorldAbsorptionPower * _config.MotionAbsorptionPower;
            var map = _worlds.Get1(0).Value;
            foreach (var i in _movedLife)
            {
                var lifeEntity = _movedLife.GetEntity(i);

                var lifeCell = new Cell(CellType.Life, lifeEntity.Get<LifeTile>().Appearance);
                var trailCell = new Cell(CellType.Trail, lifeEntity.Get<LifeTile>().Trail);

                var path = _movedLife.Get2(i).Path;
                var changesEntity = _world.NewEntity();
                changesEntity.Get<ChangedCells>().Value = path.ToArray();

                ref var life = ref lifeEntity.Get<Life>();
                ref var motion = ref lifeEntity.Get<Motion>();
                var stepAbsorption = absorptionPower * motion.EnergySpentPerMass * life.Form.Volume / path.Count;
                var lastCoord = lifeEntity.Get<Coord>().Value;
                while (path.Count > 0 && life.Energy > 0f)
                {
                    map[lastCoord] = trailCell;
                    lastCoord = path.First();
                    path.RemoveAt(0);

                    life.Energy -= stepAbsorption;
                }
                map[lastCoord] = lifeCell;
                motion.EnergySpentPerMass = 0;

                lifeEntity.Get<Coord>().Value = lastCoord;
                lifeEntity.Del<Moved>();
            }
        }
    }
}