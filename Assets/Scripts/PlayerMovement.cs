using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	public float maxSpeed = 10f;
	public float jumpSpeed = 100f;
	private bool isActive = false;

	void Jump() {
		GetComponent<Rigidbody2D>().AddForce (Vector2.up * jumpSpeed);
	}

	void FixedUpdate () {
		if (isActive) {
			float move = Input.GetAxis ("Horizontal");
			GetComponent<Rigidbody2D>().velocity = new Vector2 (move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

			if (Input.GetButtonDown ("Jump"))
				Jump ();
		}
	}
	void SetActive() {
		isActive = true;
	}
	void SetInactive() {
		isActive = false;
	}
}
