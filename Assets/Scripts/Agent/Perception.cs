using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Perception : MonoBehaviour
{
    [Range(1, 40)] public float distance = 1;
    [Range(0,180)] public float maxAngle = 45;
    public string tagName = "";

    public abstract GameObject[] GetGameObjects();
//    List<GameObject> gameObjects = new List<GameObject>();

//    Collider[] colliders = Physics.OverlapSphere(transform.position, distance);
//        foreach(Collider collider in colliders)
//        {
//            if(collider.gameObject == gameObject)
//            {
//                continue;
//            }
//            if (tagName == "" || collider.CompareTag(tagName))
//            {

//                gameObjects.Add(collider.gameObject);

//                // calculate angle from transform forward vector to direction of game object 
//                Vector3 direction = (collider.transform.position - transform.position).normalized;

//    float cos = Vector3.Dot(transform.forward, direction);
//    float angle = Mathf.Acos(cos) * Mathf.Rad2Deg;

//                //< angle is less than or equal to maximum angle
//                if (angle <= maxAngle) 
//                {
//                    //< add collider to result >
//                    gameObjects.Add((GameObject) collider.gameObject);
//                }
//            }

//        }

//        gameObjects.Sort(CompareDistance);

//return gameObjects.ToArray();

public int CompareDistance(GameObject a, GameObject b)
    {
        float squaredRangeA = (a.transform.position - transform.position).sqrMagnitude;
        float squaredRangeB = (b.transform.position - transform.position).sqrMagnitude;
        return squaredRangeA.CompareTo(squaredRangeB);
    }
}
