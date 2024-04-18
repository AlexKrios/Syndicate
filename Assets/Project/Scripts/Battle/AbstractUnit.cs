using System;
using Syndicate.Core.Entities;
using UnityEngine;

namespace Syndicate.Battle
{
    public abstract class AbstractUnit : MonoBehaviour
    {
        public Action OnEndTurn;
        public Action OnStartTurn;

        public GameObject floorAttack;
        public GameObject floorDefend;

        public int Health { get; set; }
        public int Damage { get; set; }
        public int Initiative { get; set; }
        public int Armor { get; set; }

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
            //anim
            
            EndTurn();
        }

        private void EndTurn()
        {
            OnEndTurn?.Invoke();
        }
    }
}