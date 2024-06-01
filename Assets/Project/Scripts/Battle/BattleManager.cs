using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Project.Scripts.Battle;
using Syndicate.Core.Entities;
using Syndicate.Core.Services;
using UnityEngine;
using Zenject;

namespace Syndicate.Battle
{
    [UsedImplicitly]
    public class BattleManager
    {
        [Inject] private IUnitsService _unitsService;
        [Inject] private readonly DiContainer _container;

        public readonly List<AbstractUnit> ListUnits = new();

        public readonly List<AbstractUnit> ListAllies = new();
        public readonly List<AbstractUnit> ListEnemies = new();
        
        public AbstractUnit CurrentUnit { get; private set; }
        public AbstractUnit TargetUnit { get; set; }

        public bool CanClick;

        private readonly List<string> _unitListID = new()
        {
            "unit_trooper",
            "unit_defender",
            "unit_support",
            "unit_sniper",
            "unit_sniper"
        };
        
        public void InstantiateUnits()
        {
            for (int i = 0; i < _unitListID.Count; i++)
            {
                if (_unitListID.ElementAtOrDefault(i) == null)
                {
                    continue;
                }
                
                var point = BattleStarter.Instance.spawnPointAllies[i];
                var unitData = _unitsService.GetUnit(new UnitId(_unitListID[i]));
                var unitInstantiate = _container.InstantiatePrefabForComponent<AbstractUnit>(unitData.PrefabAlly, point);
                var unitBattleData = new BattleUnitObject(unitData);
                unitInstantiate.Data = unitBattleData;

                unitInstantiate.IsStep = false;
                unitInstantiate.IsAlive = true;

                unitInstantiate.OnStartTurn += UnitStartTurn;
                unitInstantiate.OnEndTurn += UnitEndTurn;
                
                ListAllies.Add(unitInstantiate);
                ListUnits.Add(unitInstantiate);
            }
            
            for (int i = 0; i < _unitListID.Count; i++)
            {
                if (_unitListID.ElementAtOrDefault(i) == null)
                {
                    continue;
                }
                
                var point = BattleStarter.Instance.spawnPointEnemies[i];
                var unitData = _unitsService.GetUnit(new UnitId(_unitListID[i]));
                var unitInstantiate = _container.InstantiatePrefabForComponent<AbstractUnit>(unitData.PrefabEnemy, point);
                var unitBattleData = new BattleUnitObject(unitData);
                unitInstantiate.Data = unitBattleData;

                unitInstantiate.IsStep = false;
                unitInstantiate.IsAlive = true;

                unitInstantiate.side = SideType.Enemies;
                
                unitInstantiate.OnStartTurn += UnitStartTurn;
                unitInstantiate.OnEndTurn += UnitEndTurn;
                
                ListEnemies.Add(unitInstantiate);
                ListUnits.Add(unitInstantiate);
            }
            
            SortingUnits();
        }

        public void SortingUnits()
        {
            var sortList = ListUnits
                .OrderByDescending(x => x.IsAlive)
                .ThenByDescending(x => !x.IsStep)
                .ThenByDescending(x => x.Data.OriginalData.Initiative).ToList();
            CurrentUnit = sortList[0];
            CurrentUnit.StartTurn();
        }
        
        private void UnitStartTurn()
        {
            if (CurrentUnit.side == SideType.Allies)
            {
                if (CurrentUnit.Data.OriginalData.UnitTypeId == UnitTypeId.Support)
                {
                    foreach (var ally in ListAllies)
                    {
                        ally.floorDefend.SetActive(true);
                    }

                    foreach (var enemy in ListEnemies)
                    {
                        enemy.floorDefend.SetActive(true);
                    }
                    
                    CurrentUnit.floorDefend.SetActive(false);
                }
                else
                {
                    foreach (var enemy in ListEnemies)
                    {
                        enemy.floorDefend.SetActive(true);
                    }
                }
                
                CurrentUnit.floorAttack.SetActive(true);
            }
        }
        
        private void UnitEndTurn()
        {
            var isAllyBattleContinue = ListAllies.All(x => x.IsStep);
            var isEnemyBattleContinue = ListEnemies.All(x => x.IsStep);
            
            if (isAllyBattleContinue && isEnemyBattleContinue)
            {
                EndRound();
            }
            
            SortingUnits();
        }

        public void CheckBattleEnd()
        {
            var isAllyDead = ListAllies.All(x => !x.IsAlive);
            var isEnemyDead = ListEnemies.All(x => !x.IsAlive);

            if (isAllyDead || isEnemyDead)
            {
                EndBattle();
            }
        }
        
        private void EndRound()
        {
            foreach (var unit in ListUnits)
            {
                unit.IsStep = false;
            }

            Debug.Log("Round End");
        }

        private void EndBattle()
        {
            Debug.Log("Battle End");
        }
    }
}
