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

        public ReactivePropertySlim<string> Url {  get; set; }

        public ReactivePropertySlim<string> SearchWord { get; set; }

        public ReactivePropertySlim<string> RssValue {  get; set; }

        public ReactiveCommandSlim ShowRssCommand { get; }
    }
}
