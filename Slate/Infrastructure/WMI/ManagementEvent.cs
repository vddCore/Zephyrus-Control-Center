using System;
using System.Collections.Generic;
using System.Management;

namespace Slate.Infrastructure.WMI
{
    public class ManagementEvent : IDisposable
    {
        private ManagementEventWatcher Watcher { get; }
        private HashSet<Action<ManagementBaseObject>> Subscribers { get; } = new();

        public ManagementEvent(string eventProvider)
        {
            Watcher = new ManagementEventWatcher(
                "root\\WMI",
                $"SELECT * FROM {eventProvider}"
            );
            
            Watcher.EventArrived += Watcher_EventArrived;
            Watcher.Start();
        }

        public void Subscribe(Action<ManagementBaseObject> subscriber)
        {
            Subscribers.Add(subscriber);
        }

        public void Unsubscribe(Action<ManagementBaseObject> subscriber)
        {
            Subscribers.Remove(subscriber);
        }

        private void Watcher_EventArrived(object sender, EventArrivedEventArgs e)
        {
            var invocationList = new List<Action<ManagementBaseObject>>(Subscribers);

            foreach (var subscriber in invocationList)
                subscriber.Invoke(e.NewEvent);
        }

        public void Dispose()
        {
            Watcher.Stop();
            Watcher.Dispose();
        }
    }
}