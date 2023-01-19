using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointNavNode : NavNode
{
    [SerializeField] private NavNode[] nodes;
    [SerializeField, Range(1, 10)] private float radius = 1;

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        
    }
}
