using System.Collections.Generic;
using Syndicate.Battle;
using UnityEngine;
using Zenject;

public class Units : MonoBehaviour
{
    [Inject] private BattleManager _battleManager;
    
    public List<UnitClassScriptableObjects> listScriptableUnits;

    public List<Transform> spawnPointAllies;
    public List<Transform> spawnPointEnemies;

    public static Units Instance;
    
    private void Awake()
    {
        Instance = this;
        _battleManager.InstantiateUnits();
    }
}