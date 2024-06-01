using System.Collections.Generic;
using Syndicate.Core.Entities;
using Syndicate.Core.Profile;

namespace Syndicate.Core.Services
{
    public interface IUnitsService
    {
        void LoadUnits(UnitsState state);

        List<UnitObject> GetAllUnits();
        UnitObject GetUnit(UnitId assetId);
    }
}