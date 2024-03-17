using System;
using System.Collections.Generic;
using Syndicate.Core.Entities;
using Syndicate.Core.StateMachine;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.SmartFormat.PersistentVariables;
using UnityEngine.UI;
using Zenject;

namespace Syndicate.Hub.View.Main
{
    public class ProductionQueueCellView : MonoBehaviour, IPointerClickHandler
    {
        private const string MoneyPattern = "<sprite=0> {0}";

        [Inject] private readonly ProductionCellLockedState.Factory _lockedStateFactory;
        [Inject] private readonly ProductionCellReadyState.Factory _readyStateFactory;
        [Inject] private readonly ProductionCellBusyState.Factory _busyStateFactory;
        [Inject] private readonly ProductionCellFinishState.Factory _finishStateFactory;

        [Header("Locked Section")]
        [SerializeField] private GameObject lockedWrapper;
        [SerializeField] private TMP_Text unlockCost;
        [SerializeField] private LocalizeStringEvent unlockLevel;

        [Header("Available Section")]
        [SerializeField] private GameObject availableWrapper;
        [SerializeField] private Image iconImage;
        [SerializeField] private GameObject plusImage;
        [SerializeField] private TMP_Text timerText;
        [SerializeField] private GameObject timerReady;

        [Space]
        [SerializeField] private List<GameObject> elementToReset;

        public ProductionObject Data { get; private set; }

        private readonly Dictionary<Type, IState> _stateMap = new();
        private IState _currentState;

        private void Awake()
        {
            _stateMap.Add(typeof(ProductionCellLockedState), _lockedStateFactory.Create(this));
            _stateMap.Add(typeof(ProductionCellReadyState), _readyStateFactory.Create(this));
            _stateMap.Add(typeof(ProductionCellBusyState), _busyStateFactory.Create(this));
            _stateMap.Add(typeof(ProductionCellFinishState), _finishStateFactory.Create(this));
        }

        private bool IsState<T>() where T : IState => _currentState == _stateMap[typeof(T)];

        private IState GetState<T>() where T : IState => _stateMap[typeof(T)];

        private void SetState<TState>() where TState : class, IState
        {
            _currentState?.Exit();

            elementToReset.ForEach(x => x.SetActive(false));

            _currentState = GetState<TState>();
            _currentState.Enter();

            lockedWrapper.SetActive(IsState<ProductionCellLockedState>());
            availableWrapper.SetActive(!IsState<ProductionCellLockedState>());
        }

        public void SetStateLocked()
        {
            SetState<ProductionCellLockedState>();
            unlockCost.text = string.Format(MoneyPattern, 10);
            ((IntVariable)unlockLevel.StringReference["value"]).Value = 10;
        }

        public void SetStateReady()
        {
            SetState<ProductionCellReadyState>();
            plusImage.SetActive(true);
        }

        public void SetStateBusy() => SetState<ProductionCellBusyState>();

        public void SetStateFinish() => SetState<ProductionCellFinishState>();

        public void OnPointerClick(PointerEventData eventData)
        {
            _currentState?.Click();
        }

        public void SetData(ProductionObject data) => Data = data;

        public void SetCellIcon(Sprite itemIcon)
        {
            iconImage.sprite = itemIcon;
            iconImage.gameObject.SetActive(true);
        }

        public void SetUnlockData(int cost, int level)
        {
            unlockCost.text = string.Format(MoneyPattern, cost);
            ((IntVariable)unlockLevel.StringReference["value"]).Value = level;
        }

        public void SetTimerText(string timer)
        {
            timerText.text = timer;
            timerText.gameObject.SetActive(true);
        }

        public void SetReadyTimer()
        {
            timerReady.gameObject.SetActive(true);
        }
    }
}