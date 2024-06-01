using System;
using JetBrains.Annotations;
using Syndicate.Core.View;

namespace Syndicate.Hub.View.Main
{
    [UsedImplicitly]
    public class UnitsViewModel : ViewModelBase, IScreenViewModel
    {
        public Action ForceUpdate { get; set; }
    }
}