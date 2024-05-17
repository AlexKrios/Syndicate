using Syndicate.Core.Entities;
using UnityEngine;

namespace Syndicate.Battle
{
    public class Ally : AbstractUnit
    {
        protected override void Attack(AbstractUnit target)
        {
            base.Attack(target);

            if (Data.OriginalData.UnitTypeId == UnitTypeId.Support)
            {
                if (target.side == SideType.Allies)
                {
                    target.Data.CurrentHealth += Data.OriginalData.Attack;
                }
                else if (target.side == SideType.Enemies)
                {
                    target.Data.CurrentHealth -= Data.OriginalData.Attack - target.Data.OriginalData.Defense;
                }

                foreach (var ally in BattleManager.ListAllies)
                {
                    ally.floorDefend.SetActive(false);
                }
            }
            else
            {
                target.Data.CurrentHealth -= Data.OriginalData.Attack - target.Data.OriginalData.Defense;
            }
            
            foreach (var enemy in BattleManager.ListEnemies)
            {
                enemy.floorDefend.SetActive(false);
            }
        }
    }
}