﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SearchNode
{	
	public object state;
	public float g;
	public float h;
	public float f;
	public Action action;
	public SearchNode parent;
	public int depth;

	public SearchNode(object state, float g, Action action=Action.None, SearchNode parent=null)
	{
		this.state = state;
		this.g = g;
		this.h = 0;
		this.f = g;
		this.action = action;
		this.parent = parent;
		if (parent != null) {
			this.depth = parent.depth + 1;
		} else {
			this.depth = 0;
		}
	}

	public SearchNode(object state, float g, float h, Action action=Action.None, SearchNode parent=null)
	{
		this.state = state;
		this.g = g;
		this.h = h;
		this.f = g + h;
		this.action = action;
		this.parent = parent;
		if (parent != null) {
			this.depth = parent.depth + 1;
		} else {
			this.depth = 0;
		}
	}

	public override string ToString ()
	{
		return string.Format ("[SearchNode] g={0}, h={1}, state={2}", g, h, state);
	}
}


public abstract class SearchAlgorithm : MonoBehaviour {

	public int loggerOutputFrequency = 10000;
	public int stepsPerFrame = 1000;
	[HideInInspector]public ISearchProblem problem;

	protected bool running = false;
	protected bool finished = false;
	protected SearchNode solution = null;

	void Update () {
		if(SearchAgent.batchmode){
			SearchAgent.LOGGER.Log ("Batchmode! computing until finished");
			int i = 0;
			while (!finished) {
				if (i++ % loggerOutputFrequency == 0) {
					SearchAgent.LOGGER.Log ("Visited: {0}" , problem.GetVisited ().ToString ());
					SearchAgent.LOGGER.Log ("Expanded: {0}" , problem.GetExpanded ().ToString ());
					SearchAgent.LOGGER.Flush ();
				}
				Step ();
			}
		}
		else{
			if (running && !finished) {
				for (int i = 0; i < stepsPerFrame; i++) {
					if (!finished && problem.GetExpanded () < 20000000) {
						Step ();
					} else if (problem.GetExpanded () >= 20000000) {
						SearchAgent.LOGGER.Log ("Visited: {0}" , problem.GetVisited ().ToString ());
						SearchAgent.LOGGER.Log ("Expanded: {0}" , problem.GetExpanded ().ToString ());
						SearchAgent.LOGGER.Log ("NULL");
						SearchAgent.LOGGER.Flush ();
						break;
					}
				}
			}
		}
	}

	public bool Finished()
	{
		return finished;
	}

	public List<Action> GetActionPath()
	{
		if (finished && solution != null) {
			return BuildActionPath ();
		} else {
			Debug.LogWarning ("Solution path can not be determined! Either the algorithm has not finished, or a solution could not be found.");
			return null;
		}
	}

	// These methods should be overriden on each specific search algorithm.
	protected abstract void Begin ();
	protected abstract void Step ();

	public void StartRunning()
	{
		running = true;
		Begin ();
	}

	private List<Action> BuildActionPath()
	{
		List<Action> path = new List<Action> ();
		SearchNode node = solution;

		while (node.parent != null) {
			path.Insert (0, node.action);
			node = node.parent;
		}

		return path;
	}
		
	public void setRunning(bool state){
		running = state;
	}
	public void setFinished(bool state){
		finished = state;
	}


	
}
