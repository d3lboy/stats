using System;
using System.Globalization;

namespace Stats.Fetcher.Jobs.ABA.Helpers
{
    public static class Parser
    {
        public static DateTime ToDate(string date)
        {
            try
            {
                date = date.Replace("CET", "").Trim();
                return DateTime.ParseExact(date, "dddd, dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                return DateTime.MaxValue;
            }
        }
    }
}
