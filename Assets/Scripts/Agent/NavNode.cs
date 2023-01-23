using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavNode : MonoBehaviour
{
    [SerializeField] private NavNode[] neighbors;
    [SerializeField, Range(0.5f, 10)] private float radius = 1;
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
