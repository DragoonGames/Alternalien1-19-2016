using UnityEngine;
using System.Collections;

public class ElevatorTrigger : MonoBehaviour {
	
	public int floor;
	public Elevator elevator;

	void OnTriggerEnter2D(Collider2D c) {
		Debug.Log (c);
		if(c.gameObject.layer != 8)
			elevator.SendMessage ("gotoFloor", floor);
	}
}
