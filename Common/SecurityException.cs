using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Common
{
    [DataContract]
    public class SecurityException
    {
        string _message;

        [DataMember]
        public string Message { get => _message; set => _message = value; }

        public SecurityException(string message)
        {
            Message = message;
        }
    }
}
