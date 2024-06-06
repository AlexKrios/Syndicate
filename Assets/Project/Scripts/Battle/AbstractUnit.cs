using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Syndicate.Battle
{
    public abstract class AbstractUnit : MonoBehaviour
    {
        [Inject] protected BattleManager battleManager;
        
        public Animator animator;

        public Action OnEndTurn { get; set; }
        public Action OnStartTurn { get; set; }

        public GameObject floorAttack;
        public GameObject floorDefend;

        public bool IsAlive { get; set; }
        public bool IsStep { get; set; }

        public SideType side;
        
        private static readonly int TurnTrigger = Animator.StringToHash("Turn");
        private static readonly int Fire = Animator.StringToHash("Fire");

        public BattleUnitObject Data { get; set; }
        
        public void StartTurn()
        {
            battleManager.CanClick = true;
        
            OnStartTurn?.Invoke();

            if (side == SideType.Enemies)
            {
                Turn();
            }
        }

        public async void Turn()
        {
            battleManager.CanClick = false;
            
            floorAttack.SetActive(false);

            foreach (var enemy in battleManager.ListEnemies)
            {
                enemy.floorDefend.SetActive(false);
            }
            foreach (var ally in battleManager.ListAllies)
            {
                ally.floorDefend.SetActive(false);
            }

            if (battleManager.CurrentUnit.side == SideType.Enemies)
            {
                battleManager.TargetUnit = battleManager.ListAllies[0];
                Attack(battleManager.TargetUnit);
            }
            else if (battleManager.CurrentUnit.side == SideType.Allies)
            {
                Attack(battleManager.TargetUnit);
            }
            
            if (battleManager.TargetUnit == null)
            {
                return;
            }

            StartRotate();
            
            await UniTask.Delay(1000);

            EndTurn();
        }

        private void EndTurn()
        {
            battleManager.CheckBattleEnd();
            
            OnEndTurn?.Invoke();
        }

        protected virtual void Attack(AbstractUnit target)
        {
            IsStep = true;
            
            if (target.Data.CurrentHealth <= 0)
            {
                if (battleManager.CurrentUnit.side == SideType.Allies)
                {
                    battleManager.ListEnemies.Remove(target);
                }
                else if (battleManager.CurrentUnit.side == SideType.Enemies)
                {
                    battleManager.ListAllies.Remove(target);
                }
                
                battleManager.ListUnits.Remove(target);
                
                Destroy(target.gameObject);
            }
        }

        private async void StartRotate()
        {
            var targetTransform = battleManager.TargetUnit.transform.position;
            
            var activeUnitT = transform;
            var startRotation = activeUnitT.rotation;   
            var position = activeUnitT.position;
            
            var direction = Quaternion.LookRotation(targetTransform - position);

            animator.SetFloat(TurnTrigger, 1);
            
            await transform.DORotateQuaternion(direction, 0.6f).OnComplete(() => { animator.SetTrigger(Fire);}).ToUniTask();
            
            animator.SetFloat(TurnTrigger, 0);
            
            await UniTask.Delay(1000);

            animator.SetFloat(TurnTrigger, -1);

            transform.DORotateQuaternion(startRotation, 0.6f).OnComplete(() => animator.SetFloat(TurnTrigger, 0));
        }
    }
}