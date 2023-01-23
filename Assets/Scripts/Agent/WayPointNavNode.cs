using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class WayPointNavNode : NavNode
{


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<NavAgent>(out NavAgent navAgent))
        {
            if (navAgent.targetNode == this)
            {
                navAgent.targetNode = neighbors[Random.Range(0, neighbors.Length)];
            }
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent<NavAgent>(out NavAgent navAgent))
        {
            if (navAgent.targetNode == this)
            {
                navAgent.targetNode = neighbors[Random.Range(0, neighbors.Length)];
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);

        Gizmos.color = Color.green;
        foreach (NavNode node in neighbors)
        {
            Gizmos.DrawLine(transform.position, node.transform.position);
        }
    }

    private void OnValidate()
    {
        //checks to see if object has sphere collider
        GetComponent<SphereCollider>().radius = radius;
    }
}
