﻿using HtmlAgilityPack;
using KNARZhelper;
using LinkUtilities.Models;
using Playnite.SDK;
using Playnite.SDK.Models;
using System;
using System.Collections.Generic;
using System.Net;

namespace LinkUtilities.Linker
{
    /// <summary>
    /// Adds a link to MobyGames.
    /// </summary>
    class LinkMobyGames : Link
    {
        public override string LinkName { get; } = "MobyGames";
        public override string BaseUrl { get; } = "https://www.mobygames.com/game/";
        public override string SearchUrl { get; } = "https://www.mobygames.com/search/?type=game&q=";

        public override string GetGamePath(Game game)
        {
            // MobyGames Links need the game name in lowercase without special characters and hyphens instead of white spaces.
            return game.Name.RemoveSpecialChars().CollapseWhitespaces().Replace(" ", "-").ToLower();
        }

        public override List<GenericItemOption> SearchLink(string searchTerm)
        {
            SearchResults.Clear();

            try
            {
                HtmlWeb web = new HtmlWeb();
                HtmlDocument doc = web.Load($"{SearchUrl}{searchTerm.UrlEncode()}");

                HtmlNodeCollection htmlNodes = doc.DocumentNode.SelectNodes("//table/tr/td[last()]");

                if (htmlNodes != null && htmlNodes.Count > 0)
                {
                    foreach (HtmlNode node in htmlNodes)
                    {
                        SearchResults.Add(new SearchResult
                        {
                            Name = WebUtility.HtmlDecode(node.SelectSingleNode("./b/a").InnerText),
                            Url = node.SelectSingleNode("./b/a").GetAttributeValue("href", ""),
                            Description = WebUtility.HtmlDecode(node.SelectSingleNode("./small[last()]").InnerText).CollapseWhitespaces()
                        }
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Error loading data from {LinkName}");
            }

            return base.SearchLink(searchTerm);
        }

        public LinkMobyGames(LinkUtilities plugin) : base(plugin)
        {
        }
    }
}