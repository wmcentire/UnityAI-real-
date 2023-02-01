using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAgent : Agent
{


    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            animator.SetFloat("speed", 0.5f);
        }
        else
        {
            animator.SetFloat("speed", 0);
        }
    }
}
