using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutonomousAgent : Agent
{
    public Perception flockPerception;
    public AutonomousAgentData data;


    public float wanderDistance = 1;
    [Range(3,9)]public float wanderRadius = 3;
    public float wanderDisplacement = 5;

    public float wanderAngle { get; set; } = 0;


    // Update is called once per frame
    void Update()
    {
        var gameObjects = perception.GetGameObjects();
        foreach (var gameObject in gameObjects)
        {
            Debug.DrawLine(transform.position, gameObject.transform.position);
        }
        if (gameObjects.Length > 0)
        {
            movement.ApplyForce(Steering.Seek(this, gameObjects[0]) * data.seekWeight);
            movement.ApplyForce(Steering.Flee(this, gameObjects[0]) * data.fleeWeight);
        }

        gameObjects = flockPerception.GetGameObjects();

        foreach (var gameObject in gameObjects)
        {
            Debug.DrawLine(transform.position, gameObject.transform.position, Color.cyan);
        }

        if (gameObjects.Length > 0)
        {
            movement.ApplyForce(Steering.Cohesion(this,gameObjects) * data.cohesionWeight);
        }

        if (gameObjects.Length > 0)
        {
            movement.ApplyForce(Steering.Separation(this, gameObjects, data.separationRadius) * data.separationWeight);
        }

        if (gameObjects.Length > 0)
        {
            movement.ApplyForce(Steering.Alignment(this, gameObjects) * data.alignmentWeight);
        }

        if (movement.acceleration.sqrMagnitude <= movement.maxForce * 0.1f)
        {
            movement.ApplyForce(Steering.Wander(this));
        }

        transform.position = Utilities.Wrap(transform.position, new Vector3(-20, -20, -20), new Vector3(20, 20, 20));
    }
}
