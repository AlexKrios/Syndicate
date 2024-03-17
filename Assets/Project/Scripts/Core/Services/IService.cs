using Cysharp.Threading.Tasks;

namespace Syndicate.Core.Services
{
    public interface IService
    {
        UniTask Initialize();
    }
}