using System;
using System.Collections.Generic;

namespace Source
{
    public interface ITriggerSubscriber : IDisposable
    {
        public void TryProcess();
    }

    public static class TriggerSubscribersPool
    {
        public static List<ITriggerSubscriber> List = new List<ITriggerSubscriber>();
    }
}