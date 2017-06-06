using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tournament : SelectionMethod {
	public int tournamentSize = 50;
	public Tournament(): base() {

	}


	public override List<Individual> selectIndividuals (List<Individual> oldpop, int num)
	{
		return tournamentSelection (oldpop, num);
	}


	List<Individual> tournamentSelection(List<Individual> oldpop, int num) {

		List<Individual> selectedInds = new List<Individual> ();
		List<Individual> best = new List<Individual> ();
		Individual bestIndividual;

		int popsize = oldpop.Count;
		for (int j = 0; j < num; j++) {
			selectedInds.Clear ();
			for (int i = 0; i < tournamentSize; i++) { // Número torneios
				//make sure selected individuals are different
				Individual ind = oldpop [Random.Range (0, popsize)];
				while (selectedInds.Contains (ind)) {
					ind = oldpop [Random.Range (0, popsize)];
				}
				selectedInds.Add (ind); //we return copies of the selected individuals

			}
			bestIndividual = selectedInds [0];
			for (int i = 1; i < tournamentSize; i++) { // Número torneios
				if (selectedInds [i].Fitness > bestIndividual.Fitness)
					bestIndividual = selectedInds [i];
			}
			best.Add (bestIndividual.Clone ());
		}
		return best;
	}

}
