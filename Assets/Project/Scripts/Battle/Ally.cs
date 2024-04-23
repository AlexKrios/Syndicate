using Syndicate.Core.Entities;
using UnityEngine;

namespace Syndicate.Battle
{
    public class Ally : AbstractUnit
    {
        public override void Attack(AbstractUnit target)
        {
            base.Attack(target);

            if (Data.OriginalData.UnitTypeId == UnitTypeId.Support)
            {
                if (target.side == SideType.Allies)
                {
                    Debug.Log("Heal");
                    target.Data.Health += Damage;
                    Debug.Log(target.Data.Healthв);
                }
                else if (target.side == SideType.Enemies)
                {
                    Debug.Log("Attack");
                    target.Data.Health -= Damage - target.Armor;
                }

                foreach (var ally in _battleManager.listAllies)
                {
                    ally.floorDefend.SetActive(false);
                }
            }
            else
            {
                Debug.Log("Different Attack");
                target.Data.Health -= Data.Damage - target.Data.Armor;
            }
            
            foreach (var enemy in _battleManager.listEnemies)
            {
                enemy.floorDefend.SetActive(false);
            }
            
            if (target.Data.Health <= 0)
            {
                target.IsAlive = false;
                    
                _battleManager.listEnemies.Remove(target);
                _battleManager.listUnits.Remove(target);

                Destroy(target.gameObject);
            }

            _battleManager.CheckBattleEnd();

            _battleManager.SortingUnits();
        }
    }
}