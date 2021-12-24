using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Source
{
    public sealed class SingleTriggerSubscriber : ITriggerSubscriber
    {
        private bool _trigger;
        private Action _action;
        private UnityEvent _triggerEvent;

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
            _triggerEvent = null;
            _action = action;
            _trigger = false;
            
            TriggerSubscribersPool.List.Add(this);
        }

        public SingleTriggerSubscriber(UnityEvent triggerEvent, Action action)
        {
            _triggerEvent = triggerEvent;
            _action = action;
            _trigger = false;
                
            triggerEvent.AddListener(Trigger);
            
            TriggerSubscribersPool.List.Add(this);
        }

        public void Dispose()
        {
            _triggerEvent?.RemoveListener(Trigger);
        }
    }
    
    public sealed class SingleTriggerSubscriber<T> : ITriggerSubscriber
    {
        private bool _trigger;
        private T _value;
        private Action<T> _action;
        private UnityEvent<T> _triggerEvent;

        private void Trigger(T value)
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
            _triggerEvent = null;
            _action = action;
            _trigger = false;
            _value = default;
            
            TriggerSubscribersPool.List.Add(this);
        }

        public SingleTriggerSubscriber(UnityEvent<T> triggerEvent, Action<T> action)
        {
            _triggerEvent = triggerEvent;
            _action = action;
            _trigger = false;
            _value = default;
            
            triggerEvent.AddListener(Trigger);
            
            TriggerSubscribersPool.List.Add(this);
        }

        public void Dispose()
        {
            _triggerEvent?.RemoveListener(Trigger);
        }
    }
}