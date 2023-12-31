using KNARZhelper;
using Playnite.SDK.Models;

namespace LinkUtilities.Linker
{
    /// <summary>
    ///     Adds a search link to YouTube.
    /// </summary>
    internal class LinkYouTube : BaseClasses.Linker
    {
        public override string LinkName => "YouTube Gameplay";
        public override string LinkUrl => "https://www.youtube.com/results?search_query={0}+gameplay";
        public override string SearchUrl => "https://www.youtube.com/results?search_query=";
        public override bool NeedsToBeChecked { get; set; } = false;

        public override bool AddLink(Game game)
        {
            var gameQuery = game.Name.UrlEncode();
            return LinkHelper.AddLink(game, LinkName, string.Format(LinkUrl, gameQuery), false, true);
        }
    }
}