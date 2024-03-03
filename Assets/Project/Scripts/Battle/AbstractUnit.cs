using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class AbstractUnit : MonoBehaviour
{
    public Action OnEndTurn;
    public Action OnStartTurn;
    public Action EnemyAttack;

    public enum Side
    {
        Allies,
        Enemies
    }

    public enum UnitClass
    {
        Rifler,
        Sniper,
        Defender,
        Support
    }

    public GameObject floorAttack;
    public GameObject floorDefend;
    
    public int Health { get; set; }
    public int Damage { get; set;}
    public int Initiative { get; set;}
    public int Armor { get; set;}

    public bool IsAlive { get; set; }
    public bool CanAttack { get; set; }
    public bool IsStep { get; set; }
    

    public Side side;
    public UnitClass unitClass;
    
    public void StartTurn()
    {
        OnStartTurn?.Invoke();

        if (side == Side.Enemies)
        {
            Turn();
        }
    }

    public void Turn()
    {
        //anim

        EnemyAttack?.Invoke();
        
        EndTurn();
    }

    public void EndTurn()
    {
        OnEndTurn?.Invoke();
        
        //if EndBattle
    }
}