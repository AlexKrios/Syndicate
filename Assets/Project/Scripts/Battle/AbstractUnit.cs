using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Syndicate.Battle
{
    public abstract class AbstractUnit : MonoBehaviour
    {
        [Inject] protected BattleManager BattleManager;

        public Action OnEndTurn;
        public Action OnStartTurn;

        public GameObject floorAttack;
        public GameObject floorDefend;

        public bool IsAlive { get; set; }
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

        public async void Turn()
        {
            var activeUnitT = transform;
            var startQuaternion = activeUnitT.rotation;

            activeUnitT.rotation = Quaternion.LookRotation(BattleManager.TargetUnit.transform.position);

            await UniTask.Delay(1000);
            
            if (BattleManager.CurrentUnit.side == SideType.Enemies)
            {
                Attack(BattleManager.ListAllies[0]);
            }
            else
            {
                Attack(BattleManager.TargetUnit);
            }

            //TODO Поменять после внедрения класса для изменения трансформа
            // ReSharper disable once Unity.InefficientPropertyAccess
            activeUnitT.rotation = startQuaternion;

            EndTurn();
        }

        private void EndTurn()
        {
            OnEndTurn?.Invoke();
            
            BattleManager.CheckBattleEnd();

            BattleManager.SortingUnits();
        }

        protected virtual void Attack(AbstractUnit target)
        {
            IsStep = true;
            floorAttack.SetActive(false);
        }   
    }
}