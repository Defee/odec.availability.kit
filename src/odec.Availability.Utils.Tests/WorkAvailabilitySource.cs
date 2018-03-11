using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using NUnit.Framework;
using odec.Availability.Models;

namespace odec.Availability.Utils.Tests
{
    public class WorkAvailabilitySource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {

            var obj1 = new WorkAvailabilityCase
            {
                Src = new ObserveWorkingTime
                {
                    WeekWorkingDays = AvailabilityManagerTester.defaultWorkingDays,
                    Leaves = new List<WorkingInterval>
                    {
                        new WorkingInterval(8)
                        {
                            Start = new DateTime(2018, 1, 1),
                            End = new DateTime(2018, 1, 5)
                        },
                        new WorkingInterval(4)
                        {
                            Start = new DateTime(2018, 1, 5),
                            End = new DateTime(2018, 1, 10)
                        }
                    },
                    ObservationInterval = new WorkingInterval(8)
                    {
                        Start = new DateTime(2018, 1, 1),
                        End = new DateTime(2018, 2, 1)
                    }

                },
                Result = new ExpectedResult<decimal>
                {
                    ShouldFail = false,
                    Value = 17.5M
                }
            };
            var obj2 = new WorkAvailabilityCase
            {
                Src = new ObserveWorkingTime
                {
                    WeekWorkingDays = AvailabilityManagerTester.defaultWorkingDays,
                    Leaves = new List<WorkingInterval>
                    {
                        new WorkingInterval(8)
                        {
                            Start = new DateTime(2018, 1, 1),
                            End = new DateTime(2018, 1, 5)
                        },
                        new WorkingInterval(4)
                        {
                            Start = new DateTime(2018, 1, 5),
                            End = new DateTime(2018, 1, 12)
                        }
                    },
                    ObservationInterval = new WorkingInterval(8)
                    {
                        Start = new DateTime(2018, 1, 1),
                        End = new DateTime(2018, 2, 1)
                    }

                },
                Result = new ExpectedResult<decimal>
                {
                    ShouldFail = false,
                    Value = 16.5M
                }
            };
            //var obj3 = new WorkAvailabilityCase
            //{

            //};
            //var obj4 = new WorkAvailabilityCase
            //{

            //};
            //var obj5 = new WorkAvailabilityCase
            //{

            //};
            //var obj6 = new WorkAvailabilityCase
            //{

            //};
            yield return
                new TestCaseData(obj1).SetName("WorkAvailability: " +
                                               JsonConvert.SerializeObject((object) obj1).Replace('.', ','));
            yield return
                new TestCaseData(obj2).SetName("WorkAvailability: " +
                                               JsonConvert.SerializeObject((object) obj2).Replace('.', ','));
            //yield return
            //    new TestCaseData(obj3).SetName("WorkAvailability: " +
            //                                   JsonConvert.SerializeObject(obj3).Replace('.', ','));
            //yield return
            //    new TestCaseData(obj4).SetName("WorkAvailability: " +
            //                                   JsonConvert.SerializeObject(obj4).Replace('.', ','));
            //yield return
            //    new TestCaseData(obj5).SetName("WorkAvailability: " +
            //                                   JsonConvert.SerializeObject(obj3).Replace('.', ','));
            //yield return
            //    new TestCaseData(obj6).SetName("WorkAvailability: " +
            //                                   JsonConvert.SerializeObject(obj4).Replace('.', ','));
        }
    }
}