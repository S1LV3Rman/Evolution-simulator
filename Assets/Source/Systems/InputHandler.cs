using Leopotam.Ecs;
using UnityEngine;

namespace Source.Systems
{
    public class InputHandler : IEcsRunSystem
    {
        private readonly EcsWorld _world = default;

        private readonly EcsFilter<MouseWheel> _mouseWheels;

        public void Run()
        {
            _mouseWheels.Clear();

            var delta = Input.mouseScrollDelta.y;
            if (delta != 0)
                MouseWheelHandler(delta);
        }

        private void MouseWheelHandler(float delta)
        {
            var entity = _world.NewEntity();
            entity.Get<MouseWheel>().Value = delta;
        }
    }
}