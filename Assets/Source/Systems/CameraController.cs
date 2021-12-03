using DG.Tweening;
using Leopotam.Ecs;
using UnityEngine;

namespace Source.Systems
{
    public class CameraController : IEcsInitSystem, IEcsRunSystem
    {
        private readonly ISceneContext _scene = default;
        private readonly IConfig _config = default;

        private readonly EcsFilter<Finger, Update> _updateFingers;
        private readonly EcsFilter<MouseWheel> _mouseWheels;
        
        private Camera _camera;

        private float _targetScale;
        private Tweener _resizeTweener;


        public void Init()
        {
            _camera = _scene.Camera;
            _targetScale = _camera.orthographicSize;
        }

        public void Run()
        {
            if (!_updateFingers.IsEmpty())
                foreach (var i in _updateFingers)
                {
                    var finger = _updateFingers.Get1(i).Value;
                    _camera.transform.Translate(-finger.GetWorldDelta(0f));
                }
            
            if (!_mouseWheels.IsEmpty())
                foreach (var i in _mouseWheels)
                {
                    var delta = _mouseWheels.Get1(i).Value;
                    _targetScale = Mathf.Clamp(_targetScale - _targetScale * delta * _config.CameraSizeScaler,
                        _config.CameraMinSize, _config.CameraMaxSize);
                    _resizeTweener?.Kill();
                    _resizeTweener = _camera.DOOrthoSize(_targetScale, _config.CameraResizeTime);
                }
        }
    }
}