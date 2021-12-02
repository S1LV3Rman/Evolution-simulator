using Lean.Touch;
using Leopotam.Ecs;
using UnityEngine;

namespace Source.Systems
{
    public class TouchController : IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem
    {
        private readonly ISceneContext _scene = default;
        
        private Transform _camera;
        
        private EventSubscriber<LeanFinger> _fingerUpdate;

        private Vector2 startPosition;


        public void Init()
        {
            _camera = _scene.Camera.transform;
            
            _fingerUpdate = new EventSubscriber<LeanFinger>(OnFingerUpdate);
            
            LeanTouch.OnFingerUpdate += _fingerUpdate.Trigger;
        }
        
        public void Run()
        {
            _fingerUpdate.TryProcess();
        }

        private void OnFingerUpdate(LeanFinger finger)
        {
            if(finger.Set)
                _camera.Translate(-finger.GetWorldDelta(0f));
        }

        public void Destroy()
        {
            LeanTouch.OnFingerUpdate -= _fingerUpdate.Trigger;
        }
    }
}