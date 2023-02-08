using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    private float timer;
    public PatrolState(StateAgent owner) : base(owner)
    {

    }

    public override void OnEnter()
    {
        owner.movement.Resume();
        owner.navigation.targetNode = owner.navigation.GetNearestNode();
        Debug.Log(owner.navigation.targetNode);
        owner.timer.value = (float)Random.Range(5,10);
    }

    public override void OnExit()
    {
        Debug.Log("Patrol Exit");
    }

    public override void OnUpdate()
    {
    }
}
