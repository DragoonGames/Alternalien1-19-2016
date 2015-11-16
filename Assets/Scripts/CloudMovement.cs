using UnityEngine;
using System.Collections;

public class CloudMovement : MonoBehaviour {
	public float maxSpeed = 10f;
	private bool isActive = false;
	
	void FixedUpdate () {
		if (isActive) {
			float horizontal = Input.GetAxis ("Horizontal");
			float vertical = Input.GetAxis ("Vertical");

			GetComponent<Rigidbody2D>().velocity = new Vector2 (horizontal * maxSpeed, vertical * maxSpeed);
		}
	}

	void SetActive() {
		isActive = true;
	}
	void SetInactive() {
		isActive = false;
	}
}
