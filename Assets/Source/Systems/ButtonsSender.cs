using Lean.Touch;
using Leopotam.Ecs;

namespace Source
{
    public class ButtonsSender : IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem
    {
        private readonly EcsWorld _world = default;
        private readonly SceneContext _scene = default;

        private readonly EcsFilter<UIEvent> _uiEvents;

        private SingleTriggerSubscriber _onPlayButton;
        private SingleTriggerSubscriber _onPauseButton;
        private MultiTriggerSubscriber _onSpeedUpButton;
        private MultiTriggerSubscriber _onSpeedDownButton;
        private SingleTriggerSubscriber<string> _onSpeedField;


        public void Init()
        {
            _onPlayButton = new SingleTriggerSubscriber(CreateUIEvent<PlayButton>);
            _scene.Canvas.SimulationPlayer.PlayButton.OnClick().AddListener(_onPlayButton.Trigger);
            
            _onPauseButton = new SingleTriggerSubscriber(CreateUIEvent<PauseButton>);
            _scene.Canvas.SimulationPlayer.PauseButton.OnClick().AddListener(_onPauseButton.Trigger);
            
            _onSpeedUpButton = new MultiTriggerSubscriber(CreateUIEvent<SpeedUpButton>);
            _scene.Canvas.SimulationPlayer.SpeedUpButton.OnClick().AddListener(_onSpeedUpButton.Trigger);
            
            _onSpeedDownButton = new MultiTriggerSubscriber(CreateUIEvent<SpeedDownButton>);
            _scene.Canvas.SimulationPlayer.SpeedDownButton.OnClick().AddListener(_onSpeedDownButton.Trigger);
            
            _onSpeedField = new SingleTriggerSubscriber<string>(CreateSpeedChangeEvent);
            _scene.Canvas.SimulationPlayer.SpeedField.onEndEdit.AddListener(_onSpeedField.Trigger);
        }
        
        public void Run()
        {
            _uiEvents.Clear();
            
            _onPlayButton.TryProcess();
            _onPauseButton.TryProcess();
            _onSpeedUpButton.TryProcess();
            _onSpeedDownButton.TryProcess();
            _onSpeedField.TryProcess();
        }

        private void CreateUIEvent<T>() where T: struct 
        {
            var entity = _world.NewEntity();
            entity.Get<UIEvent>();
            entity.Get<T>();
        }

        private void CreateSpeedChangeEvent(string speedText) 
        {
            if(float.TryParse(speedText, out var speedValue))
            {
                var entity = _world.NewEntity();
                entity.Get<UIEvent>();
                entity.Get<SpeedField>().Value = speedValue;
            }
        }

        public void Destroy()
        {
            _scene.Canvas.SimulationPlayer.PlayButton.OnClick().RemoveListener(_onPlayButton.Trigger);
            _scene.Canvas.SimulationPlayer.PauseButton.OnClick().RemoveListener(_onPauseButton.Trigger);
            _scene.Canvas.SimulationPlayer.SpeedUpButton.OnClick().RemoveListener(_onSpeedUpButton.Trigger);
            _scene.Canvas.SimulationPlayer.SpeedDownButton.OnClick().RemoveListener(_onSpeedDownButton.Trigger);
            _scene.Canvas.SimulationPlayer.SpeedField.onEndEdit.RemoveListener(_onSpeedField.Trigger);
        }
    }
}