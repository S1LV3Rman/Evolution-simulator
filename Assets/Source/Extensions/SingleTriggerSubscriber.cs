using System;

namespace Source
{
    public sealed class SingleTriggerSubscriber
    {
        private bool _trigger;
        private Action _action;

        public void Trigger()
        {
            _trigger = true;
        }

        public void TryProcess()
        {
            if (_trigger)
            {
                _action.Invoke();
                _trigger = false;
            }
        }

        public SingleTriggerSubscriber(Action action)
        {
            _action = action;
            _trigger = false;
        }
    }
    
    public sealed class SingleTriggerSubscriber<T>
    {
        private bool _trigger;
        private T _value;
        private Action<T> _action;

        public void Trigger(T value)
        {
            _value = value;
            _trigger = true;
        }

        public void TryProcess()
        {
            if (_trigger)
            {
                _action.Invoke(_value);
                _trigger = false;
            }
        }

        public SingleTriggerSubscriber(Action<T> action)
        {
            _action = action;
            _trigger = false;
            _value = default;
        }
    }
}