using Syndicate.Core.Entities;

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
                
                foreach (var enemy in BattleManager.ListEnemies)
                {
                    enemy.floorDefend.SetActive(false);
                }
            }
            else
            {
                target.Data.CurrentHealth -= Data.OriginalData.Attack - target.Data.OriginalData.Defense;
            }

            if (target.Data.CurrentHealth <= 0)
            {
                target.IsAlive = false;
                    
                BattleManager.ListAllies.Remove(target);
                BattleManager.ListUnits.Remove(target);

                Destroy(target.gameObject);
            }

            BattleManager.CheckBattleEnd();

            BattleManager.SortingUnits();
        }
    }
}