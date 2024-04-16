using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Syndicate.Battle
{
    public class BattleManager
    {
        public List<AbstractUnit> listUnits = new();

        public List<AbstractUnit> listAllies = new();
        public List<AbstractUnit> listEnemies = new();
        
        public AbstractUnit _activeUnit;
        
        private Ray _ray;

        private bool _enemyTurn;

        private bool nextBattleTurn;
        
        public void InstantiateUnits()
        {
            foreach (var point in Units.Instance.spawnPointAllies)
            {
                var unitInstantiate = Object.Instantiate(Units.Instance.listScriptableUnits[0].prefabAlly, point);
                
                var unitObject = new BattleUnitObject(Units.Instance.listScriptableUnits[0]);

                var unitComponent = unitInstantiate.GetComponent<AbstractUnit>();
                
                unitComponent.Health = unitObject.Health;
                unitComponent.Damage = unitObject.Damage;
                unitComponent.Initiative = unitObject.Initiative;
                unitComponent.Armor = unitObject.Armor;
                
                unitComponent.CanAttack = false;
                unitComponent.IsStep = false;
                unitComponent.IsAlive = true;

                unitComponent.OnStartTurn += UnitStartTurn;
                unitComponent.OnEndTurn += UnitEndTurn;
                
                listAllies.Add(unitComponent);
                listUnits.Add(unitComponent);
            }
            foreach (var point in Units.Instance.spawnPointEnemies)
            {
                var unitInstantiate = Object.Instantiate(Units.Instance.listScriptableUnits[0].prefabEnemy, point);
                
                var unitObject = new BattleUnitObject(Units.Instance.listScriptableUnits[0]);

                var unitComponent = unitInstantiate.GetComponent<AbstractUnit>();
                
                unitComponent.Health = unitObject.Health;
                unitComponent.Damage = unitObject.Damage;
                unitComponent.Initiative = unitObject.Initiative;
                unitComponent.Armor = unitObject.Armor;
                
                unitComponent.CanAttack = false;
                unitComponent.IsStep = false;
                unitComponent.IsAlive = true;
                
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
            if (target.side == AbstractUnit.Side.Enemies)
            {
                Quaternion startQuaternion = _activeUnit.transform.rotation;

                _activeUnit.transform.rotation =
                     Quaternion.LookRotation(target.transform.position);

                await UniTask.Delay(1000);
                
                _activeUnit.transform.rotation = startQuaternion;

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
            else if (_activeUnit.unitClass == AbstractUnit.UnitClass.Support && target.side == AbstractUnit.Side.Allies)
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
            else if (target.side == AbstractUnit.Side.Allies && _activeUnit.side == AbstractUnit.Side.Enemies)
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
            if (_activeUnit.side == AbstractUnit.Side.Allies)
            {
                foreach (var unit in listUnits)
                {
                    if (unit.CanAttack)
                    {
                        unit.floorAttack.SetActive(true);
                    }
                }

                if (_activeUnit.unitClass == AbstractUnit.UnitClass.Support)
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
            else if(_activeUnit.side == AbstractUnit.Side.Enemies)
            {
                Attack(listAllies[0]);
            }
        }
    }
}
