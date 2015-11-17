using UnityEngine;
using System.Collections;

public class WinScript : MonoBehaviour {

	public string nextLevel;

	void Win() {
		Application.LoadLevel(nextLevel);
	}
	void JumperHit() {
		Win ();
	}
	void CrusherHit() {
		Win ();
	}
	void CloudHit() {
		Win ();
	}


}
