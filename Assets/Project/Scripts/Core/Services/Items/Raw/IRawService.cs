using System.Collections.Generic;
using Syndicate.Core.Entities;

namespace Syndicate.Core.Services
{
    public interface IRawService
    {
        RawObject GetRaw(RawItemId assetItemId);

        List<RawObject> GetAllRaw();
    }
}