using System;
using System.Linq;
using HtmlAgilityPack;

namespace Kan.News
{
    public static class HtmlNodeExtensions
    {
        public static HtmlNode FirstElementOrDefault(this HtmlNode node, string name)
            => node.FirstOrDefault(n => n.Name == name);
        public static HtmlNode FirstOrDefault(this HtmlNode node, Func<HtmlNode, bool> predicate) 
            => node.ChildNodes.FirstOrDefault(predicate);
        public static HtmlNode LastElementOrDefault(this HtmlNode node, string name)
            => node.LastOrDefault(n => n.Name == name);
        public static HtmlNode LastOrDefault(this HtmlNode node, Func<HtmlNode, bool> predicate) 
            => node.ChildNodes.LastOrDefault(predicate);
    }
}