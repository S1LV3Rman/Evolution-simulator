using Leopotam.Ecs;
using TMPro;

namespace Source
{
    public class ClockController : IEcsRunSystem
    {
        private readonly ISceneContext _scene = default;
        
        private readonly EcsFilter<SimulationTime> _globalTime = default;

        public void Run()
        {
            var time = _globalTime.Get1(0).Value;

            _scene.Canvas.TopToolbar.ClockDate.text = time.DateToString();
            _scene.Canvas.TopToolbar.ClockTime.text = time.TimeToString();
        }
    }
}