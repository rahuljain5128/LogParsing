using System;

namespace LogParser.Entity
{
    public class ApiLogOutput
    {
        public string Url { get; set; }
        public string Method { get; set; }
        public int Count { get; set; }
        public int MinTime { get; set; }
        public int MaxTime { get; set; }
        public double TotalTime { get; set; }
    }
}
