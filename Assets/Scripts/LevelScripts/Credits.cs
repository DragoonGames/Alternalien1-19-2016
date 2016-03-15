using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour {

	public int speed = 2;                                                   // Speed at which the words move

    void Update() {
		transform.Translate(Vector3.down * Time.deltaTime * speed);         // Move down with time and speed factors.

		if (transform.position.y < -50 || Input.anyKeyDown)                 // When it reaches the bottom, or any key is pressed
			Application.LoadLevel(1);                                       // Load main menu.
    }
}
