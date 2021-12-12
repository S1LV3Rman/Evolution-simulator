using System;
using System.Collections.Generic;
using System.Linq;

namespace Source
{
    public sealed class MultiTriggerSubscriber
    {
        private int _triggersCount;
        private Action _action;

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
            _action = action;
            _triggersCount = 0;
        }
    }
    
    public sealed class MultiTriggerSubscriber<T>
    {
        private List<T> _values;
        private Action<T> _action;

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
            _action = action;
            _values = new List<T>();
        }
    }
}