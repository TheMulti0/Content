using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Content.Api;
using HtmlAgilityPack;

namespace Kan.News
{
    public static class NewsItemFactory
    {
        public static NewsItem Create(HtmlNode item)
        {
            HtmlNode infoBlock = item.LastElementOrDefault("div");
            HtmlNode contentInfo = infoBlock.FirstOrDefault(node => node.HasClass("program_list_link") && node.HasClass("w-inline-block"));
            

            string title = FindTitle(contentInfo);
            string description = FindDescription(contentInfo);
            
            HtmlNode additionalInfo = infoBlock
                .FirstOrDefault(node => node.HasClass("additional_info"));
            
            Author author = AuthorFactory.FromAuthor(additionalInfo);
            DateTime date = FindDate(additionalInfo);
            
            string url = FindUrl(contentInfo);

            HtmlNode mediaBlock = item.FirstElementOrDefault("div")?.FirstElementOrDefault("div");
            string imageUrl = FindImageUrl(mediaBlock);
            string videoUrl = FindVideoUrl(mediaBlock);

            return new NewsItem(
                NewsProviderType.KanNews,
                title,
                description,
                author,
                date,
                url,
                imageUrl,
                videoUrl);
        }
        
        private static string FindTitle(HtmlNode contentInfo)
        {
            HtmlNode title = contentInfo.FirstElementOrDefault("h2");
            return Decode(title.InnerText);
        }

        private static string FindDescription(HtmlNode contentInfo)
        {
            HtmlNode description = contentInfo.FirstElementOrDefault("p");
            return Decode(description.InnerText);
        }

        private static DateTime FindDate(HtmlNode additionalInfo)
        {
            HtmlNode date = additionalInfo
                .FirstElementOrDefault("div");

            string dateText = Decode(date.InnerText);
            
            return ParseDate(dateText);
        }
        
        private static string FindUrl(HtmlNode contentInfo)
        {
            string link = contentInfo.GetAttributeValue("href", string.Empty);
            
            return link == "#" 
                ? null 
                : KanNewsProvider.BaseAddress + link;
        }
        
        private static string FindImageUrl(HtmlNode mediaBlock)
        {
            // Example string inside the style attribute:
            // background-image: url('https://kanweb.blob.core.windows.net/download/pictures/2020/2/12/imgid=28446_A.jpeg');
            
            string imageStyleValue = mediaBlock.GetAttributeValue("style", string.Empty);
            return imageStyleValue?.Split('\'')[1];
        }

        private static string FindVideoUrl(HtmlNode mediaBlock)
        {
            HtmlNode frame = mediaBlock.FirstElementOrDefault("iframe");
            return frame?.GetAttributeValue("src", string.Empty);
        }

        private static DateTime ParseDate(string dateText)
        {
            // Example string: '14.02.2020  09:32'
            
            int[] split = dateText
                .Split(' ', '.', ':')
                .Select(str => int.Parse(Regex.Replace(str, @"\s", "")))
                .ToArray();
            return new DateTime(
                year: split[2],
                month: split[1],
                day: split[0],
                hour: split[3],
                minute: split[4],
                second: 0);
        }

        private static string Decode(string str)
        {
            str = HttpUtility
                .HtmlDecode(str)?
                .Replace("\r\n", string.Empty) ?? throw new NullReferenceException();
            return Regex.Replace(str," {2,}", " ");
        }
    }
}