using System.Linq;
using Leopotam.Ecs;

namespace Source
{
    public class SubscribersMaintainer : IEcsRunSystem, IEcsDestroySystem
    {
        public void Run()
        {
            foreach (var subscriber in TriggerSubscribersPool.List)
                subscriber.TryProcess();
        }

        public void Destroy()
        {
            foreach (var subscriber in TriggerSubscribersPool.List)
                subscriber.Dispose();
            TriggerSubscribersPool.List.Clear();
        }
    }
}