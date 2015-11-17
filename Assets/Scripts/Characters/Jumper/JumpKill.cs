using UnityEngine;
using System.Collections;

public class JumpKill : MonoBehaviour {
	void JumperHit() {
		Destroy (gameObject);
	}
}
