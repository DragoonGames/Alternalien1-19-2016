using UnityEngine;
using System.Collections;

public class JumpHit : MonoBehaviour {

	public GameObject cage;

	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.tag == "Player")
			Destroy(cage);
	}
}
