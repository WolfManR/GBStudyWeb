using System;

namespace Application
{
    public class BetweenDatesRequest
    {
        public DateTime FromTime { get; init; }
        public DateTime ToTime { get; set; }
    }
}