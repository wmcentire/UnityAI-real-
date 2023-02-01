using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
	public State currentState { get; set; }

	private Dictionary<string, State> states = new Dictionary<string, State>();

	public void Update()
	{
		currentState?.OnUpdate();
	}

	public void StartState(string name)
	{
		State newState = states[name];
		if (newState == null || newState == currentState) return;
	}

	public void AddState(State state)
	{

	}
}
