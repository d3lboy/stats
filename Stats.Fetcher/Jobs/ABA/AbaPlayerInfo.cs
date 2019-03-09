using System.Linq;
using HtmlAgilityPack;
using ScrapySharp.Extensions;
using Stats.Common.Dto;
using Stats.Fetcher.Library;

namespace Stats.Fetcher.Jobs.ABA
{
    public class AbaPlayerInfo : PlayerInfo
    {
        public override Player ParsePlayerInfo(HtmlDocument html)
        {
            var player = new Player
            {
                Name = html.DocumentNode.CssSelect(".club-information>strong").SingleOrDefault()?.InnerText
            };
            
            return player;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
