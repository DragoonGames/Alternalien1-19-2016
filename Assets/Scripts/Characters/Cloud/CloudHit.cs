using UnityEngine;
using System.Collections;

public class CloudHit : MonoBehaviour {
	void OnCollisionEnter2D(Collision2D c) {
		c.gameObject.SendMessage ("CloudHit");
	}
}
