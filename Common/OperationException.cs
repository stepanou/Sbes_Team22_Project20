using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [DataContract]
    public class OperationException
    {
        string _message;

        [DataMember]
        public string Message { get => _message; set => _message = value; }

        public OperationException(string message)
        {

            Message = message;
        }
    }
}
