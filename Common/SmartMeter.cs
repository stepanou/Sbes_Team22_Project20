using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [DataContract]
    public class SmartMeter
    {
        private int id;
        private string user;
        private float consumption;

        [DataMember]
        public int Id { get => id; set => id = value; }
        [DataMember]
        public string User { get => user; set => user = value; }
        [DataMember]
        public float Consumption { get => consumption; set => consumption = value; }

        public SmartMeter(int id, string user, float consumption)
        {
            Id = id;
            User = user;
            Consumption = consumption;
        }

        public SmartMeter()
        {
        }
    }
}
