using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolling : Enemy
{
    

    protected override void Start()
    {
        base.Start();

    }

    public override void FSMProcess()
    {
        //Debug.Log("Using overriden process");
    }
}
