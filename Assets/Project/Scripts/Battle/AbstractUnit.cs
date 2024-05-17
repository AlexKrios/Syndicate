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
            BattleManager.CanClick = true;
        
            OnStartTurn?.Invoke();

            if (side == SideType.Enemies)
            {
                Turn();
            }
        }

        public async void Turn()
        {
            BattleManager.CanClick = false;
            
            var activeUnitT = transform;
            var startQuaternion = activeUnitT.rotation;
            
            activeUnitT.rotation = Quaternion.LookRotation(BattleManager.TargetUnit.transform.position);

            await UniTask.Delay(1000);
            
            if (BattleManager.CurrentUnit.side == SideType.Enemies)
            {
                Attack(BattleManager.ListAllies[0]);
            }
            else if (BattleManager.CurrentUnit.side == SideType.Allies)
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
            BattleManager.CheckBattleEnd();
            
            OnEndTurn?.Invoke();
        }

        protected virtual void Attack(AbstractUnit target)
        {
            IsStep = true;
            floorAttack.SetActive(false);
            
            if (target.Data.CurrentHealth <= 0)
            {
                if (BattleManager.CurrentUnit.side == SideType.Allies)
                {
                    BattleManager.ListEnemies.Remove(target);
                }
                else if (BattleManager.CurrentUnit.side == SideType.Enemies)
                {
                    BattleManager.ListAllies.Remove(target);
                }
                
                BattleManager.ListUnits.Remove(target);
                
                Destroy(target.gameObject);
            }
        }   
    }
}