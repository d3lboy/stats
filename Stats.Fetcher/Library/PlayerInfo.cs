using HtmlAgilityPack;
using Stats.Common.Dto;

namespace Stats.Fetcher.Library
{
    public abstract class PlayerInfo
    {
        public abstract Player ParsePlayerInfo(HtmlDocument html);       
    }
}
