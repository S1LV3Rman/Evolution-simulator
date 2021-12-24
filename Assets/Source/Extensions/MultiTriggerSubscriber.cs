using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

namespace Source
{
    public sealed class MultiTriggerSubscriber : ITriggerSubscriber
    {
        private int _triggersCount;
        private Action _action;
        private UnityEvent _triggerEvent;

        public void Trigger()
        {
            ++_triggersCount;
        }

        public void TryProcess()
        {
            while(_triggersCount > 0)
            {
                _action.Invoke();
                --_triggersCount;
            }
        }

        public MultiTriggerSubscriber(Action action)
        {
            _triggerEvent = null;
            _action = action;
            _triggersCount = 0;
            
            TriggerSubscribersPool.List.Add(this);
        }

        public MultiTriggerSubscriber(UnityEvent triggerEvent, Action action)
        {
            _triggerEvent = triggerEvent;
            _action = action;
            _triggersCount = 0;
                
            triggerEvent.AddListener(Trigger);
            
            TriggerSubscribersPool.List.Add(this);
        }

        public void Dispose()
        {
            _triggerEvent?.RemoveListener(Trigger);
        }
    }
    
    public sealed class MultiTriggerSubscriber<T> : ITriggerSubscriber
    {
        private List<T> _values;
        private Action<T> _action;
        private UnityEvent<T> _triggerEvent;

        public void Trigger(T value)
        {
            _values.Add(value);
        }

        public void TryProcess()
        {
            while(_values.Count > 0)
            {
                _action.Invoke(_values[0]);
                _values.RemoveAt(0);
            }
        }

        public MultiTriggerSubscriber(Action<T> action)
        {
            _triggerEvent = null;
            _action = action;
            _values = new List<T>();
            
            TriggerSubscribersPool.List.Add(this);
        }

        public MultiTriggerSubscriber(UnityEvent<T> triggerEvent, Action<T> action)
        {
            _triggerEvent = triggerEvent;
            _action = action;
            _values = new List<T>();
                
            triggerEvent.AddListener(Trigger);
            
            TriggerSubscribersPool.List.Add(this);
        }

        public void Dispose()
        {
            _triggerEvent?.RemoveListener(Trigger);
        }
    }
}