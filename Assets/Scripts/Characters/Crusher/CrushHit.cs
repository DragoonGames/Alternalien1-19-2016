using UnityEngine;
using System.Collections;

public class CrushHit : MonoBehaviour {
	void OnCollisionEnter2D(Collision2D c) {
		if (c.gameObject.tag == "isBreakable") {
			Destroy (c.gameObject);
		}
        if (c.gameObject.tag == "isCage" && gameObject.name != "Sandy")
        {
            Destroy(c.gameObject);
        }
	}
}
