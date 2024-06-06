using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Syndicate.Battle
{
    public abstract class AbstractUnit : MonoBehaviour
    {
        [Inject] protected BattleManager BattleManager;

        [SerializeField] private Rigidbody rigidbody;
        
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

            if (BattleManager.CurrentUnit.side == SideType.Enemies)
            {
                BattleManager.TargetUnit = BattleManager.ListAllies[0];
                Attack(BattleManager.TargetUnit);
            }
            else if (BattleManager.CurrentUnit.side == SideType.Allies)
            {
                Attack(BattleManager.TargetUnit);
            }
            
            if (BattleManager.TargetUnit == null)
            {
                return;
            }

            StartRotate();
            
            await UniTask.Delay(1000);

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

        private async void StartRotate()
        {
            var targetTransform = BattleManager.TargetUnit.transform.position;
            
            var activeUnitT = transform;
            var startRotation = activeUnitT.rotation;   
            var position = activeUnitT.position;
            
            var direction = Quaternion.LookRotation(targetTransform - position);

            animator.SetFloat(TurnTrigger, 1);
            
            await transform.DORotateQuaternion(direction, 0.6f).OnComplete(() => { animator.SetTrigger("Fire");}).ToUniTask();
            
            animator.SetFloat(TurnTrigger, 0);
            
            await UniTask.Delay(1000);

            animator.SetFloat(TurnTrigger, -1);

            transform.DORotateQuaternion(startRotation, 0.6f).OnComplete(() => animator.SetFloat(TurnTrigger, 0));
        }
    }
}