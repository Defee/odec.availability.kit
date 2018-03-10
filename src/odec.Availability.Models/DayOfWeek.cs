namespace odec.Availability.Models
{
    /// <summary>
    /// Model to describe day of week. In case if we have it stored in DB for some different logic
    /// </summary>
    public class DayOfWeek
    {
        /// <summary>
        /// Value - it will generally be the same as per DateTime DayOfWeek Enum, except if you want different logic.
        /// </summary>
        public int Value { get; set; }
        /// <summary>
        /// Generally it is like: Monday, Friday(Good-day :) ), etc. So it is possibility and flexibility to you to name days of week whatever you want
        /// </summary>
        public string Name { get; set; }
    }
}