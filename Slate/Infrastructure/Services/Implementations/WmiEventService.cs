using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using Slate.Infrastructure.WMI;

namespace Slate.Infrastructure.Services.Implementations
{
    public class WmiEventService : IWmiEventService, IDisposable
    {
        private Dictionary<string, ManagementEvent>? _eventRegistry = new();

        public void SubscribeTo(string eventProvider, Action<ManagementBaseObject> handler)
        {
            ThrowIfDisposed();

            if (!_eventRegistry!.ContainsKey(eventProvider))
            {
                _eventRegistry.Add(eventProvider, new ManagementEvent(eventProvider));
            }

            _eventRegistry[eventProvider].Subscribe(handler);
        }

        public void UnsubscribeFrom(string eventProvider, Action<ManagementBaseObject> handler)
        {
            ThrowIfDisposed();

            if (!_eventRegistry!.ContainsKey(eventProvider))
            {
                return;
            }

            _eventRegistry[eventProvider].Unsubscribe(handler);

            if (!_eventRegistry.Any())
            {
                _eventRegistry.Remove(eventProvider);
            }
        }

        public void Dispose()
        {
            if (_eventRegistry == null)
            {
                throw new InvalidOperationException("This service has already been disposed.");
            }

            foreach (var kvp in _eventRegistry)
            {
                kvp.Value.Dispose();
            }

            _eventRegistry.Clear();
            _eventRegistry = null;
        }

        private void ThrowIfDisposed()
        {
            if (_eventRegistry == null)
            {
                throw new InvalidOperationException("This service has been disposed.");
            }
        }
    }
}