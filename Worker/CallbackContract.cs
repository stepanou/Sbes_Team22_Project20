using System;
using System.Collections.Generic;
using System.Configuration;
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
           
            double greenZoneCost = Double.Parse(ConfigurationManager.AppSettings["greenZoneCost"]);
            double blueZoneCost = Double.Parse(ConfigurationManager.AppSettings["blueZoneCost"]);
            double redZoneCost = Double.Parse(ConfigurationManager.AppSettings["redZoneCost"]);
            float amount = float.Parse(consumption);

            int greenZoneUpperLimit = Int32.Parse(ConfigurationManager.AppSettings["greenZoneUpperLimit"]);
            int blueZoneUpperLimit = Int32.Parse(ConfigurationManager.AppSettings["blueZoneUpperLimit"]);

            if (amount <= greenZoneUpperLimit)
            {
                return (amount * greenZoneCost).ToString();
            }
            else if (amount > greenZoneUpperLimit && amount <= blueZoneUpperLimit)
            {
                return (greenZoneUpperLimit * greenZoneCost + (amount - greenZoneUpperLimit) * blueZoneCost).ToString();
            }
            else if (amount > blueZoneUpperLimit)
            {
                return (greenZoneUpperLimit * greenZoneCost + (blueZoneUpperLimit - greenZoneUpperLimit) * blueZoneCost + (amount - blueZoneUpperLimit) * redZoneCost).ToString();
            }
            else
            {
                return "0";
            }
        }
    }
}
