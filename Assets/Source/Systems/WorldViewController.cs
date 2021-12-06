using Leopotam.Ecs;
using Unity.VisualScripting;

namespace Source
{
    public class WorldViewController : IEcsRunSystem
    {
        private readonly ISceneContext _scene = default;
        
        private readonly EcsFilter<WorldMap> _worlds = default;
        private readonly EcsFilter<ChangedCells> _changes = default;
        

        public void Run()
        {
            if (_changes.IsEmpty()) return;

            var map = _worlds.Get1(0).Value;
            foreach (var i in _changes)
            {
                var changedCells = _changes.Get1(i).Value;
                
                foreach (var cell in changedCells)
                    _scene.Map.Put(map[cell].Tile, cell.GetPos());
            }
            _changes.Clear();
        }
    }
}