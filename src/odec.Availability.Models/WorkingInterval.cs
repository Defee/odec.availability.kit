using System;
using odec.Framework.Generic.Utility;

namespace odec.Availability.Models
{
    /// <summary>
    /// Extension of the Interval object in odec.Framework,
    /// which adds extra property how much hours per day team/member can work in the time interval.
    /// </summary>
    public class WorkingInterval:Interval<DateTime>
    {
        /// <summary>
        /// You should pass hours in this object to be constructed - no default constructor allowed.
        /// </summary>
        /// <param name="hours"></param>
        public WorkingInterval(decimal hours)
        {
            HoursPerDay = hours;
        }
        /// <summary>
        /// How much hours per day the team/member can relate to work in the particular interval.
        /// </summary>
        public decimal HoursPerDay { get; set; }
    }
}