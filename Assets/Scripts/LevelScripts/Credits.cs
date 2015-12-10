using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour {
	int speed = 2;

    void Update() {
		transform.Translate(Vector3.down * Time.deltaTime * speed);

		if (transform.position.y < -50 || Input.anyKeyDown)
			Application.LoadLevel(1);
    }
}
