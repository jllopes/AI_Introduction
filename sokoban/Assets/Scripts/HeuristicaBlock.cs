using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HeuristicaBlock : SearchAlgorithm {
	//looks for the remaining crates and calculates the closest distance to the closest goal from the crate
	private PriorityQueue priorityQueue = new PriorityQueue ();
	private HashSet<object> closedSet = new HashSet<object> ();
	private int no;

	protected override void Begin () 
	{
		SearchNode start = new SearchNode (problem.GetStartState (), 0);
		priorityQueue.Add (start, problem.getGoal(problem.GetStartState()));
	}


	protected override void Step()
	{
		if (priorityQueue.Count > 0) {

			SearchNode cur_node = priorityQueue.PopFirst (); // Nó <- RetiraListaOrdenada(l_nós)
			closedSet.Add (cur_node.state);

			if (problem.IsGoal (cur_node.state)) { // Se TesteObjetivo(nó) Então
				solution = cur_node; // Devolve nó
				finished = true;
				running = false;
			} else { // Senão
				Successor[] sucessors = problem.GetSuccessors (cur_node.state);
				foreach (Successor suc in sucessors) {
					if (!closedSet.Contains (suc.state)) {
						//print ("not null\n");

						if (((SokobanProblem)problem).check_lock (suc.state)) {
							SearchNode new_node = new SearchNode (suc.state, suc.cost + cur_node.g, ((SokobanProblem)problem).GetClosestCrate (suc.state), suc.action, cur_node);
							priorityQueue.Add (new_node, (int)new_node.f); // InsereListaOrdenada(l_nós, g+h(Expansão(nó,Operadores(problema))))

							}
						}
					}
				}
			} else { // Se VaziaListaOrdenada(l_nís) Então
				finished = true; // Devolve falha
				running = false;
			}
		}
	}