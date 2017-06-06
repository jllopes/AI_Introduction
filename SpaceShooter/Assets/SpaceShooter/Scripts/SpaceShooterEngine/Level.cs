using UnityEngine;
using System.Collections;

public class Level 
{

	public int hazardCount;
	public float[] timeBetweenHazard;
	public int[] levelHazards;
	public float[] levelHazardsX;
	public float[][] enemyCommands;

	public void load(int i){
		switch (i){
		case 0: // test !
			hazardCount = 3;
			levelHazards = new int[] { 2, 0, 1};
			levelHazardsX = new float[] { -1.0f, 1f, -3.2f };
			timeBetweenHazard = new float[] { 1, 2 , 3};
			break;
		case 1:
			hazardCount = 20;
			levelHazards = new int[] {2, 0, 1, 2, 0, 0, 0, 0, 0, 3, 3, 0, 0, 1, 3, 1, 2, 3, 3, 2};
			levelHazardsX = new float[] {-1.0f, -3.2f, -1.8f, 0.0f, 4.4f, -1.7f, -3.2f, 2.3f, 2.7f, -0.6f, -2.3f, -3.2f, 2.3f, 5.7f, 0.3f, -1.0f, 0.0f, 0.5f, -4.5f, 0.0f};
			enemyCommands = new float[][]
			{
				new float[] {0.8f,1.0f,1.8f,1.0f,2.7f,1.9f,1.8f,1.3f,1.3f,1.1f}, 
				new float[] {0.69f,4.3f,1.3f,1.7f,4.2f,1.0f,1.0f,2.4f,1.7f,1.1f},
				new float[] {0.67f,4.7f,1.4f,1.3f,1.1f,1.2f,1.7f,4.6f,1.2f,1.6f},
				new float[] {0.98f,4.9f,1.7f,1.4f,2.9f,1.7f,1.5f,1.7f,1.5f,1.8f},
				new float[] {0.75f,2.9f,1.4f,1.9f,4.9f,2.0f,1.1f,2.4f,1.8f,1.9f}
			};

			timeBetweenHazard = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
			break;
		case 2:
			hazardCount = 20;
			levelHazards = new int[] {2, 0, 1, 2, 0, 0, 0, 0, 0, 3, 3, 0, 0, 1, 3, 1, 2, 3, 3, 2};
			levelHazardsX = new float[] {-1.0f, -3.2f, -1.8f, 0.0f, 4.4f, -1.7f, -3.2f, 2.3f, 2.7f, -0.6f, -2.3f, -3.2f, 2.3f, 5.7f, 0.3f, -1.0f, 0.0f, 0.5f, -4.5f, 0.0f};
			enemyCommands = new float[][]
			{
				new float[] {0.8f,1.0f,1.8f,1.0f,2.7f,1.9f,1.8f,1.3f,1.3f,1.1f}, 
				new float[] {0.69f,4.3f,1.3f,1.7f,4.2f,1.0f,1.0f,2.4f,1.7f,1.1f},
				new float[] {0.67f,4.7f,1.4f,1.3f,1.1f,1.2f,1.7f,4.6f,1.2f,1.6f},
				new float[] {0.98f,4.9f,1.7f,1.4f,2.9f,1.7f,1.5f,1.7f,1.5f,1.8f},
				new float[] {0.75f,2.9f,1.4f,1.9f,4.9f,2.0f,1.1f,2.4f,1.8f,1.9f}
			}; 
			timeBetweenHazard = new float[] { 0.4f, 0.4f, 0.4f, 0.4f, 0.4f, 0.4f, 0.4f, 0.4f, 0.4f, 0.4f, 0.4f, 0.4f, 0.4f, 0.4f, 0.4f, 0.4f, 0.4f, 0.4f, 0.4f, 0.4f };
			break;
		}



	}


	public int calcNumberOfMoves(){
		// calculate timeBetweenHazard to give us how many moves 
		float moves = 0;
		foreach (float time in timeBetweenHazard) {
			moves += time * 50;
		}
		moves += 50 * 5; // with move speed -5 it takes aprox 5 secs to get out of reach of the player. If we change the speed of enemies this has to be adjusted
		//Debug.Log ("Calculated size: " + moves);
		return (int)moves;
	}

}