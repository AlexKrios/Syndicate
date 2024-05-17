using Syndicate.Battle;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Battle
{
    public class BattleInputHandler : MonoBehaviour
    {
        [Inject] private BattleManager _battleManager;
    
        private Ray _ray;
        private RaycastHit _hit;
    
        private void Update()
        {
            RayMechanic();
        }

        private void RayMechanic()
        {
            if (Camera.main != null) _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (_battleManager.CanClick)
            {
                if (Physics.Raycast(_ray, out _hit, 100))
                {
                    if (_hit.transform.GetComponent<AbstractUnit>())
                    {
                        if (Input.GetKeyDown(KeyCode.Mouse0))
                        {
                            //_battleSignalBus.Fire(HitObj = new BattleInputSignal());

                            _battleManager.TargetUnit = _hit.transform.GetComponent<AbstractUnit>();
                            _battleManager.CurrentUnit.Turn();
                        }
                    }
                }
            }
        }
    }
}
