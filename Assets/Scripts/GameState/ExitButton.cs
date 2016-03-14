using UnityEngine;
using System.Collections;

public class ExitButton : MonoBehaviour {

	public GUIStyle myStyle;                        // Use same style with each GUI.

	void OnGUI(){
		if (GUI.Button (new Rect ((Screen.width / 2) + (Screen.width/3), (Screen.height / 2) + (Screen.height/3), 160, 40), "Main Menu", myStyle)) {        // Create Main menu button....
			Application.LoadLevel(1);                                                                                                                       // If clikced, load Main Menu
		}
	}
}
