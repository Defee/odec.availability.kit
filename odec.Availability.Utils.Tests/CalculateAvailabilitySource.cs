using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using NUnit.Framework;
using odec.Availability.Models;
using odec.Framework.Generic.Utility;

namespace odec.Availability.Utils.Tests
{
    public class CalculateAvailabilitySource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            var obj1 = new CalculateAvailabilityCase
            {
                Src = new ObservationTime
                {
                    FullDayLeaves = new List<Interval<DateTime>>() {
                        new Interval<DateTime>
                        {
                            Start = new DateTime(2018, 1, 1),
                            End = new DateTime(2018, 1, 5)
                        }},
                    ObservationInterval = new Interval<DateTime>
                    {
                        Start = new DateTime(2018, 1, 1),
                        End = new DateTime(2018, 2, 1)
                    },
                    WeekWorkingDays = AvailabilityManagerTester.defaultWorkingDays
                },
                Result = new ExpectedResult<decimal>
                {
                    ShouldFail = false,
                    Value = 19
                }
            };
            var obj2 = new CalculateAvailabilityCase
            {
                Src = new ObservationTime
                {
                    FullDayLeaves = new List<Interval<DateTime>>() {
                        new Interval<DateTime>
                        {
                            Start = new DateTime(2018, 1, 2),
                            End = new DateTime(2018, 1, 8)
                        }},
                    ObservationInterval = new Interval<DateTime>
                    {
                        Start = new DateTime(2018, 1, 1),
                        End = new DateTime(2018, 2, 1)
                    },
                    WeekWorkingDays = AvailabilityManagerTester.defaultWorkingDays
                },
                Result = new ExpectedResult<decimal>
                {
                    ShouldFail = false,
                    Value = 19
                }
            };
            //var obj3 = new CalculateAvailabilityCase
            //{
                
            //    Src = new ObservationTime
            //    {
            //        FullDayLeaves = new List<Interval<DateTime>>() {
            //            new Interval<DateTime>
            //            {
            //                Start = new DateTime(2018, 1, 2),
            //                End = new DateTime(2018, 1, 8)
            //            }},
            //        ObservationInterval = new Interval<DateTime>
            //        {
            //            Start = new DateTime(2018, 1, 1),
            //            End = new DateTime(2018, 2, 1)
            //        },
            //        WeekWorkingDays = AvailabilityManagerTester.defaultWorkingDays
            //    },
            //    Result = new ExpectedResult<decimal>
            //    {
            //        ShouldFail = false,
            //        Value = 19
            //    }
            //};
            //var obj4 = new CalculateAvailabilityCase
            //{

            //};
            //var obj5 = new CalculateAvailabilityCase
            //{

            //};
            //var obj6 = new CalculateAvailabilityCase
            //{

            //};
            yield return
                new TestCaseData(obj1).SetName("CalculateAvailability: " +
                                               JsonConvert.SerializeObject((object) obj1).Replace('.', ','));
            yield return
                new TestCaseData(obj2).SetName("CalculateAvailability: " +
                                               JsonConvert.SerializeObject((object) obj2).Replace('.', ','));
            //yield return
            //    new TestCaseData(obj3).SetName("CalculateAvailability: " +
            //                                   JsonConvert.SerializeObject(obj3).Replace('.', ','));
            //yield return
            //    new TestCaseData(obj4).SetName("CalculateAvailability: " +
            //                                   JsonConvert.SerializeObject(obj4).Replace('.', ','));
            //yield return
            //    new TestCaseData(obj5).SetName("CalculateAvailability: " +
            //                                   JsonConvert.SerializeObject(obj3).Replace('.', ','));
            //yield return
            //    new TestCaseData(obj6).SetName("CalculateAvailability: " +
            //                                   JsonConvert.SerializeObject(obj4).Replace('.', ','));
        }
    }
}