using System;
using Syndicate.Core.Entities;

[Serializable]
public class BattleUnitObject
{
    public int CurrentHealth { get; set; }
    
    public UnitObject OriginalData { get; }

    public BattleUnitObject(UnitObject data)
    {
        CurrentHealth = data.Health;
        
        OriginalData = data;
    }
}