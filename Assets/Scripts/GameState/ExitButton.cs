using UnityEngine;
using System.Collections;

public class ExitButton : MonoBehaviour {

	public GUIStyle myStyle;

	void OnGUI(){
		if (GUI.Button (new Rect ((Screen.width / 2) + (Screen.width/3), (Screen.height / 2) + (Screen.height/3), 160, 40), "Main Menu", myStyle)) {
			Application.LoadLevel(1);
		}
	}
}
