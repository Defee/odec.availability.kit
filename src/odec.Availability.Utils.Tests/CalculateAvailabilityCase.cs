using odec.Availability.Models;

namespace odec.Availability.Utils.Tests
{
    public class CalculateAvailabilityCase
    {
        public ObservationTime Src { get; set; }
        public ExpectedResult<decimal> Result { get; set; }
    }
}