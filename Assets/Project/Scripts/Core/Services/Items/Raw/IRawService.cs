using System.Collections.Generic;
using Syndicate.Core.Entities;

namespace Syndicate.Core.Services
{
    public interface IRawService
    {
        void LoadRawObjectData(ItemDto data);

        List<RawObject> GetAllRaw();
        RawObject GetRawByKey(RawItemId key);
    }
}