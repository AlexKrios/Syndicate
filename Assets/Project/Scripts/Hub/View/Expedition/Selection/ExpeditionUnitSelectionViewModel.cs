using System.Collections.Generic;
using JetBrains.Annotations;
using Syndicate.Core.Entities;
using Syndicate.Core.View;

namespace Syndicate.Hub.View
{
    [UsedImplicitly]
    public class ExpeditionUnitSelectionViewModel : ViewModelBase, IPopupViewModel
    {
        public ExpeditionSlotId CurrentIndex { get; set; }
        public List<UnitTypeId> UnitTypes { get; set; }
    }
}