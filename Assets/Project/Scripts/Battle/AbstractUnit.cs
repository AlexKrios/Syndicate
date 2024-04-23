using System;
using Cysharp.Threading.Tasks;
using Syndicate.Core.Entities;
using UnityEngine;
using Zenject;

namespace Syndicate.Battle
{
    public abstract class AbstractUnit : MonoBehaviour
    {
        [Inject] public BattleManager _battleManager;

        public Action OnEndTurn;
        public Action OnStartTurn;

        public GameObject floorAttack;
        public GameObject floorDefend;

        public int Health { get; set; }
        public int Damage { get; set; }
        public int Initiative { get; set; }
        public int Armor { get; set; }

        public bool ActiveUnit { get; set; }
        public bool IsAlive { get; set; }
        public bool CanAttack { get; set; }
        public bool IsStep { get; set; }

        public SideType side;

        public BattleUnitObject Data { get; set; }
        
        public void StartTurn()
        {
            OnStartTurn?.Invoke();

            if (side == SideType.Enemies)
            {
                Turn();
            }
        }

        public void Turn()
        {
            if(_battleManager._currentUnit.side == SideType.Enemies)
            {
                _battleManager._currentUnit.Attack(_battleManager.listAllies[0]);
            }
            
            EndTurn();
        }

        private void EndTurn()
        {
            OnEndTurn?.Invoke();
        }

        public virtual void Attack(AbstractUnit target)
        {
            var activeUnitT = transform;
            var startQuaternion = activeUnitT.rotation;
                
            activeUnitT.rotation = Quaternion.LookRotation(target.transform.position);
            
            //TODO Поменять после внедрения класса для изменения трансформа
            // ReSharper disable once Unity.InefficientPropertyAccess
            activeUnitT.rotation = startQuaternion;

            IsStep = true;
            CanAttack = false;
            floorAttack.SetActive(false);
        }
    }
}