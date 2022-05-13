using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coco.Core.Entities
{
    public class WorkingDays : BaseEntity
    {
        public Guid StoreId { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }
        public TimeSpan TimeFrom { get; set; }
        public TimeSpan TimeTo { get; set; }

        public List<string> GetCurrentWorkingWeek()
        {
            var week = new List<string>();
            if (Monday)
            {
                week.Add("Monday");
            }
            if (Tuesday)
            {
                week.Add("Tuesday");
            }
            if (Wednesday)
            {
                week.Add("Wednesday");
            }
            if (Thursday)
            {
                week.Add("Thursday");
            }
            if (Friday)
            {
                week.Add("Friday");
            }
            if (Saturday)
            {
                week.Add("Saturday");
            }
            if (Sunday)
            {
                week.Add("Sunday");
            }

            return week;
        }

    }
}
