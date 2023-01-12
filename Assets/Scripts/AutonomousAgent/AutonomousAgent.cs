using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutonomousAgent : Agent
{
    public Perception flockPerception;

    [Range(0, 3)] public float cohesionWeight;
    [Range(0, 3)] public float separationWeight;
    [Range(0, 3)] public float alignmentWeight;

    [Range (0, 15)] public float separationRadius = 5;

    [Range(0, 3)] public float fleeWeight;
    [Range(0, 3)] public float seekWeight;


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
            movement.ApplyForce(Steering.Seek(this, gameObjects[0]) * seekWeight);
            movement.ApplyForce(Steering.Flee(this, gameObjects[0]) * fleeWeight);
        }

        gameObjects = flockPerception.GetGameObjects();

        foreach (var gameObject in gameObjects)
        {
            Debug.DrawLine(transform.position, gameObject.transform.position, Color.cyan);
        }

        if (gameObjects.Length > 0)
        {
            movement.ApplyForce(Steering.Cohesion(this,gameObjects) * cohesionWeight);
        }

        if (gameObjects.Length > 0)
        {
            movement.ApplyForce(Steering.Separation(this, gameObjects, separationRadius) * separationWeight);
        }

        if (gameObjects.Length > 0)
        {
            movement.ApplyForce(Steering.Alignment(this, gameObjects) * alignmentWeight);
        }

        if (movement.acceleration.sqrMagnitude <= movement.maxForce * 0.1f)
        {
            movement.ApplyForce(Steering.Wander(this));
        }

        transform.position = Utilities.Wrap(transform.position, new Vector3(-10, -10, -10), new Vector3(10, 10, 10));
    }
}
