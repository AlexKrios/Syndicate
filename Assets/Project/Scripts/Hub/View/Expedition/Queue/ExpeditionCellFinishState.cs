using System.Collections.Generic;
using JetBrains.Annotations;
using Syndicate.Battle;
using Syndicate.Core.Entities;
using Syndicate.Core.Services;
using Syndicate.Core.StateMachine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Syndicate.Hub.View
{
    public class ExpeditionCellFinishState : IState
    {
        [Inject] private readonly IAssetsService _assetsService;
        [Inject] private readonly IExpeditionService _expeditionService;
        [Inject] private readonly BattleManager _battleManager;

        private readonly ExpeditionQueueCellView _cell;

        public ExpeditionCellFinishState(ExpeditionQueueCellView cell)
        {
            _cell = cell;
        }

        public void Enter()
        {
            var data = _cell.Data;
            var location = _expeditionService.GetLocation(new LocationId(data.Key));
            var sprite = _assetsService.GetSprite(location.IconAssetId);
            _cell.SetCellIcon(sprite);
            _cell.SetReadyTimer();
        }

        public async void Click()
        {
            await _expeditionService.RemoveExpedition(_cell.Data.Guid);

            _cell.SetStateReady();

            var location = _expeditionService.GetLocation((LocationId)_cell.Data.Key);

            var allies = new List<UnitPosObject>();
            foreach (var (key, value) in _cell.Data.Roster)
            {
                var ally = new UnitPosObject
                {
                    slotId = (ExpeditionSlotId) key,
                    unitId = (UnitId) value
                };

                allies.Add(ally);
            }

            _battleManager.SetData(allies, location.Enemies);

            SceneManager.LoadScene("Battle");
        }

        public void Exit() { }

        [UsedImplicitly]
        public class Factory : PlaceholderFactory<ExpeditionQueueCellView, ExpeditionCellFinishState> { }
    }
}