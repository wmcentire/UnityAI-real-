using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutonomousAgent : Agent
{
    // Update is called once per frame
    void Update()
    {
        var gameObjects = perception.GetGameObjects();
        foreach (var gameObject in gameObjects)
        {
            Debug.DrawLine(transform.position,gameObject.transform.position);
        }
        if(gameObjects.Length > 0)
        {
            Vector3 direction = (gameObjects[0].transform.position - transform.position).normalized;
            Debug.DrawLine();
        }
    }
}
