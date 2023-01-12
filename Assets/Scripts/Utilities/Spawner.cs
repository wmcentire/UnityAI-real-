using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Agent[] agents;
    public LayerMask layerMask;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, 100, layerMask))
            {
                if(Input.GetKey(KeyCode.Alpha1)) Instantiate(agents[0], hitInfo.point, Quaternion.identity);
                if(Input.GetKey(KeyCode.Alpha2)) Instantiate(agents[1], hitInfo.point, Quaternion.identity);
            }
        }
    }
}
