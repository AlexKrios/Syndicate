using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Syndicate.Battle
{
    public class BattleManager : MonoBehaviour
    {
        [SerializeField] List<AbstractUnit> listUnits;

        [SerializeField] private List<AbstractUnit> listAllies;
        [SerializeField] private List<AbstractUnit> listEnemies;


        private AbstractUnit _activeUnit;

        private AbstractUnit _targetUnit;

        private AbstractUnit _hitObj;

        private Ray _ray;

        private bool _enemyTurn;

        private bool nextBattleTurn;

        private void Awake()
        {
            Initialize();
        }

        private void Update()
        {
            MainMechanic();
        }

        private void MainMechanic()
        {
            if (!_enemyTurn)
            {
                _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit;
                if (Physics.Raycast(_ray, out hit, 100))
                {
                    _hitObj = hit.transform.GetComponent<AbstractUnit>();

                    if (_hitObj)
                    {
                        if (_hitObj.IsAlive)
                        {
                            if (Input.GetButtonDown("Fire1"))
                            {
                                if (_activeUnit.unitClass == AbstractUnit.UnitClass.Support)
                                {
                                    if (_activeUnit.CanAttack)
                                    {
                                        _targetUnit = _hitObj;

                                        _targetUnit.Health += _activeUnit.Damage;
                                        _activeUnit.CanAttack = false;
                                        _activeUnit.IsStep = true;
                                        _activeUnit.floorAttack.SetActive(false);

                                        foreach (var ally in listAllies)
                                        {
                                            ally.floorDefend.SetActive(false);
                                        }

                                        _activeUnit.Turn();
                                    }
                                }
                                else if (_hitObj.side == AbstractUnit.Side.Enemies)
                                {
                                    if (_activeUnit.CanAttack)
                                    {
                                        _targetUnit = _hitObj;

                                        _targetUnit.Health -= _activeUnit.Damage - _targetUnit.Armor;

                                        _activeUnit.CanAttack = false;
                                        _activeUnit.IsStep = true;
                                        _activeUnit.floorAttack.SetActive(false);

                                        foreach (var enemy in listEnemies)
                                        {
                                            enemy.floorDefend.SetActive(false);
                                        }

                                        if ((_targetUnit.Health <= 0))
                                        {
                                            _targetUnit.IsAlive = false;
                                            listUnits.Remove(_targetUnit);
                                            listEnemies.Remove(_targetUnit);
                                            Destroy(_targetUnit.gameObject);
                                        }

                                        _activeUnit.Turn();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void Initialize()
        {
            foreach (var unit in listAllies)
            {
                unit.Health = 100;
                unit.Damage = 20;
                unit.Initiative = 30;
                unit.Armor = 10;
                unit.IsAlive = true;

                unit.OnStartTurn += UnitStartTurn;
                unit.OnEndTurn += UnitEndTurn;
            }

            foreach (var unit in listEnemies)
            {
                unit.Health = 100;
                unit.Damage = 20;
                unit.Initiative = 30;
                unit.Armor = 10;
                unit.IsAlive = true;

                unit.EnemyAttack += EnemyAttackTurn;
                unit.OnEndTurn += UnitEndTurn;
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
            var isAllyBattleContinue = listUnits.All(x => x.IsStep);
            var isEnemyBattleContinue = listEnemies.All(x => x.IsStep);

            if (isAllyBattleContinue && isEnemyBattleContinue)
            {
                EndRound();
            }

            var isAllyDead = listAllies.All(x => !x.IsAlive);
            var isEnemyDead = listEnemies.All(x => !x.IsAlive);

            if (isAllyDead || isEnemyDead)
            {
                EndBattle();
            }

            SortingUnits();
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
        }

        private void EnemyAttackTurn()
        {
            if ((_activeUnit.side == AbstractUnit.Side.Enemies))
            {
                _targetUnit = listAllies[0];
                _targetUnit.Health -= _activeUnit.Damage - _targetUnit.Armor;
                _activeUnit.IsStep = true;

                if ((_targetUnit.Health <= 0))
                {
                    _targetUnit.IsAlive = false;
                    listUnits.Remove(_targetUnit);
                    listAllies.Remove(_targetUnit);
                    Destroy(_targetUnit.gameObject);
                }
            }
        }
    }
}
