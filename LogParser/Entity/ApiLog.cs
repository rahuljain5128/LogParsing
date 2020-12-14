namespace LogParser.Entity
{
    public class ApiLog
    {
        public string Url { get; set; }
        public string Method { get; set; }
        public int ResponseCode { get; set; }
        public int ResponseTime { get; set; }
    }
}
