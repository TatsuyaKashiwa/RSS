using CodeHollow.FeedReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RSS
{
    public static class FeedDictionary
    {
        private static Dictionary<string, string> s_titleLink = new();

        //指定したRSSのタイトル・リンクの対をディクショナリに保存
        //重複登録を防ぐため初回登録時にRSSのアドレスをKey、DescriptionをValueとして登録する。
        public static void RegisterFeed(Feed feed) 
        {
            var items = feed.Items;

            if (!s_titleLink.ContainsKey(feed.Link)) 
            {
                registerDictionary(items);

                s_titleLink[feed.Link] = feed.Description;
            }
        }

        //指定したタイトルに対応するリンクを取得
        public static string getLink(string title) => s_titleLink[title];
        

        //指定単語に部分一致するタイトルのListを返す
        public static List<string> getTitles(string title) 
        {
            return s_titleLink.Keys
                .Where(x=> Regex.IsMatch(x, $".*{title}.*"))
                .ToList();
        }

        //RSSのタイトルとリンクの対をディクショナリに保存
        //同一タイトルが複数ある場合は順に付番して重複を回避
        private static void registerDictionary(IList<FeedItem> items) 
        {
            var suffixNumber = 0;
            foreach (FeedItem item in items) 
            {
                if (s_titleLink.ContainsKey(item.Title))
                {
                    s_titleLink.Add(item.Title + suffixNumber, item.Link);
                    suffixNumber++;
                }
                else 
                {
                    s_titleLink.Add(item.Title, item.Link);
                }
            }
        }
    }
}
