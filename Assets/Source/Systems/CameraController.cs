using DG.Tweening;
using Lean.Touch;
using Leopotam.Ecs;
using UnityEngine;

namespace Source
{
    public class CameraController : IEcsInitSystem, IEcsRunSystem
    {
        private readonly ISceneContext _scene = default;
        private readonly IConfig _config = default;

        private readonly EcsFilter<Finger, Update> _updateFingers;
        private readonly EcsFilter<Mouse, Wheel> _mouseWheels;
        
        private float _targetSize;
        private Tweener _resizeTweener;
        private Tweener _translateTweener;


        public void Init()
        {
            _scene.Camera.orthographicSize = _config.CameraMaxSize;
            _targetSize = _scene.Camera.orthographicSize;
        }

        public void Run()
        {
            if (!_updateFingers.IsEmpty())
                foreach (var i in _updateFingers)
                {
                    var finger = _updateFingers.Get1(i).Value;
                    if(finger.StartedOverGui || finger.Index == LeanTouch.MOUSE_FINGER_INDEX) continue;

                    _resizeTweener?.Kill();
                    _translateTweener?.Kill();
                    _targetSize = _scene.Camera.orthographicSize;
                    _scene.Camera.transform.Translate(-finger.GetWorldDelta(0f));
                }
            else if (!_mouseWheels.IsEmpty())
                foreach (var i in _mouseWheels)
                {
                    var mouse = _mouseWheels.Get1(0).finger;
                    if(mouse.IsOverGui) continue;
                    
                    var delta = _mouseWheels.Get2(i).Value;
                    var targetSize = Mathf.Clamp(_targetSize - _targetSize * delta * _config.CameraSizeScaler,
                        _config.CameraMinSize, _config.CameraMaxSize);
                    if(targetSize < _targetSize || targetSize > _targetSize)
                    {
                        _targetSize = targetSize;
                        _resizeTweener?.Kill();
                        _resizeTweener = _scene.Camera.DOOrthoSize(_targetSize, _config.CameraResizeTime);
                    }

                    var mousePos = mouse.GetWorldPosition(0f);
                    var cameraPos = _scene.Camera.transform.position;
                    var sizeMultiplier = _targetSize / _scene.Camera.orthographicSize - 1f;
                    var translate = (cameraPos - mousePos) * sizeMultiplier;
                    var targetPos = cameraPos + translate;
                    _translateTweener?.Kill();
                    _translateTweener = _scene.Camera.transform.DOMove(targetPos, 
                        _config.CameraResizeTime - _resizeTweener.Elapsed());
                }
        }
    }
}