using System;
using odec.Availability.Models;

namespace odec.Availability.Utils.Abst
{
    /// <summary>
    /// Availability Manager Abstraction which will allow to operate and calculate the leaves.
    /// </summary>
    public interface IAvailabilityManager
    {
        /// <summary>
        /// Calculates Simplified availability that will not count partial leaves of team/member.
        /// </summary>
        /// <param name="observation">Observation interval within which you will calculate the leaves</param>
        /// <returns>Total time in TimeSpan</returns>
        TimeSpan CalculateAvailability(ObservationTime observation);
        /// <summary>
        /// Converts TimeSpan to the working day hours. 
        /// </summary>
        /// <param name="workingDayHours">How much hours team/person is working in the TimeSpan provided.</param>
        /// <param name="time">TimeSpan which person/team has worked.</param>
        /// <returns>Decimal number of days.</returns>
        decimal TimeSpanToWorkingDays(decimal workingDayHours, TimeSpan time);
        /// <summary>
        /// Calculates work availability that will count partial leaves of team/member.
        /// </summary>
        /// <param name="observeWorkingTime">Observation working time within which you want to calculate the availability</param>
        /// <returns>Total time in TimeSpan</returns>
        TimeSpan WorkAvailability(ObserveWorkingTime observeWorkingTime);
    }
}