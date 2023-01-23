using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavNode : MonoBehaviour
{
    [SerializeField] public List<NavNode> neighbors;
    [SerializeField, Range(0.5f, 10)] public float radius = 1;

    private void OnValidate()
    {
        GetComponent<SphereCollider>().radius = radius;
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

    // Start is called before the first frame update
    public static NavNode[] GetNodes()
    {
        return FindObjectsOfType<NavNode>();
    }

    public static NavNode GetRandomNode()
    {
        var nodes = GetNodes();
        return (nodes == null) ? null : nodes[Random.Range(0, nodes.Length)];
    }
}
