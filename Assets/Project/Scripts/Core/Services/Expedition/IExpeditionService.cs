using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Syndicate.Core.Entities;
using Syndicate.Core.Profile;

namespace Syndicate.Core.Services
{
    public interface IExpeditionService
    {
        int Size { get; set; }

        void LoadData(ExpeditionState state);

        List<LocationObject> GetAllLocations();
        LocationObject GetLocation(LocationId key);

        int GetFreeCell();
        List<ExpeditionObject> GetAllExpedition();
        UniTask AddExpedition(ExpeditionObject data);
        UniTask RemoveExpedition(Guid id);
    }
}