using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LimitedDeepSearch : SearchAlgorithm {

	private Stack<SearchNode> openQueue = new Stack<SearchNode> ();
	private HashSet<object> closedSet = new HashSet<object> ();
	public int deep_limit = 10;
	protected override void Begin () 
	{
		SearchNode start = new SearchNode (problem.GetStartState (), 0);
		openQueue.Push (start);
	}
		

	protected override void Step()
	{
		if (openQueue.Count > 0)
		{
			SearchNode cur_node = openQueue.Pop();
			closedSet.Add (cur_node.state);

			if (problem.IsGoal (cur_node.state)) {
				solution = cur_node;
				finished = true;
				running = false;
			} else {
				
				if(cur_node.depth<deep_limit){
					Successor[] sucessors = problem.GetSuccessors (cur_node.state);
					foreach (Successor suc in sucessors) {
						//if (!closedSet.Contains (suc.state)) {
							SearchNode new_node = new SearchNode (suc.state, suc.cost + cur_node.g, suc.action, cur_node);
							openQueue.Push (new_node);
						//}
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
