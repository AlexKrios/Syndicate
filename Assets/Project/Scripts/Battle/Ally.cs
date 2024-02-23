using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : AbstractUnit
{
    public Ally()
    {
        
    }

    private void Start()
    {
        GetClass();
        Debug.Log(Health);
        Debug.Log(Damage);
        Debug.Log(Initiative);
        Debug.Log(Armor);
    }
}