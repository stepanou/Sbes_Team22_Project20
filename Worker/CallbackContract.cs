using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Common;

namespace Worker
{
    public class CallbackContract : ICallBackContract
    {
        public string DoWork(string consumption)
        {
           
            Console.WriteLine($"Calculating consumption :");
            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine(i);
                Thread.Sleep(1000);
            }


            double amount = 0;
            double greenZoneCost = 0;
            double blueZoneCost = 0;
            double redZoneCost = 0;
            

            var cultureInfo = CultureInfo.InvariantCulture;
            // if the first regex matches, the number string is in us culture
            if (Regex.IsMatch(consumption, @"^(:?[\d,]+\.)*\d+$"))
            {
                cultureInfo = new CultureInfo("en-US");
            }
            // if the second regex matches, the number string is in de culture
            else if (Regex.IsMatch(consumption, @"^(:?[\d.]+,)*\d+$"))
            {
                cultureInfo = new CultureInfo("de-DE");
            }
            NumberStyles styles = NumberStyles.Number;
            bool isDouble = double.TryParse(consumption, styles, cultureInfo, out amount);

            string greenZoneCostStr = ConfigurationManager.AppSettings["greenZoneCost"];
            string blueZoneCostStr = ConfigurationManager.AppSettings["blueZoneCost"];
            string redZoneCostStr = ConfigurationManager.AppSettings["redZoneCost"];

            if (cultureInfo.Equals(new CultureInfo("en-US")))
            {
                if (greenZoneCostStr.Contains(","))
                {
                    greenZoneCostStr = greenZoneCostStr.Replace(",", ".");
                }
                if (blueZoneCostStr.Contains(","))
                {
                    blueZoneCostStr = blueZoneCostStr.Replace(",", ".");
                }
                if (redZoneCostStr.Contains(","))
                {
                    redZoneCostStr = redZoneCostStr.Replace(",", ".");
                }
            }
            else if (cultureInfo.Equals(new CultureInfo("de-DE")))
            {
                if (greenZoneCostStr.Contains("."))
                {
                    greenZoneCostStr = greenZoneCostStr.Replace(".", ",");
                }
                if (blueZoneCostStr.Contains("."))
                {
                    blueZoneCostStr = blueZoneCostStr.Replace(".", ",");
                }
                if (redZoneCostStr.Contains("."))
                {
                    redZoneCostStr = redZoneCostStr.Replace(".", ",");
                }
            }

            isDouble = double.TryParse(greenZoneCostStr, styles, cultureInfo, out greenZoneCost);
            isDouble = double.TryParse(blueZoneCostStr, styles, cultureInfo, out blueZoneCost);
            isDouble = double.TryParse(redZoneCostStr, styles, cultureInfo, out redZoneCost);

            int greenZoneUpperLimit = Int32.Parse(ConfigurationManager.AppSettings["greenZoneUpperLimit"]);
            int blueZoneUpperLimit = Int32.Parse(ConfigurationManager.AppSettings["blueZoneUpperLimit"]);


            if (amount <= greenZoneUpperLimit)
            {
                return  (amount * greenZoneCost).ToString("0.000",cultureInfo);
            }
            else if (amount > greenZoneUpperLimit && amount <= blueZoneUpperLimit)
            {
                return  (greenZoneUpperLimit * greenZoneCost + (amount - greenZoneUpperLimit) * blueZoneCost).ToString("0.000", cultureInfo);
            }
            else if (amount > blueZoneUpperLimit)
            {
                return (greenZoneUpperLimit * greenZoneCost + (blueZoneUpperLimit - greenZoneUpperLimit) * blueZoneCost + (amount - blueZoneUpperLimit) * redZoneCost).ToString("0.000", cultureInfo);
            }
            else
            {
                return string.Empty;
            }

        }
    }
}
