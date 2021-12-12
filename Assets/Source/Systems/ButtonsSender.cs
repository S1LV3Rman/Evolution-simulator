using Lean.Touch;
using Leopotam.Ecs;

namespace Source
{
    public class ButtonsSender : IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem
    {
        private readonly EcsWorld _world = default;
        private readonly SceneContext _scene = default;

        private readonly EcsFilter<UIEvent> _uiEvents;

        private SingleTriggerSubscriber _onPlayPauseButton;


        public void Init()
        {
            _onPlayPauseButton = new SingleTriggerSubscriber(PlayPauseButtonHandler);
            _scene.Canvas.SimulationPlayer.PlayPauseButton.OnClick().AddListener(_onPlayPauseButton.Trigger);
        }
        
        public void Run()
        {
            _uiEvents.Clear();
            
            _onPlayPauseButton.TryProcess();
        }

        private void PlayPauseButtonHandler()
        {
            var entity = _world.NewEntity();
            entity.Get<UIEvent>();
            entity.Get<PlayPauseButton>();
        }

        public void Destroy()
        {
            _scene.Canvas.SimulationPlayer.PlayPauseButton.OnClick().RemoveListener(_onPlayPauseButton.Trigger);
        }
    }
}