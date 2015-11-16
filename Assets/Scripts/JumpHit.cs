using UnityEngine;
using System.Collections;

public class JumpHit : MonoBehaviour {
	void OnCollisionEnter2D(Collision2D c) {
		c.gameObject.SendMessage ("JumperHit");
	}
}
