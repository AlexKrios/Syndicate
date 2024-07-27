using Cysharp.Threading.Tasks;

namespace Syndicate.Core.Services
{
    public interface IGameService
    {
        UniTask LoadPlayerProfile();

        string Name { get; set; }
        public int Cash { get; set; }
        int Diamond { get; set; }

        UniTask SetName(string name);
        UniTask SetCash(int cash);
        UniTask SetDiamond(int diamond);
    }
}