using UnityEngine;
using System.Collections;
using System.Collections.Generic;    

public class EvolutionState : MonoBehaviour
{
	public int individualSize;
	public int individualMultiplier;
	public int numGenerations;
	public int populationSize;
	public float mutationProbability;
	public float crossoverProbability;
	public int ncortes;
	public int tournamentSize;
	public string statsFilename = "log.txt";
	public int elitismAffected;
	public StatisticsLogger stats;
	public int random;
	public bool flagSelection = true;

	protected List<Individual> population;
	protected SelectionMethod selection;

	protected int evaluatedIndividuals;

	public int generation; 

	public List<Individual> Population
	{
		get
		{
			return population;
		}
	}

	public Individual Best
	{
		get
		{
			float max = float.MinValue;
			Individual max_ind = null;
			foreach (Individual indiv in population) {
				if (indiv.Fitness > max) {
					max = indiv.Fitness;
					max_ind = indiv;
				}
			}
			return max_ind;
		}
	}

	void Start()
	{
		generation = 0;
		Random.InitState (random);
		if (flagSelection == true) {
			Debug.Log ("Tournament Selection");
			selection = new Tournament ();
		}
		else{
			Debug.Log ("Random Selection");
			selection = new RandomSelection();
		}
		stats = new StatisticsLogger (statsFilename);
	}


	public virtual void InitPopulation(){
		population = new List<Individual> ();

		while (population.Count < populationSize) {
			ExampleIndividual new_ind= new ExampleIndividual (individualSize, individualMultiplier);
			new_ind.Initialize ();
			new_ind.Translate ();
			population.Add (new_ind);
		}
			
	}

	//The Step function assumes that the fitness values of all the individuals in the population have been calculated.
	public virtual void Step()
	{
		if (generation < numGenerations) 
		{
			List<Individual> new_pop;

			//Store statistics in log, and sorts population
			stats.GenLog (population, generation);
			population.Sort((x, y) => y.Fitness.CompareTo(x.Fitness));

			//List<Individual> aux = new List<Individual> ();


			int new_pop_size = populationSize - elitismAffected;
			//Select parents
			new_pop = selection.selectIndividuals (population, new_pop_size);

			//Crossover par/impar
			if (new_pop_size % 2 == 1) {
				for (int i = 0; i < new_pop_size - 1; i += 2) {
					Individual parent1 = new_pop [i];
					Individual parent2 = new_pop [i + 1];
					parent1.Crossover (parent2, crossoverProbability, ncortes);
				}
			} else {
				for (int i = 0; i < new_pop_size; i += 2) {
					Individual parent1 = new_pop [i];
					Individual parent2 = new_pop [i + 1];
					parent1.Crossover (parent2, crossoverProbability, ncortes);
				}
			}
			//Mutation and Translation 
			for (int i = 0; i < new_pop_size; i++) {
				new_pop [i].Mutate (mutationProbability);
				new_pop [i].Translate ();
			}
				
			//Select new population
			for (int i = 0; i < elitismAffected; i++) {
				new_pop.Add (population [i]);
			}

			new_pop.Sort((x, y) => y.Fitness.CompareTo(x.Fitness));
			population = new_pop;

			generation++;
		}
	}

	public void FinalLog()
	{
		stats.GenLog (population, generation);
		stats.FinalLog ();
	}

}

