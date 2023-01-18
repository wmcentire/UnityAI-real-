using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistancePerception : Perception
{
    public override GameObject[] GetGameObjects()
    {
        List<GameObject> gameObjects = new List<GameObject>();

        Collider[] colliders = Physics.OverlapSphere(transform.position, distance);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject == gameObject)
            {
                continue;
            }
            if (tagName == "" || collider.CompareTag(tagName))
            {

                gameObjects.Add(collider.gameObject);

                // calculate angle from transform forward vector to direction of game object 
                Vector3 direction = (collider.transform.position - transform.position).normalized;

                float cos = Vector3.Dot(transform.forward, direction);
                float angle = Mathf.Acos(cos) * Mathf.Rad2Deg;

                //< angle is less than or equal to maximum angle
                if (angle <= maxAngle)
                {
                    //< add collider to result >
                    gameObjects.Add((GameObject)collider.gameObject);
                }
            }

        }

        gameObjects.Sort(CompareDistance);

        return gameObjects.ToArray();
    }

}
