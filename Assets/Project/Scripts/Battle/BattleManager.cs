using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] List<AbstractUnit> listUnits;

    [SerializeField] private List<AbstractUnit> listAllies;
    [SerializeField] private List<AbstractUnit> listEnemies;

    private AbstractUnit _getTurnUnit;

    private AbstractUnit _target;

    private AbstractUnit _hitObj;

    private Ray _ray;

    private bool _enemyTurn;
    
    private void Awake()
    {
        StartTurn();
    }

    private void Update()
    {
        MainMechanic();
    }

    private void MainMechanic()
    {
        if (_enemyTurn)
        {
            _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
        
            if (Physics.Raycast(_ray, out hit, 100))
            {
                _hitObj = hit.transform.GetComponent<AbstractUnit>();
            }
        }
    }

    private void SortingUnits()
    {
        var sortList = listUnits
            .OrderByDescending(x => x.IsAlive)
            .ThenByDescending(x => x.IsStep)
            .ThenByDescending(x => x.Initiative).ToList();
        _getTurnUnit = sortList[0];
        _getTurnUnit.CanAttack = true;
    }

    private void Initialize()
    {
        
    }
    
    private void Battle()
    {
        
    }

    private void EndBattle()
    {
        
    }
    
    private void StartTurn()
    {
        SortingUnits();
    }

    private void Turn()
    {
        
    }

    private void EndTurn()
    {
        
    }
}
