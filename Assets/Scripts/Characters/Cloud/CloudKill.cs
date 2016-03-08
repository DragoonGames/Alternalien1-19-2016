using UnityEngine;
using System.Collections;

public class CloudKill : MonoBehaviour {
	public GameObject obj;
	
	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.name == "cloud")
			Destroy(obj);
	}
}
