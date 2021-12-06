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

            var map = _worlds.Get1(0).Value;
            foreach (var i in _movedLife)
            {
                var lifeEntity = _movedLife.GetEntity(i);

                var lifeCell = new Cell(CellType.Life, lifeEntity.Get<LifeTile>().Appearance);
                var trailCell = new Cell(CellType.Trail, lifeEntity.Get<LifeTile>().Trail);

                var path = _movedLife.Get2(i).Path;
                var changesEntity = _world.NewEntity();
                changesEntity.Get<ChangedCells>().Value = path.ToArray();
                
                var endCoord = path.Last();
                while (path.Count > 0)
                {
                    map[path[0]] = path.Count > 1 ? trailCell : lifeCell;
                    path.RemoveAt(0);
                }

                lifeEntity.Get<Coord>().Value = endCoord;
                lifeEntity.Del<Moved>();
            }
        }
    }
}