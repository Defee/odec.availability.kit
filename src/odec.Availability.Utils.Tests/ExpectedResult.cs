namespace odec.Availability.Utils.Tests
{
    public class ExpectedResult<T>
    {
        public T Value { get; set; }
        public bool ShouldFail { get; set; }
    }
}