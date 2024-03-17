using System.Collections.Generic;
using Syndicate.Core.Entities;

namespace Syndicate.Core.Services
{
    public interface IUnitsService
    {
        UnitObject GetUnit(UnitId assetId);

        List<UnitObject> GetAllUnits();
    }
}