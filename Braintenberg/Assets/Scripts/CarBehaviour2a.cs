using UnityEngine;
using System.Collections;

public class CarBehaviour2a : CarBehaviour {
	
	void Update()
	{
		//Read sensor values
		float leftSensor = LeftLD.GetLinearOutput ();
		float rightSensor = RightLD.GetLinearOutput ();
		//float leftBlockSensor = LeftBD.GetLinearOutput ();
		//float rightBlockSensor = RightBD.GetLinearOutput ();
		float speed;
		//Calculate target motor values
		switch (escolha) {

		//Caso linear 1
		case 1:
				m_LeftWheelSpeed = (leftSensor /*+ leftBlockSensor*/) * MaxSpeed;
				m_RightWheelSpeed = (rightSensor /*+ rightBlockSensor*/) * MaxSpeed;
			break;

		//Caso linear 2
		case 2:
			//Sensor Esquerda
			if (leftSensor > minLimiar && leftSensor < maxLimiar) {
				m_LeftWheelSpeed = (leftSensor /*+ leftBlockSensor*/) * MaxSpeed;
			} else {
				m_LeftWheelSpeed = 0;
			}

			//Sensor Direita
			if (rightSensor > minLimiar && rightSensor < maxLimiar) {
				m_RightWheelSpeed = (rightSensor /*+ rightBlockSensor*/) * MaxSpeed;
			} else {
				m_RightWheelSpeed = 0;
			}
			break;

		//Caso linear 3
		case 3:
			//Sensor Esquerda
			if (leftSensor > minLimiar && leftSensor < maxLimiar) {
				float y = (leftSensor /*+ leftBlockSensor*/);
				if (y > botLimit && y < topLimit) {
					m_LeftWheelSpeed = y;
				} else if (y < botLimit) {
					m_LeftWheelSpeed = botLimit;
				} else if (y > topLimit) {
					m_LeftWheelSpeed = topLimit;
				}
			} else {
				m_LeftWheelSpeed = botLimit;
			}
				
			if (rightSensor > minLimiar && rightSensor < maxLimiar) {
				float y = (rightSensor /*+ leftBlockSensor*/);
				if (y > botLimit && y < topLimit) {
					m_RightWheelSpeed = y;
				} else if (y < botLimit) {
					m_RightWheelSpeed = botLimit;
				} else if (y > topLimit) {
					m_RightWheelSpeed = topLimit;
				}
			} else {
				m_RightWheelSpeed = botLimit;
			}
			m_RightWheelSpeed = m_RightWheelSpeed * MaxSpeed;
			m_LeftWheelSpeed = m_LeftWheelSpeed * MaxSpeed;

			break;

		case 7:
			//Sensor Esquerda
			if (leftSensor > minLimiar && leftSensor < maxLimiar) {
				float y  = (1 - leftSensor);
				if (y > botLimit && y < topLimit) {
					m_LeftWheelSpeed = y;
				} else if (y < botLimit) {
					m_LeftWheelSpeed = botLimit;
				} else if (y > topLimit) {
					m_LeftWheelSpeed = topLimit;
				}
			} else {
				m_LeftWheelSpeed = botLimit;
			}

			if (rightSensor > minLimiar && rightSensor < maxLimiar) {
				float y = (1 - rightSensor);
				if (y > botLimit && y < topLimit) {
					m_RightWheelSpeed = y;
				} else if (y < botLimit) {
					m_RightWheelSpeed = botLimit;
				} else if (y > topLimit) {
					m_RightWheelSpeed = topLimit;
				}
			} else {
				m_RightWheelSpeed = botLimit;
			}
			m_RightWheelSpeed = m_RightWheelSpeed * MaxSpeed;
			m_LeftWheelSpeed = m_LeftWheelSpeed * MaxSpeed;

			break;
		//Caso gaussiana 1 (Fórmula encontrada na Wikipédia)
		case 4:
			m_LeftWheelSpeed = Mathf.Exp ((-Mathf.Pow (leftSensor - media, 2) / (2.0f * Mathf.Pow (desvio, 2)))) * MaxSpeed;
			m_RightWheelSpeed = Mathf.Exp ((-Mathf.Pow (rightSensor - media, 2) / (2.0f * Mathf.Pow (desvio, 2)))) * MaxSpeed;
			print ("left "+ m_LeftWheelSpeed+" right "+ m_RightWheelSpeed+"\n"); 
			break;

		//Caso gaussiana 2 - limite esquerda e direita
		case 5:
			//Sensor Esquerda
			if (leftSensor > minLimiar && leftSensor < maxLimiar) {
				m_LeftWheelSpeed = Mathf.Exp ((-Mathf.Pow (leftSensor - media, 2) / (2.0f * Mathf.Pow (desvio, 2)))) * MaxSpeed;
			} else {
				m_LeftWheelSpeed = 0;
			}

			//Sensor Direita
			if (rightSensor > minLimiar && rightSensor < maxLimiar) {
				m_RightWheelSpeed = Mathf.Exp ((-Mathf.Pow (rightSensor - media, 2) / (2.0f * Mathf.Pow (desvio, 2)))) * MaxSpeed;
			} else {
				m_RightWheelSpeed = 0;
			}
			break;

		//Caso gaussiana 3
		case 6:
			//Sensor Esquerda
			if (leftSensor < minLimiar || leftSensor > maxLimiar) {
				leftSensor = minLimiar;
			}
			//Cálculo da gaussiana
			speed = Mathf.Exp (-Mathf.Pow (leftSensor - media, 2) / (2 * Mathf.Pow (desvio, 2)));
			if (speed > topLimit) {
				speed = topLimit;
			} else if (speed < botLimit) {
				speed = botLimit;
			}
			m_RightWheelSpeed = speed * MaxSpeed;

			//Sensor Direita
			if (rightSensor < minLimiar || rightSensor > maxLimiar) {
				rightSensor = minLimiar;
			}
			//Cálculo da gaussiana
			speed = Mathf.Exp (-Mathf.Pow (rightSensor - media, 2) / (2 * Mathf.Pow (desvio, 2)));
			if (speed > topLimit) {
				speed = topLimit;
			} else if (speed < botLimit) {
				speed = botLimit;
			}
			m_LeftWheelSpeed = speed * MaxSpeed;

			break;
			//Caso gaussiana ->elipse
		}
		if (troca == true) {
			float temp = m_RightWheelSpeed;
			m_RightWheelSpeed = m_LeftWheelSpeed;
			m_LeftWheelSpeed = temp;
		}
	}
}
