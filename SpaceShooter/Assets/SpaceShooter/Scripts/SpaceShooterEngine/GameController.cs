using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;




public class GameController : MonoBehaviour
{

	public GameObject[] hazards;
	public Vector3 spawnValues;
	private float waveWait = 4;

	public Level lvl;
	public int level;

	public bool gameOver;
	public int score;

	public int nextSpawnTimeIndex;
	private float nextSpawnTime;
	private int nextSpawn;
	private int nextEnemy;

	public int count_moves = 0;

	public bool running = false;
	public bool stageCleared = false;
	public int id = 0;// id for the simulator

	void Start ()
	{
		// init properties
		gameOver = false;
	}

	public void initGame(){
		score = 0;
		count_moves = 0;
		running = true;
	
		nextSpawnTime = Time.fixedTime;
		nextSpawn = 0;
		nextEnemy = 0;
		nextSpawnTimeIndex = 0;
	}


	void FixedUpdate(){
		
		if (running && !gameOver) {
			count_moves++;
			if (nextSpawn < lvl.hazardCount && Time.fixedTime >= nextSpawnTime) {
				SpawnNext ();
				if (nextSpawn < lvl.hazardCount) {
					nextSpawnTime += lvl.timeBetweenHazard[nextSpawnTimeIndex++];
				} else {
					nextSpawnTime += waveWait;
				}
			}

			if (nextSpawn == lvl.hazardCount && Time.fixedTime >= nextSpawnTime) {
				count_moves = 0;
				running = false;
				stageCleared = true;
			}
		}
	}



	private void SpawnNext()
	{
		GameObject hazard = hazards [lvl.levelHazards[nextSpawn]];
		Vector3 spawnPosition = new Vector3 (
			transform.position.x + lvl.levelHazardsX[nextSpawn],
			transform.position.y + spawnValues.y,
			transform.position.z + spawnValues.z);
		
		Quaternion spawnRotation = Quaternion.identity;
	
		GameObject created = Instantiate (hazard, spawnPosition, spawnRotation, transform.parent);
		if (lvl.levelHazards [nextSpawn] == 3) {
			EvasiveManeuver enemy = created.GetComponent<EvasiveManeuver> ();
			enemy.evadeCommands = lvl.enemyCommands [nextEnemy++];
		}
		nextSpawn++;
	}


	public void AddScore (int newScoreValue)
	{
		score += newScoreValue;
	}

	public int GetScore()
	{
		return score;
	}

	public void GameOver ()
	{
		running = false;
		gameOver = true;
	}
}