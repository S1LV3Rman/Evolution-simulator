using Leopotam.Ecs;
using UnityEngine;

namespace Source.Systems
{
    public class InputHandler : IEcsRunSystem
    {
        private readonly EcsWorld _world = default;

        private readonly EcsFilter<Mouse> _mice;

        public void Run()
        {
            if (_mice.IsEmpty()) return;
            
            var delta = Input.mouseScrollDelta.y;
            if (delta != 0)
                MouseWheelHandler(delta);
        }

        private void MouseWheelHandler(float delta)
        {
            var entity = _mice.GetEntity(0);
            entity.Get<Wheel>().Value = delta;
        }
    }
}