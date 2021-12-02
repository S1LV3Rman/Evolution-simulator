using System;

namespace Source.Systems
{
    public sealed class EventSubscriber<T> where T : class
    {
        private bool _trigger;
        private T _value;
        private Action<T> _onTrigger;
        
        public T Value
        {
            get => _trigger ? _value : null;
            private set => _value = value;
        }

        public void Trigger(T value)
        {
            _value = value;
            _trigger = true;
        }

        public void TryProcess()
        {
            if (_trigger)
            {
                _onTrigger.Invoke(_value);
                _trigger = false;
            }
        }

        public EventSubscriber(Action<T> onTrigger)
        {
            _onTrigger = onTrigger;
            _trigger = false;
            Value = null;
        }

        public static implicit operator bool(EventSubscriber<T> subscriber) => subscriber._trigger;
    }
}