using UnityEngine;
using System.Collections;

public class CarBehaviour1a : CarBehaviour1 {

	void Update()
	{
		//Read sensor values
		float MidlleSensor = MidlleLD.GetLinearOutput ();

		//Calculate target motor values
		m_LeftWheelSpeed = MidlleSensor * MaxSpeed;
		m_RightWheelSpeed = MidlleSensor * MaxSpeed;
	}
}
