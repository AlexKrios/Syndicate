using System;

[Serializable]
public class BattleUnitObject
{
    public int Health { get; set; }
    public int Damage { get; set; }
    public int Initiative { get; set; }
    public int Armor { get; set; }

    public BattleUnitObject(UnitClassScriptableObjects data)
    {
        Health = data.Health;
        Damage = data.Damage;
        Initiative = data.Initiative;
        Armor = data.Armor;
    }
}