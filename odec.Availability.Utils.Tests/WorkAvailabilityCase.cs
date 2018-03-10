using odec.Availability.Models;

namespace odec.Availability.Utils.Tests
{
    public class WorkAvailabilityCase
    {
        public ObserveWorkingTime Src { get; set; }
        public ExpectedResult<decimal> Result { get; set; }
    }
}