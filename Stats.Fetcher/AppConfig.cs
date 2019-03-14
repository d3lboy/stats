namespace Stats.Fetcher
{
    public class AppConfig
    {
        public string ApplicationName { get; set; }
        public string ApiUrl { get; set; }
        public int MinJobsInCache { get; set; }
        public int CheckFrequency { get; set; }
    }
}
