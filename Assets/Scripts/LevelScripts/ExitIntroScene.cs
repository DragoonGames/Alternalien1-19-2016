using UnityEngine;
using System.Collections;

public class ExitIntroScene : MonoBehaviour {

	void Start(){
		StartCoroutine (EndIntro());
	}

	IEnumerator EndIntro(){
		print ("Intro Starts");
		yield return new WaitForSeconds (3);
		Application.LoadLevel(1);
	}
}
