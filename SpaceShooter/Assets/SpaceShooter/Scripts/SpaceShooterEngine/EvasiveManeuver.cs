using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EvasiveManeuver : MonoBehaviour
{
	public Boundary boundary;
	public float tilt;
	public float dodge;
	public float smoothing;

	private float currentSpeed;
	private float targetManeuver;
	public float[] evadeCommands;
	private float nextCommandTime;
	private int nextCommand;

	void Start ()
	{
		currentSpeed = GetComponent<Rigidbody> ().velocity.z;
		boundary.xMin = transform.parent.position.x + boundary.xMin;
		boundary.xMax = transform.parent.position.x + boundary.xMax;
		boundary.zMin = transform.parent.position.z + boundary.zMin;
		boundary.zMax = transform.parent.position.z + boundary.zMax;

		targetManeuver = 0.0f;
		nextCommand = 0;
		nextCommandTime = Time.fixedTime + evadeCommands [nextCommand++];
	}

	void FixedUpdate ()
	{
		if (nextCommand < evadeCommands.Length) {
			if (Time.fixedTime >= nextCommandTime) {
				if (targetManeuver == 0.0f) {
					targetManeuver = evadeCommands [nextCommand++] * -Mathf.Sign (transform.localPosition.x);
					nextCommandTime += evadeCommands [nextCommand++];
				} else {
					targetManeuver = 0.0f;
					nextCommandTime += evadeCommands [nextCommand++];
				}
			}
		}

		float newManeuver = Mathf.MoveTowards (GetComponent<Rigidbody> ().velocity.x, targetManeuver, smoothing * Time.fixedDeltaTime);
		GetComponent<Rigidbody> ().velocity = new Vector3 (newManeuver, 0, currentSpeed);
		GetComponent<Rigidbody> ().position = new Vector3 (
			Mathf.Clamp (GetComponent<Rigidbody> ().position.x, boundary.xMin, boundary.xMax), 
			GetComponent<Rigidbody> ().position.y, 
			Mathf.Clamp (GetComponent<Rigidbody> ().position.z, boundary.zMin, boundary.zMax)
		);

		GetComponent<Rigidbody> ().rotation = Quaternion.Euler (0, 0, GetComponent<Rigidbody> ().velocity.x * -tilt);
		
	}
}