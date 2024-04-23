using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Syndicate.Core.Entities;
using Syndicate.Core.Services;
using UnityEngine;
using Zenject;

namespace Syndicate.Battle
{
    public class BattleManager
    {
        [Inject] private IUnitsService _unitsService;
        [Inject] private readonly DiContainer _container;

        public readonly List<AbstractUnit> listUnits = new();

        public readonly List<AbstractUnit> listAllies = new();
        public readonly List<AbstractUnit> listEnemies = new();
        
        public AbstractUnit _currentUnit { get; set; }
        public AbstractUnit _targetUnit { get; set; }

        private List<string> unitListID = new()
        {
            "unit_trooper",
            "unit_defender",
            "unit_support",
            "unit_sniper",
            "unit_sniper"
        };
        
        public void InstantiateUnits()
        {
            for (int i = 0; i < unitListID.Count; i++)
            {
                if (unitListID.ElementAtOrDefault(i) == null)
                {
                    continue;
                }
                
                var point = BattleStarter.Instance.spawnPointAllies[i];
                var unitData = _unitsService.GetUnit(new UnitId(unitListID[i]));
                var unitInstantiate = _container.InstantiatePrefabForComponent<AbstractUnit>(unitData.PrefabAlly, point);
                var unitBattleData = new BattleUnitObject(unitData);
                unitInstantiate.Data = unitBattleData;

                unitInstantiate.CanAttack = false;
                unitInstantiate.IsStep = false;
                unitInstantiate.IsAlive = true;

                unitInstantiate.OnStartTurn += UnitStartTurn;
                unitInstantiate.OnEndTurn += UnitEndTurn;
                
                listAllies.Add(unitInstantiate);
                listUnits.Add(unitInstantiate);
            }
            
            for (int i = 0; i < unitListID.Count; i++)
            {
                if (unitListID.ElementAtOrDefault(i) == null)
                {
                    continue;
                }
                
                var point = BattleStarter.Instance.spawnPointEnemies[i];
                var unitData = _unitsService.GetUnit(new UnitId(unitListID[i]));
                var unitInstantiate = _container.InstantiatePrefabForComponent<AbstractUnit>(unitData.PrefabEnemy, point);
                var unitBattleData = new BattleUnitObject(unitData);
                unitInstantiate.Data = unitBattleData;

                unitInstantiate.CanAttack = false;
                unitInstantiate.IsStep = false;
                unitInstantiate.IsAlive = true;

                unitInstantiate.side = SideType.Enemies;
                
                unitInstantiate.OnStartTurn += UnitStartTurn;
                unitInstantiate.OnEndTurn += UnitEndTurn;
                
                listEnemies.Add(unitInstantiate);
                listUnits.Add(unitInstantiate);
            }
            
            SortingUnits();
        }

        public void SortingUnits()
        {
            var sortList = listUnits
                .OrderByDescending(x => x.IsAlive)
                .ThenByDescending(x => !x.IsStep)
                .ThenByDescending(x => x.Initiative).ToList();
            _currentUnit = sortList[0];
            _currentUnit.CanAttack = true;
            _currentUnit.ActiveUnit = true;
            _currentUnit.StartTurn();
        }

        private void EndRound()
        {
            foreach (var unit in listUnits)
            {
                unit.IsStep = false;
            }

            Debug.Log("Round End");
        }

        private void EndBattle()
        {
            Debug.Log("Battle End");
        }

        private void UnitEndTurn()
        {
            var isAllyBattleContinue = listAllies.All(x => x.IsStep);
            var isEnemyBattleContinue = listEnemies.All(x => x.IsStep);
            
            if (isAllyBattleContinue && isEnemyBattleContinue)
            {
                EndRound();
            }
            
            SortingUnits();
        }

        public void CheckBattleEnd()
        {
            var isAllyDead = listAllies.All(x => !x.IsAlive);
            var isEnemyDead = listEnemies.All(x => !x.IsAlive);

            if (isAllyDead || isEnemyDead)
            {
                EndBattle();
            }
        }

        private void UnitStartTurn()
        {
            if (_currentUnit.side == SideType.Allies)
            {
                foreach (var unit in listUnits)
                {
                    if (unit.CanAttack)
                    {
                        unit.floorAttack.SetActive(true);
                    }
                }

                if (_currentUnit.Data.OriginalData.UnitTypeId == UnitTypeId.Support)
                {
                    foreach (var ally in listAllies)
                    {
                        ally.floorDefend.SetActive(true);
                    }

                    foreach (var enemy in listEnemies)
                    {
                        enemy.floorDefend.SetActive(true);
                    }

                    _currentUnit.floorDefend.SetActive(false);
                    _currentUnit.floorAttack.SetActive(true);
                }
                else
                {
                    foreach (var enemy in listEnemies)
                    {
                        enemy.floorDefend.SetActive(true);
                    }
                }
            }
        }
    }
}
