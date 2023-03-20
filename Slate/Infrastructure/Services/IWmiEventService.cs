using System;
using System.Management;
using Glitonea.Mvvm;

namespace Slate.Infrastructure.Services
{
    public interface IWmiEventService : IService
    {
        void SubscribeTo(string eventProvider, Action<ManagementBaseObject> handler);
        void UnsubscribeFrom(string eventProvider, Action<ManagementBaseObject> handler);
    }
}