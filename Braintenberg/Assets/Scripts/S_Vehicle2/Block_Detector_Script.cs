using UnityEngine;
using System.Collections;
using System.Linq;
using System;

public class Block_Detector_Script : MonoBehaviour {

	public float strength;
	public int numObjects;

	void Start () {
		strength = 0;
		numObjects = 0;
	}

	void Update () {
		GameObject[] blocks = GetAllBlocks ();


		strength = 0;
		numObjects = blocks.Length;
		float dist = 0;

		dist = GetClosest ();

		strength = 1f / (dist + 1);


	}

	float GetClosest(){
		GameObject[] blocks = GetAllBlocks ();
		Vector3 currentPos = transform.position;
		float minDist = Mathf.Infinity;

		foreach (GameObject block in blocks)
		{
			//Calculate the minimum distance to all the object to return the closest
			float dist = Vector3.Distance(block.transform.position, currentPos);
			if (dist < minDist)
			{
				minDist = dist;
			}
		}
		return minDist;
	}

	public float GetLinearOutput()
	{
		return strength;
	}

	GameObject[] GetAllBlocks()
	{
		return GameObject.FindGameObjectsWithTag("Block");
	}
}