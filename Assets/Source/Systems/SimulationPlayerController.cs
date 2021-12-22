using Leopotam.Ecs;

namespace Source
{
    public class SimulationPlayerController : IEcsInitSystem, IEcsRunSystem
    {
        private readonly ISceneContext _scene = default;
        
        private readonly EcsFilter<SimulationTime> _globalTime = default;
        private readonly EcsFilter<SpeedChange> _speedChange = default;
        
        public void Init()
        {
            var time = _globalTime.Get1(0);
            _scene.Canvas.SimulationPlayer.PlayButton.gameObject.SetActive(time.IsPaused);
            _scene.Canvas.SimulationPlayer.PauseButton.gameObject.SetActive(!time.IsPaused);
            _scene.Canvas.SimulationPlayer.SpeedField.text = time.Speed.ToString("G3");
        }

        public void Run()
        {
            var time = _globalTime.Get1(0);
            _scene.Canvas.SimulationPlayer.PlayButton.gameObject.SetActive(time.IsPaused);
            _scene.Canvas.SimulationPlayer.PauseButton.gameObject.SetActive(!time.IsPaused);

            if (_speedChange.IsEmpty()) return;
            
            var speed = _speedChange.Get1(0).Value;
            _scene.Canvas.SimulationPlayer.SpeedField.text = speed.ToString("G3");
        }
    }
}