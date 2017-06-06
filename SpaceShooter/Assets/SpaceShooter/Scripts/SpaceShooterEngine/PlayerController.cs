using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary 
{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
	public float speed;
	public float tilt;
	public Boundary boundary;

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	public float moveRate;

	private float nextFire;

	public int count_moves = 0;

	public bool running = false;

	public Individual testIndividual = null;
	public bool generateRandom = false;

	private GameController gc;


	void Start()
	{
		gc = transform.parent.gameObject.GetComponentInChildren<GameController> ();
		if (gc == null) {
			Debug.Log ("Could not find Game Controller!");
		}

		count_moves = 0;
		nextFire = 0;

		boundary.xMin = transform.parent.position.x + boundary.xMin;
		boundary.xMax = transform.parent.position.x + boundary.xMax;
		boundary.zMin = transform.parent.position.z + boundary.zMin;
		boundary.zMax = transform.parent.position.z + boundary.zMax;

	}

	public void restart()
	{
		count_moves = 0;
		nextFire = 0;
	}


	void FixedUpdate ()
	{
		if (running) {
			float moveHorizontal = 0.0f; 
			float moveVertical = 0.0f; 

			if (count_moves < testIndividual.Size) {
				if (Time.time > nextFire) {
					if (testIndividual.shots [count_moves]) {
						nextFire = Time.time + (fireRate);
						Instantiate (shot, shotSpawn.position, shotSpawn.rotation, transform.parent);
						gc.AddScore (-1); // for every shot fired -1 score
					}
				}

				moveHorizontal = testIndividual.horizontalMoves [count_moves];
				moveVertical = testIndividual.verticalMoves [count_moves];

			}

			Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
			GetComponent<Rigidbody> ().velocity = movement * speed;

			GetComponent<Rigidbody> ().position = new Vector3 (
				Mathf.Clamp (GetComponent<Rigidbody> ().position.x, boundary.xMin, boundary.xMax), 
				transform.parent.position.y, 
				Mathf.Clamp (GetComponent<Rigidbody> ().position.z, boundary.zMin, boundary.zMax)
			);

			GetComponent<Rigidbody> ().rotation = Quaternion.Euler (0.0f, 0.0f, GetComponent<Rigidbody> ().velocity.x * -tilt);

			count_moves++;
		}

	}
}