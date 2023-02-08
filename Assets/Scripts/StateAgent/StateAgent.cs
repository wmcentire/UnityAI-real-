using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAgent : Agent
{

    public StateMachine stateMachine = new StateMachine();
    public GameObject[] perceived;

    // condition parameters
    public FloatRef health = new FloatRef();
    public FloatRef timer = new FloatRef();
    public FloatRef enemyDistance = new FloatRef();

    public BoolRef enemySeen = new BoolRef();
    public BoolRef animationDone = new BoolRef();
    public BoolRef atDestination = new BoolRef();

    private Camera mainCamera;
    void Start()
    {
        mainCamera = Camera.main;

        health.value = 100;

        stateMachine.AddState(new IdleState(this));
        stateMachine.AddState(new PatrolState(this));
        stateMachine.AddState(new ChaseState(this));
        stateMachine.AddState(new WanderState(this));
        stateMachine.AddState(new AttackState(this));

        // create conditions
        Condition timerExpiredCondition = new FloatCondition(timer, Condition.Predicate.LESS_EQUAL, 0);
        Condition enemySeenCondition = new BoolCondition(enemySeen, true);
        Condition enemyNotSeenCondition = new BoolCondition(enemySeen, false);
        Condition healthLowCondition = new FloatCondition(health, Condition.Predicate.LESS_EQUAL, 30);
        Condition healthOkCondition = new FloatCondition(health, Condition.Predicate.GREATER, 30);
        Condition deathCondition = new FloatCondition(health, Condition.Predicate.LESS_EQUAL, 0);
        Condition animationDoneCondition = new BoolCondition(animationDone, true);
        Condition atDestinationCondition = new BoolCondition(atDestination, true);

        // create transitions
        stateMachine.AddTransition(nameof(IdleState), new Transition(new Condition[] { timerExpiredCondition }), nameof(PatrolState));
        stateMachine.AddTransition(nameof(IdleState), new Transition(new Condition[] { enemySeenCondition }), nameof(ChaseState));
        stateMachine.AddTransition(nameof(PatrolState), new Transition(new Condition[] { timerExpiredCondition }), nameof(WanderState));
        stateMachine.AddTransition(nameof(PatrolState), new Transition(new Condition[] { enemySeenCondition }), nameof(ChaseState));





        stateMachine.StartState(nameof(IdleState));
    }

    void Update()
    {
        perceived = perception.GetGameObjects();

        // update condition parameters
        enemySeen.value = (perceived.Length != 0);
        enemyDistance.value = (enemySeen) ? (Vector3.Distance(transform.position, perceived[0].transform.position)) : float.MaxValue;
        timer.value -= Time.deltaTime;
        atDestination.value = ((movement.destination - transform.position).sqrMagnitude <= 1);
        animationDone.value = (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0));

        stateMachine.Update();
        

        if(navigation.targetNode != null)
        {
            Debug.Log("move");
            movement.MoveTowards(navigation.targetNode.transform.position);
        }

        animator.SetFloat("speed", movement.velocity.magnitude);
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
