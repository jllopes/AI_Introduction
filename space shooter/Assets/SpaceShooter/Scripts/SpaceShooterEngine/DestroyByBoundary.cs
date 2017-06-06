using UnityEngine;
using System.Collections;

public class DestroyByBoundary : MonoBehaviour
{
	void OnTriggerExit (Collider other) 
	{
		// any object that trespasses the boundary is destroyed
		Destroy (other.gameObject);

	}
}