using System.Collections;
using System.Collections.Generic;
using Syndicate.Core.Configurations;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "Unit", menuName = "Scriptable/Unit", order = 0)]
public class UnitClassScriptableObjects : ScriptableObject
{
    public enum Side
    {
        Allies,
        Enemy
    }
    
    public GameObject prefabAlly;
    public GameObject prefabEnemy;
    
    public int Health;
    public int Damage;
    public int Initiative;
    public int Armor;

    private bool IsAlive;
    private bool CanAttack;
    private bool IsStep;
    

    public Side side;
}
