using UnityEngine;
using System.Collections;

public class CrushHit : MonoBehaviour {
	void OnCollisionEnter2D(Collision2D c) {
		print ("Destroy");
		if (c.gameObject.tag == "isBreakable") {
			Destroy (c.gameObject);
		}
	}
}
