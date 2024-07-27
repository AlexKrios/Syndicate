using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Syndicate.Core.Entities;
using Syndicate.Core.Services;
using Syndicate.Core.View;
using Syndicate.Hub.View.Main;
using Syndicate.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;
using Zenject;

namespace Syndicate.Hub.View
{
    public class ExpeditionView : ScreenViewBase<ExpeditionViewModel>
    {
        [Inject] private readonly IAssetsService _assetsService;
        [Inject] private readonly IScreenService _screenService;
        [Inject] private readonly IItemsProvider _itemsProvider;
        [Inject] private readonly IUnitsService _unitsService;
        [Inject] private readonly IExpeditionService _expeditionService;
        [Inject] private readonly IComponentViewFactory _componentViewFactory;

        [SerializeField] private Button close;

        [Space]
        [SerializeField] private List<ExpeditionUnitSelectView> units;

        [Header("Locations")]
        [SerializeField] private Transform locationsParent;
        [SerializeField] private List<ExpeditionLocationView> locations;

        [Header("Sidebar")]
        [SerializeField] private Image itemIcon;
        [SerializeField] private TMP_Text timerValue;
        //[SerializeField] private Image starIcon;
        [SerializeField] private LocalizeStringEvent itemName;
        [SerializeField] private List<ExpeditionPartView> rewards;
        [SerializeField] private RequestButton send;
        [SerializeField] private Button starButton;
        [SerializeField] private TMP_Text starCount;

        private ExpeditionLocationView _currentLocation;
        private int _currentStar;

        private ExpeditionLocationView CurrentLocation
        {
            get => _currentLocation;
            set
            {
                if (_currentLocation != null)
                    _currentLocation.SetInactive();

                _currentLocation = value;
                _currentLocation.SetActive();
            }
        }
        private int CurrentStar
        {
            set
            {
                _currentStar = value <= Constants.MaxStar ? value : 1;
                starCount.text = _currentStar.ToString();
            }
        }

        private void Awake()
        {
            close.onClick.AddListener(CloseClick);
            send.Button.onClick.AddListener(SendClick);
            starButton.onClick.AddListener(StarClick);
        }

        protected override void OnBind()
        {
            ViewModel.UpdateView += UpdateView;
        }

        private async void OnEnable()
        {
            await UniTask.Yield();

            CurrentStar = 1;
            CreateLocations();
            SetUnits();
            SetSidebarData();
        }

        private void CloseClick()
        {
            _screenService.Back();
        }

        private void UpdateView()
        {
            SetUnits();
            SetSidebarData();
        }

        private void CreateLocations()
        {
            if (locations.Count != 0)
                locations.ForEach(x => x.gameObject.SetActive(false));

            var locationObjects = _expeditionService.GetAllLocations();
            for (var i = 0; i < locationObjects.Count; i++)
            {
                if (locations.ElementAtOrDefault(i) == null)
                    locations.Add(_componentViewFactory.Create<ExpeditionLocationView>(locationsParent));

                locations[i].SetData(locationObjects[i]);
                locations[i].OnClickEvent += OnLocationClick;
                locations[i].gameObject.SetActive(true);
            }

            CurrentLocation = locations.First();
        }

        private void OnLocationClick(ExpeditionLocationView item)
        {
            if (CurrentLocation == item) return;

            CurrentLocation = item;

            SetUnits();
            SetSidebarData();
        }

        private void SetUnits()
        {
            foreach (var cell in units)
            {
                if (cell.SideId == UnitSideId.Ally)
                {
                    if (ViewModel.Roster.TryGetValue(cell.SlotId, out var unitId))
                    {
                        if (!string.IsNullOrEmpty(unitId))
                        {
                            var unit = _unitsService.GetUnit(new UnitId(unitId));
                            cell.SetData(unit);
                        }
                        else
                        {
                            cell.ResetData();
                        }
                    }
                }
                else if (cell.SideId == UnitSideId.Enemy)
                {
                    var data = CurrentLocation.Data;
                    var unitData = data.Enemies.FirstOrDefault(x => x.slotId == cell.SlotId);
                    if (unitData != null)
                    {
                        var unit = _unitsService.GetUnit(unitData.unitId);
                        cell.SetData(unit);
                    }
                    else
                    {
                        cell.SetData(null);
                    }
                }
            }
        }

        private void SetSidebarData()
        {
            var data = CurrentLocation.Data;

            itemIcon.sprite = _assetsService.GetSprite(data.IconAssetId);
            itemName.StringReference = data.NameLocale;
            timerValue.text = TimeUtil.DateCraftTimer(data.WayTime);

            for (var i = 0; i < rewards.Count; i++)
            {
                var rewardCell = rewards[i];
                if (data.Rewards.ElementAtOrDefault(i) == null)
                {
                    rewardCell.SetData(null);
                    continue;
                }

                var rewardObject = _itemsProvider.GetItem(data.Rewards[i]);
                rewardCell.SetData(rewardObject, data.Rewards[i].Count);
            }

            SetSendButtonState();
        }

        private async void SendClick()
        {
            var data = CurrentLocation.Data;
            var expeditionObject = new ExpeditionObject
            {
                Guid = Guid.NewGuid(),
                Key = data.Key,
                TimeEnd = DateTime.Now.AddSeconds(data.WayTime).ToFileTime(),
                Index = _expeditionService.GetFreeCell(),
                Roster = ViewModel.Roster
            };

            await _expeditionService.AddExpedition(expeditionObject);

            _screenService.Back();
        }

        private void SetSendButtonState()
        {
            send.Button.interactable = ViewModel.Roster.Any(x => x.Value != null);
        }

        private void StarClick()
        {
            CurrentStar = _currentStar + 1;

            SetUnits();
            SetSidebarData();
        }
    }
}