using CodeHollow.FeedReader;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace RSS
{
    class ShowRssResultsViewModel
    {
        //メモリリークを防ぐために必要な記述
        public event PropertyChangedEventHandler? PropertyChanged;

        //表示させたいRSSのURLを保持する変数
        public ReactivePropertySlim<string> Url { get; set; } = new ReactivePropertySlim<string>("RSSのURLを入力");

        //検索単語
        public ReactivePropertySlim<string> SearchWord { get; set; } = new ReactivePropertySlim<string>("検索単語を入力してください");

        public ReactivePropertySlim<string> RssResult { get; set; } = new ReactivePropertySlim<string>("");

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

                    FeedDictionary.RegisterFeed(feed);

                    //RSS情報が保存されたディクショナリから
                    //入力された単語を含むキー(タイトル)の配列を取得
                    var feedTitles = FeedDictionary.getTitles(SearchWord.Value);

                    //文字列結合を多数繰り返し得るためStringBuilderインスタンスを生成
                    StringBuilder sbTitles = new StringBuilder();

                    //得られたタイトルの配列からタイトル・リンクを取得して
                    //それぞれStringBuilderに追加
                    foreach (var title in feedTitles)
                    {
                        sbTitles.Append($"タイトル：{title}{Environment.NewLine}");
                        sbTitles.Append($"URL：{FeedDictionary.getLink(title)}{Environment.NewLine}");
                    }

                    //結果表示領域に結果を表示
                    RssResult.Value = (sbTitles.Length == 0)
                        ? "指定された文字列は見つかりませんでした"
                        : sbTitles.ToString();
                }
                catch (System.AggregateException ex)
                {
                    //結果表示領域に例外に対応するメッセージを表示
                    RssResult.Value = (ex.InnerException is XmlException)
                    ? $"XMLに問題がございます。"
                    : $"アドレス未入力です";
                }
            }); 
        }
            
    }
}
