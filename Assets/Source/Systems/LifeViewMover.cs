using System.Linq;
using Leopotam.Ecs;

namespace Source
{
    public class LifeViewMover : IEcsRunSystem
    {
        private readonly IConfig _config = default;
        private readonly ISceneContext _scene = default;

        private readonly EcsFilter<Life, Moved> _movedLife = default;
        
        public void Run()
        {
            if (_movedLife.IsEmpty()) return;

            foreach (var i in _movedLife)
            {
                var entity = _movedLife.GetEntity(i);
                
                var oldCoord = entity.Get<Coord>().Value;
                var oldPoint = GridMath.GetPos(oldCoord);

                var trail = entity.Get<LifeTile>().Trail;
                PaintMath.Put(_scene.Map, trail, oldPoint);
            }

            foreach (var i in _movedLife)
            {
                var entity = _movedLife.GetEntity(i);

                var path = entity.Get<Moved>().Path;
                var endCoord = path.Last();
                while (path.Count > 0)
                {
                    var point = GridMath.GetPos(path[0]);
                    
                    var tile = path.Count > 1 ?
                        entity.Get<LifeTile>().Trail :
                        entity.Get<LifeTile>().Appearance;
                    
                    PaintMath.Put(_scene.Map, tile, point);
                    
                    path.RemoveAt(0);
                }

                entity.Get<Coord>().Value = endCoord;
                entity.Del<Moved>();
            }
        }
    }
}