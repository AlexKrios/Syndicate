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

        private readonly List<AbstractUnit> listUnits = new();

        private readonly List<AbstractUnit> listAllies = new();
        private readonly List<AbstractUnit> listEnemies = new();

        private List<string> unitListID = new()
        {
            "unit_trooper",
            "unit_defender",
            "unit_support",
            "unit_sniper"
        };

        private AbstractUnit _activeUnit;
        
        public void InstantiateUnits()
        {
            foreach (var point in BattleStarter.Instance.spawnPointAllies)
            {
                var unitData = _unitsService.GetUnit(new UnitId(unitListID[0]));
                var unitInstantiate = _container.InstantiatePrefabForComponent<AbstractUnit>(unitData.Prefab, point);
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
            foreach (var point in BattleStarter.Instance.spawnPointEnemies)
            {
                var unitData = _unitsService.GetUnit(new UnitId(unitListID[0]));
                var unitInstantiate = Object.Instantiate(unitData.Prefab, point);
                var unitComponent = unitInstantiate.GetComponent<AbstractUnit>();
                
                unitComponent.CanAttack = false;
                unitComponent.IsStep = false;
                unitComponent.IsAlive = true;

                unitComponent.side = SideType.Enemies;
                
                unitComponent.OnStartTurn += UnitStartTurn;
                unitComponent.OnEndTurn += UnitEndTurn;
                
                listEnemies.Add(unitComponent);
                listUnits.Add(unitComponent);
            }
            
            SortingUnits();
        }

        private void SortingUnits()
        {
            var sortList = listUnits
                .OrderByDescending(x => x.IsAlive)
                .ThenByDescending(x => !x.IsStep)
                .ThenByDescending(x => x.Initiative).ToList();
            _activeUnit = sortList[0];
            _activeUnit.CanAttack = true;
            _activeUnit.StartTurn();
        }

        public async void Attack(AbstractUnit target)
        {
            if (target.side == SideType.Enemies)
            {
                var activeUnitT = _activeUnit.transform;
                var startQuaternion = activeUnitT.rotation;
                
                activeUnitT.rotation = Quaternion.LookRotation(target.transform.position);

                await UniTask.Delay(1000);
                
                //TODO Поменять после внедрения класса для изменения трансформа
                // ReSharper disable once Unity.InefficientPropertyAccess
                activeUnitT.rotation = startQuaternion;

                target.Health -= _activeUnit.Damage - target.Armor;
                _activeUnit.IsStep = true;
                _activeUnit.CanAttack = false;
                _activeUnit.floorAttack.SetActive(false);
                foreach (var enemy in listEnemies)
                {
                    enemy.floorDefend.SetActive(false);
                }

                if (target.Health <= 0)
                {
                    target.IsAlive = false;
                    
                    listEnemies.Remove(target);
                    listUnits.Remove(target);

                    Object.Destroy(target.gameObject);
                }

                CheckBattleEnd();

                SortingUnits();
            }
            else if (_activeUnit.Data.OriginalData.UnitTypeId == UnitTypeId.Support && target.side == SideType.Allies)
            {
                target.Health += _activeUnit.Damage;
                _activeUnit.IsStep = true;
                _activeUnit.CanAttack = false;
                _activeUnit.floorAttack.SetActive(false);
                foreach (var ally in listAllies)
                {
                    ally.floorDefend.SetActive(false);
                }
                
                CheckBattleEnd();

                SortingUnits();
            }
            else if (target.side == SideType.Allies && _activeUnit.side == SideType.Enemies)
            {
                target.Health -= _activeUnit.Damage - target.Armor;
                _activeUnit.IsStep = true;
                _activeUnit.CanAttack = false;
                _activeUnit.floorAttack.SetActive(false);
                foreach (var enemy in listEnemies)
                {
                    enemy.floorDefend.SetActive(false);
                }

                if (target.Health <= 0)
                {
                    target.IsAlive = false;
                    
                    listAllies.Remove(target);
                    listUnits.Remove(target);

                    Object.Destroy(target.gameObject);
                }

                CheckBattleEnd();

                SortingUnits();
            }   
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

        private void CheckBattleEnd()
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
            if (_activeUnit.side == SideType.Allies)
            {
                foreach (var unit in listUnits)
                {
                    if (unit.CanAttack)
                    {
                        unit.floorAttack.SetActive(true);
                    }
                }

                if (_activeUnit.Data.OriginalData.UnitTypeId == UnitTypeId.Support)
                {
                    foreach (var ally in listAllies)
                    {
                        ally.floorDefend.SetActive(true);
                    }

                    _activeUnit.floorDefend.SetActive(false);
                    _activeUnit.floorAttack.SetActive(true);
                }
                else
                {
                    foreach (var enemy in listEnemies)
                    {
                        enemy.floorDefend.SetActive(true);
                    }
                }
            }
            else if(_activeUnit.side == SideType.Enemies)
            {
                Attack(listAllies[0]);
            }
        }
    }
}
