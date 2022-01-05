using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Worker
{
    public class CallbackContract : ICallBackContract
    {
        public string DoWork(string consumption)
        {
           return "Radi posao " + consumption +  ".";
        }
    }
}
