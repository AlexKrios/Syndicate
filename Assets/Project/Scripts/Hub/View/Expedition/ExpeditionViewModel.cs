using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Syndicate.Core.View;

namespace Syndicate.Hub.View
{
    [UsedImplicitly]
    public class ExpeditionViewModel : ViewModelBase, IScreenViewModel
    {
        public Action UpdateView { get; set; }

        public Dictionary<string, string> Roster { get; } = new();
    }
}