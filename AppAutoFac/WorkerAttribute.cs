using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAutoFac
{
    //[System.ComponentModel.Composition.MetadataAttribute]
    public class WorkerAttribute : Attribute
    {
        public string Invoker { get; set; }

        public WorkerAttribute(string invoker)
        {
            this.Invoker = invoker;
        }
    }
}
