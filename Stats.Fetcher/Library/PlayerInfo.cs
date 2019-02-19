using HtmlAgilityPack;
using Stats.Fetcher.Library.Dto;

namespace Stats.Fetcher.Library
{
    public abstract class PlayerInfo
    {
        public abstract Player ParsePlayerInfo(HtmlDocument html);       
    }
}
