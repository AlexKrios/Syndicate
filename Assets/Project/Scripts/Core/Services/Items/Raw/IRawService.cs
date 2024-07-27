using System.Collections.Generic;
using Syndicate.Core.Entities;

namespace Syndicate.Core.Services
{
    public interface IRawService
    {
        void LoadData(ItemDto data);

        Dictionary<string, ItemDto> CreateRaw();

        RawObject GetRaw(PartObject part);
        RawObject GetRaw(RawId key);
    }
}