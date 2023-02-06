using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAgent : Agent
{

    public StateMachine stateMachine = new StateMachine();
    public GameObject[] percieved;
    public Camera mainCamera;
    void Start()
    {
        stateMachine.AddState(new IdleState(this));
        stateMachine.AddState(new PatrolState(this));
        stateMachine.AddState(new ChaseState(this));
        stateMachine.AddState(new WanderState(this));
        stateMachine.AddState(new AttackState(this));
        stateMachine.StartState(nameof(IdleState));
    }

    void Update()
    {
        percieved = perception.GetGameObjects();
        stateMachine.Update();
        

        if(navigation.targetNode != null)
        {
            Debug.Log("move");
            movement.MoveTowards(navigation.targetNode.transform.position);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            animator.SetFloat("speed", 0.5f);
        }
        else
        {
            animator.SetFloat("speed", 0);
        }
    }

    private void OnGUI()
    {
        Vector3 point = mainCamera.WorldToScreenPoint(transform.position);
        GUI.backgroundColor = Color.black;
        GUI.skin.label.alignment = TextAnchor.MiddleCenter;
        Rect rect = new Rect(0, 0, 100, 20);
        rect.x = point.x - (rect.width / 2);
        rect.y = Screen.height - point.y - rect.height - 20;
        GUI.Label(rect, stateMachine.currentState.name);
    }
}
