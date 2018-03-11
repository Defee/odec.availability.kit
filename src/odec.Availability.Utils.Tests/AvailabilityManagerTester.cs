using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using NUnit.Framework;
using odec.Availability.Models;
using odec.Availability.Utils.Abst;
using odec.Framework.Generic.Utility;

namespace odec.Availability.Utils.Tests
{
    public class AvailabilityManagerTester
    {
        protected IAvailabilityManager Manager = new AvailabilityManager();

        public static List<odec.Availability.Models.DayOfWeek> defaultWorkingDays = new List<odec.Availability.Models.DayOfWeek>
        {
            new odec.Availability.Models.DayOfWeek
            {
                Name = "Monday",
                Value = 1
            },
            new odec.Availability.Models.DayOfWeek
            {
                Name = "Tuesday",
                Value = 2
            },
            new odec.Availability.Models.DayOfWeek
            {
                Name = "Wensday",
                Value = 3
            },
            new odec.Availability.Models.DayOfWeek
            {
                Name = "Thursday",
                Value = 4
            },
            new odec.Availability.Models.DayOfWeek
            {
                Name = "Friday",
                Value = 5
            }
        };



        public class TimeSpanToWorkingDaysCase
        {
            public TimeSpan TimeSpan { get; set; }
            public decimal WorkingHours { get; set; }
            public ExpectedResult<decimal> Result { get; set; }
        }



        public class TimeSpanToWorkingDaysSource : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {

                var obj1 = new TimeSpanToWorkingDaysCase
                {
                    TimeSpan = TimeSpan.FromHours(20),
                    WorkingHours = 5,
                    Result = new ExpectedResult<decimal>
                    {
                        Value = 4,
                        ShouldFail = false
                    }
                };
                var obj2 = new TimeSpanToWorkingDaysCase
                {
                    TimeSpan = TimeSpan.FromHours(20),
                    WorkingHours = 5,
                    Result = new ExpectedResult<decimal>
                    {
                        Value = 4,
                        ShouldFail = false
                    }
                };
                var obj3 = new TimeSpanToWorkingDaysCase
                {
                    Result = new ExpectedResult<decimal>
                    {
                        Value = 4,
                        ShouldFail = true
                    }
                };
                var obj4 = new TimeSpanToWorkingDaysCase
                {
                    WorkingHours = 0,
                    Result = new ExpectedResult<decimal>
                    {
                        ShouldFail = true
                    }
                };
                var obj5 = new TimeSpanToWorkingDaysCase
                {
                    TimeSpan = TimeSpan.FromHours(4),
                    WorkingHours = 0,
                    Result = new ExpectedResult<decimal>
                    {
                        ShouldFail = true
                    }
                };
                var obj6 = new TimeSpanToWorkingDaysCase
                {
                    TimeSpan = TimeSpan.FromHours(0),
                    WorkingHours = 4,
                    Result = new ExpectedResult<decimal>
                    {
                        Value = 0,
                        ShouldFail = false
                    }
                };
                yield return
                    new TestCaseData(obj1).SetName("TimeSpanToWorkingDays: " +
                                                            JsonConvert.SerializeObject(obj1).Replace('.', ','));
                yield return
                    new TestCaseData(obj2).SetName("TimeSpanToWorkingDays: " +
                                                            JsonConvert.SerializeObject(obj2).Replace('.', ','));
                yield return
                    new TestCaseData(obj3).SetName("TimeSpanToWorkingDays: " +
                                                   JsonConvert.SerializeObject(obj3).Replace('.', ','));
                yield return
                    new TestCaseData(obj4).SetName("TimeSpanToWorkingDays: " +
                                                   JsonConvert.SerializeObject(obj4).Replace('.', ','));
                yield return
                    new TestCaseData(obj5).SetName("TimeSpanToWorkingDays: " +
                                                   JsonConvert.SerializeObject(obj3).Replace('.', ','));
                yield return
                    new TestCaseData(obj6).SetName("TimeSpanToWorkingDays: " +
                                                   JsonConvert.SerializeObject(obj4).Replace('.', ','));
            }
        }
        [TestCaseSource(typeof(TimeSpanToWorkingDaysSource))]
        [Test]
        public void TimeSpanToWorkingDays(TimeSpanToWorkingDaysCase tCase)
        {
            var result = 0M;
            if (tCase.Result.ShouldFail)
            {
                Assert.Throws<DivideByZeroException>(() => result = Manager.TimeSpanToWorkingDays(tCase.WorkingHours, tCase.TimeSpan));
            }
            else
            {
                Assert.DoesNotThrow(() => result = Manager.TimeSpanToWorkingDays(tCase.WorkingHours, tCase.TimeSpan));
                Assert.AreEqual(result, tCase.Result.Value);
            }
        }
        [TestCaseSource(typeof(CalculateAvailabilitySource))]
        //TODO: Test Cases
        [Test]
        public void CalculateAvailability(CalculateAvailabilityCase tCase)
        {

            decimal result = 0;

            if (tCase.Result.ShouldFail)
                Assert.Throws<Exception>(() => Manager.CalculateAvailability(tCase.Src));
            else
            {
                Assert.DoesNotThrow(() => result =
                    Convert.ToDecimal(Manager.CalculateAvailability(tCase.Src).TotalDays));
                Assert.AreEqual(result, tCase.Result.Value);
            }
        }
        //TODO: Test Cases
        [Test]
        [TestCaseSource(typeof(WorkAvailabilitySource))]
        public void WorkAvailability(WorkAvailabilityCase tCase)
        {
            TimeSpan result = TimeSpan.Zero;

            if (tCase.Result.ShouldFail)
            {
                Assert.Throws<Exception>(() => Manager.WorkAvailability(tCase.Src));
            }
            else
            {
                Assert.DoesNotThrow(() => result =
                    Manager.WorkAvailability(tCase.Src));
                Assert.AreEqual(Manager.TimeSpanToWorkingDays(tCase.Src.ObservationInterval.HoursPerDay, result), tCase.Result.Value);
            }
        }

        [Test]
        public void ResultsMatchingTest()
        {
            var cObject = new CalculateAvailabilityCase
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
                    WeekWorkingDays = defaultWorkingDays
                },
                Result = new ExpectedResult<decimal>
                {
                    ShouldFail = false,
                    Value = 19
                }
            };
            var wObject = new ObserveWorkingTime
            {
                WeekWorkingDays = defaultWorkingDays,
                Leaves = new List<WorkingInterval>
                {
                    new WorkingInterval(8)
                    {
                        Start = new DateTime(2018, 1, 1),
                        End = new DateTime(2018, 1, 5)
                    }
                },
                ObservationInterval = new WorkingInterval(8)
                {
                    Start = new DateTime(2018, 1, 1),
                    End = new DateTime(2018, 2, 1)
                }

            };
            TimeSpan result = TimeSpan.Zero;
            Assert.DoesNotThrow(() => result = Manager.WorkAvailability(wObject));
            Assert.DoesNotThrow(() => Manager.CalculateAvailability(cObject.Src));
            Assert.AreEqual(Manager.CalculateAvailability(cObject.Src).TotalDays, Manager.TimeSpanToWorkingDays(wObject.ObservationInterval.HoursPerDay, result));
        }
    }
}