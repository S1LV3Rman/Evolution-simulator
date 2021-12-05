using Leopotam.Ecs;

namespace Source
{
    public class LifeViewCreator : IEcsRunSystem
    {
        private readonly IConfig _config = default;
        private readonly ISceneContext _scene = default;

        private readonly EcsFilter<Life, Awake> _awakenedLife = default;
        
        public void Run()
        {
            if (_awakenedLife.IsEmpty()) return;

            foreach (var i in _awakenedLife)
            {
                var entity = _awakenedLife.GetEntity(i);
                
                var coord = entity.Get<Coord>().Value;
                var point = GridMath.GetPos(coord);
                var tile = entity.Get<LifeTile>().Appearance;
                PaintMath.Put(_scene.Map, tile, point);
                
                entity.Del<Awake>();
                entity.Get<Alive>();
            }
        }
    }
}