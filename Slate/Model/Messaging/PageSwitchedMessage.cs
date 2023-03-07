using Avalonia.Controls;
using Glitonea.Mvvm.Messaging;

namespace Slate.Model.Messaging
{
    public class PageSwitchedMessage : Message
    {
        public UserControl Page { get; }

        public PageSwitchedMessage(UserControl page)
        {
            Page = page;
        }
    }
}