using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcuraSofrega : SearchAlgorithm {
	private PriorityQueue openList = new PriorityQueue ();
	private HashSet<object> closedSet = new HashSet<object> ();
	private int no; 

	protected override void Begin () 
	{
		SearchNode start = new SearchNode (problem.GetStartState (), 0);
		openList.Add (start, problem.getGoal(problem.GetStartState()));
	}


	protected override void Step()
	{
		if (openList.Count > 0)
		{
			SearchNode cur_node = openList.PopFirst();
			closedSet.Add (cur_node.state);

			if (problem.IsGoal (cur_node.state)) {
				solution = cur_node;
				finished = true;
				running = false;
			} else {
				Successor[] sucessors = problem.GetSuccessors (cur_node.state);
				foreach (Successor suc in sucessors) {
					if (!closedSet.Contains (suc.state)) {
						no = problem.getGoal (suc.state);
						SearchNode new_node = new SearchNode (suc.state, suc.cost + cur_node.g, no, suc.action, cur_node);
						openList.Add (new_node, no);
					}
				}
			}
		}
		else
		{
			finished = true;
			running = false;
		}
	}
}

