using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Syndicate.Battle
{
    public abstract class AbstractUnit : MonoBehaviour
    {
        [Inject] protected BattleManager BattleManager;

        public Animator animator;

        public Action OnEndTurn;
        public Action OnStartTurn;

        public GameObject floorAttack;
        public GameObject floorDefend;

        public bool IsAlive { get; set; }
        public bool IsStep { get; set; }

        public SideType side;
        private static readonly int TurnTrigger = Animator.StringToHash("Turn");

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
            
            floorAttack.SetActive(false);

            foreach (var enemy in BattleManager.ListEnemies)
            {
                enemy.floorDefend.SetActive(false);
            }
            foreach (var ally in BattleManager.ListAllies)
            {
                ally.floorDefend.SetActive(false);
            }

            var targetTransform = BattleManager.TargetUnit.transform.position;
            
            var activeUnitT = transform;
            var startRotation = activeUnitT.rotation;
            var position = activeUnitT.position;
            
            var direction = Quaternion.LookRotation(targetTransform - position);

            transform.DORotateQuaternion(direction, 0.6f);

            //activeUnitT.rotation = Quaternion.LookRotation(BattleManager.TargetUnit.transform.position);

            animator.SetFloat(TurnTrigger, targetTransform.x);
            
            await UniTask.Delay(1000);
            
            if (BattleManager.CurrentUnit.side == SideType.Enemies)
            {
                Attack(BattleManager.ListAllies[0]);
            }
            else if (BattleManager.CurrentUnit.side == SideType.Allies)
            {
                Attack(BattleManager.TargetUnit);
            }

            transform.DORotateQuaternion(startRotation, 0.6f);
            
            //TODO Поменять после внедрения класса для изменения трансформа
            // ReSharper disable once Unity.InefficientPropertyAccess
            //activeUnitT.rotation = startQuaternion;

            animator.SetFloat(TurnTrigger, startRotation.x);
            
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