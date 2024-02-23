using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Unit", menuName = "ScriptableObjects/Unit", order = 1)]
public class UnitClassScriptableObjects : ScriptableObject
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

    public int Health;
    public int Damage;
    public int Initiative;
    public int Armor;

    private bool IsAlive;
    private bool CanAttack;
    private bool IsStep;
    private bool IsTarget;
    

    public Side side;
    public UnitClass unitClass;
}
