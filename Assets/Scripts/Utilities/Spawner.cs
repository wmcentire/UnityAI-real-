using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Agent[] agents;
    public LayerMask layerMask;

    int index = 0;

    void Update()
    {
        if(Input.GetKey(KeyCode.Tab)) index = ++index % agents.Length;
        if(Input.GetKey(KeyCode.Alpha2)) index = 1;
        if (Input.GetMouseButtonDown(0) || (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftControl)))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, 100, layerMask))
            {
                Instantiate(agents[index], hitInfo.point, Quaternion.AngleAxis(Random.Range(0,360),Vector3.up));
            }
        }
    }
}
