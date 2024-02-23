using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class AbstractUnit : MonoBehaviour
{
    public enum Side
    {
        Allies,
        Enemy
    }

    public enum UnitClass
    {
        Rifler,
        Sniper,
        Defender,
        Support
    }

    public int Health { get; set; }
    public int Damage { get; set;}
    public int Initiative { get; set;}
    public int Armor { get; set;}

    public bool IsAlive { get; set; }
    public bool CanAttack { get; set; }
    public bool IsStep { get; set; }
    

    public Side side;
    public UnitClass unitClass;

    private List<UnitClassScriptableObjects> resourcesLoad;

    private void Awake()
    {
        resourcesLoad = Resources.LoadAll<UnitClassScriptableObjects>("Battle").ToList();
    }
    
    public void SetData(UnitObject obj)
    {
        Health = obj.Health;
        Damage = obj.Damage;
        Initiative = obj.Initiative;
        Armor = obj.Armor;
    }

    public void GetClass()
    {
        foreach (var resLoad in resourcesLoad)
        {
            if (resLoad.unitClass == UnitClassScriptableObjects.UnitClass.Rifler)
            {
                var obj = new UnitObject(resLoad);
                SetData(obj);
            }
            else if (resLoad.unitClass == UnitClassScriptableObjects.UnitClass.Sniper)
            {
                var obj = new UnitObject(resLoad);
                SetData(obj);
            }
            else if (resLoad.unitClass == UnitClassScriptableObjects.UnitClass.Defender)
            {
                var obj = new UnitObject(resLoad);
                SetData(obj);
            }
            else if (resLoad.unitClass == UnitClassScriptableObjects.UnitClass.Support)
            {
                var obj = new UnitObject(resLoad);
                SetData(obj);
            }
        }
    }
}