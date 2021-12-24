using Lean.Touch;
using Leopotam.Ecs;
using UnityEngine;

namespace Source
{
    public class InputSender : IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem
    {
        private readonly EcsWorld _world = default;

        private readonly EcsFilter<InputEvent> _inputEvents;
        

        private MultiTriggerSubscriber<LeanFinger> _onFingerUpdate;


        public void Init()
        {
            _onFingerUpdate = new MultiTriggerSubscriber<LeanFinger>(FingerUpdateHandler);
            LeanTouch.OnFingerUpdate += _onFingerUpdate.Trigger;
        }
        
        public void Run()
        {
            _inputEvents.Clear();
            
            _onFingerUpdate.TryProcess();
        }

        private void FingerUpdateHandler(LeanFinger finger)
        {
            var entity = _world.NewEntity();
            entity.Get<InputEvent>();
            if(finger.Index == LeanTouch.HOVER_FINGER_INDEX)
            {
                entity.Get<Mouse>().finger = finger;
                var delta = Input.mouseScrollDelta.y;
                if (delta != 0)
                    entity.Get<Wheel>().Value = delta;
            }
            else
            {
                entity.Get<Finger>().Value = finger;
                entity.Get<Update>();
            }
        }

        public void Destroy()
        {
            LeanTouch.OnFingerUpdate -= _onFingerUpdate.Trigger;
        }
    }
}