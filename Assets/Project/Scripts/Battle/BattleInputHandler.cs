using System;
using Syndicate.Battle;
using UnityEngine;
using Zenject;

public class BattleInputHandler : MonoBehaviour
{
    [Inject] private BattleManager _battleManager;
    [Inject] private readonly SignalBus _battleSignalBus;
    
    private Ray ray;
    private RaycastHit hit;

    public BattleInputSignal HitObj;

    public static BattleInputHandler Instance;

    private void Awake()
    {
        Instance = this;
    }

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
                    
                    _battleManager.Attack(hit.transform.GetComponent<AbstractUnit>());
                }
            }
        }
    }
}
