using Syndicate.Core.Entities;
using UnityEngine;

namespace Syndicate.Battle
{
    public class Enemy : AbstractUnit
    {
        public override void Attack(AbstractUnit target)
        {
            base.Attack(target);
            
            if (Data.OriginalData.UnitTypeId == UnitTypeId.Support)
            {
                if (target.side == SideType.Enemies)
                {
                    target.Data.Health += Damage;
                }
                else if (target.side == SideType.Allies)
                {
                    target.Data.Health -= Data.Damage - target.Data.Armor;
                }
                
                foreach (var enemy in _battleManager.listEnemies)
                {
                    enemy.floorDefend.SetActive(false);
                }
            }
            else
            {
                target.Data.Health -= Data.Damage - target.Data.Armor;
            }

            if (target.Data.Health <= 0)
            {
                target.IsAlive = false;
                    
                _battleManager.listAllies.Remove(target);
                _battleManager.listUnits.Remove(target);

                Destroy(target.gameObject);
            }

            _battleManager.CheckBattleEnd();

            _battleManager.SortingUnits();
        }
    }
}