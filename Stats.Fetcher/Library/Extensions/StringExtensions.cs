namespace Stats.Fetcher.Library.Extensions
{
    public static class StringExtensions
    {
        public static string TrimFull(this string value)
        {
            return value.Trim(new[] { ' ', '\n' });
        }
    }
}
