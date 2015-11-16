using UnityEngine;
using System.Collections;

public class AddCloud : MonoBehaviour {
	
	void JumperHit() {
		Camera.main.SendMessage("UnlockAliens");
	}
}
