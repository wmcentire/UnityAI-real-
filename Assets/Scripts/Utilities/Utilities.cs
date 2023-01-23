using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Utilities : MonoBehaviour
{
   public static Vector3 Wrap(Vector3 v, Vector3 min, Vector3 max)
    {
        Vector3 result = v;

        if (result.x > max.x) { result.x = min.x; }
        else if (result.x < min.x) { result.x = max.x; }

        if (result.y > max.y) { result.y = min.y; }
        else if (result.y < min.y) { result.y = max.y; }

        if (result.z > max.z) { result.z = min.z; }
        else if (result.z < min.z) { result.z = max.z; }

        return result;
    }

    public static Vector3 ClampMagnitude(Vector3 v, float min, float max)
    {
        return v.normalized * Mathf.Clamp(v.magnitude, min, max);
    }

    public static Vector3[] GetDirectionsInCircle(int num, float angle)
    {
        List<Vector3> result = new List<Vector3>();

        // if odd number, set first direction as forward (0, 0, 1) 
        if (num % 2 != 0) result.Add(Vector3.forward);

        // compute the angle between rays 
        float angleOffset = angle/(num-1);
        // add the +/- directions around the circle 
        for (int i = 0; i < num / 2; i++)
        {
            result.Add(Quaternion.AngleAxis(+angleOffset * i, Vector3.up) * Vector3.forward);
            result.Add(Quaternion.AngleAxis(-angleOffset * i, Vector3.up) * Vector3.forward);
        }

        return result.ToArray();
    }
}

[ExecuteInEditMode]
public class NavNodeEditor : MonoBehaviour
{
    [SerializeField] GameObject navNodePrefab;
    [SerializeField] LayerMask layerMask;

    private Vector3 position = Vector3.zero;
    private Vector3 nodeCameraDirection = Vector3.zero;
    private Vector3 activeCameraDirection = Vector3.zero;
    private bool spawnable = false;
    private NavNode navNode = null;
    private NavNode activeNavNode = null;

    private void OnEnable()
    {
        if (!Application.isEditor)
        {
            Destroy(this);
        }
        SceneView.duringSceneGui += OnScene;
    }

    void OnScene(SceneView scene)
    {
        Event e = Event.current;

        // get scene mouse position
        Vector3 mousePosition = e.mousePosition;
        mousePosition.y = scene.camera.pixelHeight - mousePosition.y * EditorGUIUtility.pixelsPerPoint;
        mousePosition.x *= EditorGUIUtility.pixelsPerPoint;

        Ray ray = scene.camera.ScreenPointToRay(mousePosition);
        // check mouse over spawn/nav layer
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 100, layerMask))
        {
            position = hitInfo.point;

            if (hitInfo.collider.gameObject.TryGetComponent<NavNode>(out navNode))
            {
                if (activeNavNode == null)
                {
                    Selection.activeGameObject = navNode.gameObject;
                }
                spawnable = false;
            }
            else spawnable = true;
        }
        else
        {
            navNode = null;
            spawnable = false;
        }

        // if spawnable and mouse pressed, create nav node
        if (e.type == EventType.KeyDown && e.keyCode == KeyCode.Z)
        {
            if (spawnable && navNode == null && activeNavNode == null) Instantiate(navNodePrefab, position, Quaternion.identity, transform);
            if (navNode != null && activeNavNode == null)
            {
                activeNavNode = navNode;
                navNode = null;
            }
        }
        // add connection to active nav node
        if (e.type == EventType.KeyUp && e.keyCode == KeyCode.Z)
        {
            if (activeNavNode != null && navNode != null && activeNavNode != navNode)
            {
                if (!activeNavNode.neighbors.Exists(n => n == navNode))
                {
                    activeNavNode.neighbors.Add(navNode);
                }
            }
            activeNavNode = null;
        }

        // delete nav node
        if (e.type == EventType.KeyDown && e.keyCode == KeyCode.X)
        {
            if (navNode != null)
            {
                DestroyImmediate(navNode.gameObject);
            }
        }

        activeCameraDirection = (activeNavNode != null) ? (scene.camera.transform.position - activeNavNode.transform.position).normalized : Vector3.zero;
        nodeCameraDirection = (navNode != null) ? (scene.camera.transform.position - navNode.transform.position).normalized : Vector3.zero;
    }

    private void OnDrawGizmos()
    {
        if (spawnable && navNode == null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(position, 1);
        }
        if (navNode != null && navNode != activeNavNode)
        {
            Gizmos.DrawIcon(navNode.transform.position + nodeCameraDirection, "node.png", true, Color.green);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(navNode.gameObject.transform.position, navNode.radius);
        }
        if (activeNavNode != null)
        {
            Gizmos.DrawIcon(activeNavNode.transform.position + activeCameraDirection, "node.png", true, Color.red);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(activeNavNode.gameObject.transform.position, activeNavNode.radius * 1.5f);
            Gizmos.DrawLine(activeNavNode.gameObject.transform.position, position);
        }

    }
}
