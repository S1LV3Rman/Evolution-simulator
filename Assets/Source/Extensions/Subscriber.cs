using System;

namespace Source.Systems
{
    public sealed class Subscriber<T>
    {
        private bool _trigger;
        private T _value;
        private Action<T> _onTrigger;
        
        public T Value
        {
            get => _trigger ? _value : default;
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

        public Subscriber(Action<T> onTrigger)
        {
            _onTrigger = onTrigger;
            _trigger = false;
            Value = default;
        }

        public static implicit operator bool(Subscriber<T> subscriber) => subscriber._trigger;
    }
}