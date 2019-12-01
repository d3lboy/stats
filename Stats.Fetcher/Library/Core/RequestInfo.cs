using System.Collections.Generic;
using Stats.Common.Dto;

namespace Stats.Fetcher.Library.Core
{
    public class RequestInfo
    {
        public RequestInfo()
        {
            Data = new List<BaseDto>();
        }
        public string Endpoint { get; set; }
        public bool SingleDto { get; set; } = false;
        public List<BaseDto> Data { get; set; }
    }
}
