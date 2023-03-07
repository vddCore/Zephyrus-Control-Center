using Glitonea.Mvvm.Messaging;

namespace Slate.Model.Messaging
{
    public class GlobalTickMessage : Message
    {
        public ulong TotalTicksElapsed { get; }

        public GlobalTickMessage(ulong totalTicksElapsed)
        {
            TotalTicksElapsed = totalTicksElapsed;
        }
    }
}