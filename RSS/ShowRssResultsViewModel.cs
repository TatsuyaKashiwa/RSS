using CodeHollow.FeedReader;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSS
{
    class ShowRssResultsViewModel
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public ReactivePropertySlim<string> Url { get; set; } = new ReactivePropertySlim<string>("");

        public ReactivePropertySlim<string> SearchWord { get; set; } = new ReactivePropertySlim<string>("");

        public ReactivePropertySlim<string> RssValue { get; set; } = new ReactivePropertySlim<string>("");

        public ReactiveCommandSlim ShowRssCommand { get; }

        public ReactivePropertySlim<bool> CanEnter { get; } = new ReactivePropertySlim<bool>(true);

        public ShowRssResultsViewModel() 
        {
            this.ShowRssCommand = this.CanEnter.ToReactiveCommandSlim().WithSubscribe(() => 
            {
                var feedTask = FeedReader.ReadAsync(Url.Value);

                try
                {
                    //フィード情報を取得してディクショナリに登録
                    var feed = feedTask.Result;

                    var items = feed.Items;

                    FeedDictionary.RegisterFeed(items);

                    var feedTitles = items
                                    .Select(x => x.Title)
                                    .Where(x => x.Contains(SearchWord.Value))
                                    .ToList();

                    StringBuilder sbTitles = new StringBuilder();

                    foreach (var title in feedTitles)
                    {
                        sbTitles.Append("タイトル：");
                        sbTitles.Append(title);
                        sbTitles.Append("URL：");
                        sbTitles.Append(FeedDictionary.getLink(title));
                        sbTitles.Append(Environment.NewLine);
                    }

                    RssValue.Value = (sbTitles.Length == 0)
                        ? "指定された文字列は見つかりませんでした"
                        : sbTitles.ToString();
                }
                catch (System.AggregateException)
                {
                    RssValue.Value = "アドレス未入力です";
                }
            }); 
        }
            
    }
}
