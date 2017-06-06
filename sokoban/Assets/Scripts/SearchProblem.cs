﻿using UnityEngine;
using System.Collections;


public struct Successor
{
	public object state;
	public float cost;
	public Action action;


	public Successor(object state, float cost, Action a)
	{
		this.state = state;
		this.cost = cost;
		this.action = a;
	}
}

public delegate float Heuristic(object state);

public interface ISearchProblem
{
	object GetStartState ();
	bool IsGoal (object state);
	Successor[] GetSuccessors (object state);

	int GetVisited ();
	int GetExpanded ();

	int getGoal(object state);
	float manhaGetGoal(object state);
	float manhaGetPlayer(object state);
	float euclGetGoal(object state);
	float euclGetPlayer(object state);
}

