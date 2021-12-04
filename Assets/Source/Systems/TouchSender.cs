using Lean.Touch;
using Leopotam.Ecs;
using UnityEngine;

namespace Source.Systems
{
    public class TouchHandler : IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem
    {
        private readonly EcsWorld _world = default;

        private readonly EcsFilter<Mouse> _mice;
        private readonly EcsFilter<Finger, Update> _updateFingers;

        private Subscriber<LeanFinger> _onFingerUpdate;


        public void Init()
        {
            _onFingerUpdate = new Subscriber<LeanFinger>(FingerUpdateHandler);
            
            LeanTouch.OnFingerUpdate += _onFingerUpdate.Trigger;
        }
        
        public void Run()
        {
            _mice.Clear();
            _updateFingers.Clear();
            
            _onFingerUpdate.TryProcess();
        }

        private void FingerUpdateHandler(LeanFinger finger)
        {
            var entity = _world.NewEntity();
            if(finger.Index == LeanTouch.HOVER_FINGER_INDEX)
            {
                entity.Get<Mouse>().finger = finger;
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