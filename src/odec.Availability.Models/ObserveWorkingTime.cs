using System.Collections.Generic;

namespace odec.Availability.Models
{
    /// <summary>
    /// This models is used when you want to calculate number of working days, 
    /// counting partly working days and partial leaves.
    /// </summary>
    public class ObserveWorkingTime
    {
        /// <summary>
        /// Time interval within which the observation will took place
        /// </summary>
        public WorkingInterval ObservationInterval { get; set; }
        /// <summary>
        /// Working days for the team/member/project. 
        /// </summary>
        public IList<DayOfWeek> WeekWorkingDays { get; set; }
        /// <summary>
        /// Working days for the team/member, counting partial leaves, half-hour leaves etc.
        /// </summary>
        public IList<WorkingInterval> Leaves { get; set; }
    }
}