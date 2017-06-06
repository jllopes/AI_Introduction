using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExampleIndividual : Individual {

	public int multiplier = 10;

	private int chromosomeSize;
	private int[] chromosome1;
	private bool[] chromosome2;

	public ExampleIndividual(int size, int mult): base(size)
	{
		multiplier = mult;
		chromosomeSize = (int) (size / multiplier);
		chromosome1 = new int[chromosomeSize];
		chromosome2 = new bool[chromosomeSize];
	}

	public override void Initialize ()
	{
		for (int i = 0; i < chromosomeSize; i++) 
		{
			chromosome1 [i] = Random.Range (-1, 2);
			chromosome2 [i] = (Random.Range (0, 2) == 1);
		}
			
	}

	public override void Mutate (float probability)
	{
		float random;
		for (int i = 0; i < chromosomeSize; i++) {
			random = Random.value;
			if (random < probability) {
				chromosome1 [i] = Random.Range (-1, 2);
			}
			random = Random.value;
			if (random < probability) {
				chromosome2 [i] = (Random.Range (0, 2) == 1);
			}
		}
	}
		

	public override void Crossover (Individual partner, float probability,int Ncortes)
	{
		int cromI;
		bool cromB;
		ExampleIndividual crom = (ExampleIndividual) partner;
		if (Random.value < probability) {
			List<int> nCorte = new List<int>();
			for (int i = 0; i < Ncortes; i++) {
				int aux = Random.Range (0, chromosomeSize);
				while (nCorte.Contains(aux)){
					aux = Random.Range (0, chromosomeSize);
				}
				nCorte.Add (aux);
			}
			nCorte.Sort();
			for (int i = 1; i < nCorte.Count; i+=2) {
				for (int j = nCorte [i-1]; j < nCorte [i]; j++) {
					cromI = this.chromosome1 [j];
					cromB = this.chromosome2 [j];
					this.chromosome1 [j] = crom.chromosome1 [j];
					this.chromosome2 [j] = crom.chromosome2 [j];
					crom.chromosome1 [j] = cromI;
					crom.chromosome2 [j] = cromB;
				}
			}
		}

	}

	public override void Translate ()
	{
		for (int i = 0; i < chromosomeSize; i++) 
		{
			for (int j = 0; j < multiplier; j++) 
			{
				horizontalMoves [i * multiplier + j] = chromosome1 [i];
				shots [i * multiplier + j] = chromosome2 [i];
			}
		}
	}

	public override Individual Clone ()
	{
		ExampleIndividual new_ind = new ExampleIndividual(totalSize, multiplier);

		chromosome1.CopyTo (new_ind.chromosome1, 0);
		chromosome2.CopyTo (new_ind.chromosome2, 0);

		//new_ind.Translate ();

		new_ind.fitness = 0.0f;
		new_ind.evaluated = false;

		return new_ind;
	}

	public override string ToString ()
	{
		string res = "[ExampleIndividual] Chromosome1: [";

		for (int i = 0; i < chromosomeSize; i++) {
			res += chromosome1 [i].ToString ();
			if (i != chromosomeSize - 1) {
				res += ",";
			}
		}

		res += "] Chromosome2: [";

		for (int i = 0; i < chromosomeSize; i++) {
			res += chromosome2 [i].ToString ();
			if (i != chromosomeSize - 1) {
				res += ",";
			}
		}
		res += "]";

		return res;
	}
}
