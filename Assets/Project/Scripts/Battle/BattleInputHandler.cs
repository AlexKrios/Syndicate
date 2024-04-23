using Syndicate.Battle;
using UnityEngine;
using Zenject;

public class BattleInputHandler : MonoBehaviour
{
    [Inject] private BattleManager _battleManager;
    
    private Ray ray;
    private RaycastHit hit;
    
    private void Update()
    {
        RayMechanic();
    }

    private void RayMechanic()
    {
        ray = Camera.main.ScreenPointToRay (Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit, 100))
        {
            if (hit.transform.GetComponent<AbstractUnit>())
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    //_battleSignalBus.Fire(HitObj = new BattleInputSignal());

                    _battleManager._targetUnit = hit.transform.GetComponent<AbstractUnit>();
                    _battleManager._currentUnit.Attack(_battleManager._targetUnit);
                }
            }
        }
    }
}
