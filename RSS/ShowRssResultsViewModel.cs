﻿using Reactive.Bindings;
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
            ShowRssCommand = CanEnter.ToReactiveCommandSlim().WithSubscribe(() => 
            {
                RssValue.Value = "a";
            }); 
        }
            
    }
}
