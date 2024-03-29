using System.Collections.Generic;
using Syndicate.Core.Entities;

namespace Syndicate.Core.Services
{
    public interface IRawService
    {
        RawObject GetRawByKey(RawItemId key);

        List<RawObject> GetAllRaw();

        RawObject GetRawById(string id);
    }
}