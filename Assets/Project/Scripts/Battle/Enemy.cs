using Syndicate.Core.Entities;
using UnityEngine;

namespace Syndicate.Battle
{
    public class Enemy : AbstractUnit
    {
        protected override void Attack(AbstractUnit target)
        {
            base.Attack(target);
            
            if (Data.OriginalData.UnitTypeId == UnitTypeId.Support)
            {
                if (target.side == SideType.Enemies)
                {
                    target.Data.CurrentHealth += Data.OriginalData.Attack;
                }
                else if (target.side == SideType.Allies)
                {
                    target.Data.CurrentHealth -= Data.OriginalData.Attack - target.Data.OriginalData.Defense;
                }
            }
            else
            {
                target.Data.CurrentHealth -= Data.OriginalData.Attack - target.Data.OriginalData.Defense;
            }
        }
    }
}