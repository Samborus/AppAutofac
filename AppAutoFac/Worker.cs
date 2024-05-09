using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAutoFac
{
    public interface IWorker
    {
        void Go();
    }

    [Worker("c")]
    public class Worker : IWorker
    {
        private ISender _sender;
        public Worker(ISender sender) {
            _sender = sender;
        }
        public void Go()
        {
            _sender.Send("a", "a");
        }
    }

    [Worker("d")]
    public class NewWorker : Worker
    {
        public NewWorker(ISender sender) : base(sender)
        {
        }
    }
}
