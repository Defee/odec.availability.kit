using System;
using System.Linq;
using odec.Availability.Models;
using odec.Availability.Utils.Abst;

namespace odec.Availability.Utils
{
    /// <inheritdoc/>
    public class AvailabilityManager : IAvailabilityManager
    {
        /// <inheritdoc/>
        public TimeSpan CalculateAvailability(ObservationTime observation)
        {
            TimeSpan step = TimeSpan.FromHours(24);
            var totalSpan = observation.ObservationInterval.End.Subtract(observation.ObservationInterval.Start).Ticks;
            long availableTicks = 0;
            DateTime iterationDate = observation.ObservationInterval.Start;
            if (step.Days > 1)
            {
                throw new ArgumentOutOfRangeException(nameof(step), "step should be less then a day in ticks");
            }

            var modulo = totalSpan % step.Ticks;
            long totalMistake = 0;
            for (long i = 0; i <= totalSpan; i = i + step.Ticks)
            {
                var isNewDay = iterationDate.AddTicks(step.Ticks).Date != iterationDate.Date || i == 0;

                
                

                if (observation.WeekWorkingDays.All(it => it.Value != (int) iterationDate.DayOfWeek))
                {
                    iterationDate = iterationDate.AddTicks(step.Ticks);
                    continue;
                }
                    
                if (observation.FullDayLeaves.Any(it => it.Start <= iterationDate
                                                        && iterationDate <= it.End))
                {

                    iterationDate = iterationDate.AddTicks(step.Ticks);
                    continue;
                }
                if (!isNewDay)
                {
                    totalMistake = iterationDate.Date.AddDays(1).Ticks - iterationDate.Ticks + totalMistake;
                    iterationDate = iterationDate.AddTicks(step.Ticks);
                    continue;
                }


                iterationDate = iterationDate.AddTicks(step.Ticks);
                availableTicks += step.Ticks;
            }
            availableTicks += modulo;
            return TimeSpan.FromTicks(availableTicks);
        }
        /// <inheritdoc/>
        public TimeSpan WorkAvailability(ObserveWorkingTime observeWorkingTime)
        {
            TimeSpan step = TimeSpan.FromHours(24);
            var totalSpan = observeWorkingTime.ObservationInterval.End.Subtract(observeWorkingTime.ObservationInterval.Start).Ticks;
            long availableTicks = 0;
            DateTime iterationDate = observeWorkingTime.ObservationInterval.Start;
            if (step.Days > 1)
            {
                throw new ArgumentOutOfRangeException(nameof(step), "step should be less then a day in ticks");
            }

            var modulo = totalSpan % step.Ticks;
            long totalMistake = 0;
            for (long i = 0; i < totalSpan; i = i + step.Ticks)
            {

                var isNewDay = iterationDate.AddTicks(step.Ticks).Date != iterationDate.Date || i == 0;
                iterationDate = iterationDate.AddTicks(step.Ticks);
                if (!isNewDay)
                {
                    totalMistake = iterationDate.Date.AddDays(1).Ticks - iterationDate.Ticks + totalMistake;
                    continue;
                }
                if (observeWorkingTime.WeekWorkingDays.All(it => it.Value != (int)iterationDate.DayOfWeek))
                    continue;
                //TODO: check for the crossing in dates.
                var leavesOnDate = observeWorkingTime.Leaves.Where(it => it.Start <= iterationDate
                                                                         && iterationDate <= it.End);
                if (leavesOnDate.Any())
                {
                    var leaveHoursSum = leavesOnDate.Sum(it => it.HoursPerDay);
                    if (leaveHoursSum < observeWorkingTime.ObservationInterval.HoursPerDay)
                    {
                        availableTicks += TimeSpan.FromHours(Convert.ToDouble(observeWorkingTime.ObservationInterval.HoursPerDay - leaveHoursSum)).Ticks;
                    }
                    continue;
                }
                availableTicks += TimeSpan
                    .FromHours(Convert.ToDouble(observeWorkingTime.ObservationInterval.HoursPerDay)).Ticks;

            }
            return TimeSpan.FromTicks(availableTicks);
        }
        /// <inheritdoc/>
        public decimal TimeSpanToWorkingDays(decimal workingDayHours, TimeSpan time)
        {
            try
            {
                if (workingDayHours == 0) throw new DivideByZeroException();

                if (time.Equals(TimeSpan.FromMilliseconds(0))) return 0;

                var totalHours = Convert.ToDecimal(time.TotalHours);
                var result = totalHours / workingDayHours;
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
