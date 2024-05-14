using System.Collections.Generic;
using Syndicate.Battle;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Battle
{
    public class BattleStarter : MonoBehaviour
    {
        [Inject] private BattleManager _battleManager;
    
        public List<Transform> spawnPointAllies;
        public List<Transform> spawnPointEnemies;

        public static BattleStarter Instance;
    
        private void Awake()
        {
            Instance = this;
            _battleManager.InstantiateUnits();
        }
    }
}