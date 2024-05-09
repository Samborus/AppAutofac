using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace AppAutoFac
{
    // We can keep the ISender interface if we want...
    public interface ISender
    {
        void Send(string dest, string content);
    }

    // But we'll introduce intermediate interfaces, even
    // if they're just "markers," so we can differentiate between
    // the sort of sending the strategies can perform.
    public interface IOrderSender : ISender { }
    public interface INotificationSender : ISender { }

    // We change the strategies so they implement the appropriate
    // interfaces based on what they are allowed to send.
    public class PostalServiceSender : IOrderSender
    {
        public void Send(string dest, string content)
        {
            Console.WriteLine("PostalServiceSender");
        }
    }
    public class EmailNotifier : INotificationSender
    {
        public void Send(string dest, string content)
        {
            Console.WriteLine("EmailNotifier");
        }
    }

    // Finally, we update the classes consuming the sending
    // strategies so they only allow the right kind of strategy
    // to be used.
    public class ShippingProcessor
    {
        public ShippingProcessor(IOrderSender shippingStrategy) {  }
    }

    public class CustomerNotifier
    {
        public CustomerNotifier(INotificationSender notificationStrategy) {  }
    }
}
