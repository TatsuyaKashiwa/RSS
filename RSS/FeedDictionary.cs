using CodeHollow.FeedReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSS
{
    public class FeedDictionary
    {
        private static Dictionary<string, string> s_titleLink = new();

        public static void RegisterFeed(IList<FeedItem> items) 
        {
            foreach (FeedItem item in items) 
            {
                s_titleLink.Add(item.Title, item.Link);
            }
        }

        public static string getLink(string title) 
        {
            return s_titleLink[title];
        }
    }
}
