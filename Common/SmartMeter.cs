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
        private double consumption;

        [DataMember]
        public int Id { get => id; set => id = value; }
        [DataMember]
        public string User { get => user; set => user = value; }
        [DataMember]
        public double Consumption { get => consumption; set => consumption = value; }

        public SmartMeter(int id, string user, double consumption)
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
