using Leopotam.Ecs;
using TMPro;

namespace Source
{
    public class ClockController : IEcsInitSystem, IEcsRunSystem
    {
        private readonly ISceneContext _scene = default;
        
        private readonly EcsFilter<SimulationTime> _globalTime = default;

        private TextMeshProUGUI ClockDate;
        private TextMeshProUGUI ClockTime;
        
        public void Init()
        {
            ClockDate = _scene.Canvas.TopToolbar.ClockDate;
            ClockTime = _scene.Canvas.TopToolbar.ClockTime;
        }

        public void Run()
        {
            var dateTime = _globalTime.Get1(0).Value;

            ClockDate.text = dateTime.DateToString();
            ClockTime.text = dateTime.TimeToString();
        }
    }
}