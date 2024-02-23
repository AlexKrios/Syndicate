using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UnitObject
{
    public int Health { get; set; }
    public int Damage { get; set; }
    public int Initiative { get; set; }
    public int Armor { get; set; }

    public UnitObject(UnitClassScriptableObjects data)
    {
        Health = data.Health;
        Damage = data.Damage;
        Initiative = data.Initiative;
        Armor = data.Armor;
    }
}