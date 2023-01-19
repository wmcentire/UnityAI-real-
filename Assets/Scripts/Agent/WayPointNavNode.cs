using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class WayPointNavNode : NavNode
{
    [SerializeField] private NavNode[] nodes;
    [SerializeField, Range(0.5f, 10)] private float radius = 1;

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);

        Gizmos.color = Color.green;
        foreach (NavNode node in nodes)
        {
            Gizmos.DrawLine(transform.position, node.transform.position);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
    }

    private void OnValidate()
    {
        //checks to see if object has sphere collider
        GetComponent<SphereCollider>().radius = radius;
    }
}
