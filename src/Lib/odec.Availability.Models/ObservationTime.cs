using System;
using System.Collections.Generic;
using odec.Framework.Generic.Utility;

namespace odec.Availability.Models
{
    /// <summary>
    /// This models is used when you want to calculate simplified number of working days, 
    /// without counting partly working days etc.
    /// </summary>
    public class ObservationTime
    {
        /// <summary>
        /// Time interval where you want to observe workload or other event
        /// </summary>
        public Interval<DateTime> ObservationInterval { get; set; }
        /// <summary>
        /// Working days for the team/member/project. 
        /// </summary>
        public IList<DayOfWeek> WeekWorkingDays { get; set; }
        /// <summary>
        /// Day intervals where the team/member doesn't work at all. Like sick leaves or annual leaves.
        /// </summary>
        public IList<Interval<DateTime>> FullDayLeaves { get; set; }
    }
}
