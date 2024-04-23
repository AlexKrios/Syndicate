using System;
using Syndicate.Core.Entities;

[Serializable]
public class BattleUnitObject
{
    public int Health { get; set; }
    public int Damage { get; set; }
    public int Initiative { get; set; }
    public int Armor { get; set; }
    
    public UnitObject OriginalData { get; }

    public BattleUnitObject(UnitObject data)
    {
        Health = 100;
        Damage = 40;
        Initiative = 20;
        Armor = 5;
        OriginalData = data;
    }
}