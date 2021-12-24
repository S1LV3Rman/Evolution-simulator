using Lean.Touch;
using Leopotam.Ecs;

namespace Source
{
    public class ButtonsSender : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorld _world = default;
        private readonly SceneContext _scene = default;

        private readonly EcsFilter<UIEvent> _uiEvents;


        public void Init()
        {
            new SingleTriggerSubscriber(_scene.Canvas.SimulationPlayer.PlayButton.OnClick(), CreateUIEvent<PlayButton>);
            new SingleTriggerSubscriber(_scene.Canvas.SimulationPlayer.PauseButton.OnClick(), CreateUIEvent<PauseButton>);
            new MultiTriggerSubscriber(_scene.Canvas.SimulationPlayer.SpeedUpButton.OnClick(), CreateUIEvent<SpeedUpButton>);
            new MultiTriggerSubscriber(_scene.Canvas.SimulationPlayer.SpeedDownButton.OnClick(), CreateUIEvent<SpeedDownButton>);
            new SingleTriggerSubscriber<string>(_scene.Canvas.SimulationPlayer.SpeedField.onEndEdit, CreateSpeedChangeEvent);
        }
        
        public void Run()
        {
            _uiEvents.Clear();
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
    }
}