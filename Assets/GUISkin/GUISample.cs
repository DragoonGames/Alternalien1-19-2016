using UnityEngine;
using System.Collections;

public class GUISample : MonoBehaviour
{
	//public Texture2D image;
	public string text = "";
	public GUISkin mySkin;
	float buttonW = Screen.width/8;
	float buttonH = Screen.height/20;
	//public int fontSize;
	
	void OnGUI() {
		GUI.skin = mySkin;
		mySkin.button.fontSize = (int)(buttonH / 1.5);
		if (Screen.width > 1000)
			buttonW = Screen.width/5;

		if (gameObject.tag == "AlienData") {
			if (GUI.Button (new Rect ((Screen.width / 2) - (buttonW / 2), (Screen.height / 2) - (buttonH * 2), buttonW, buttonH), text))
				Application.LoadLevel(1);
		}
		if (gameObject.tag == "Personnel") {
			if (GUI.Button (new Rect ((Screen.width / 2) - (buttonW / 2), (Screen.height / 2), buttonW, buttonH), text))
				Application.LoadLevel(2);
		}
		if (gameObject.tag == "LevelSelect") {
			if (GUI.Button (new Rect ((Screen.width / 2) - (buttonW / 2), (Screen.height / 2) + (buttonH * 2), buttonW, buttonH), text))
				Application.LoadLevel(3);
		}
		if (gameObject.tag == "Exit") {
			if (GUI.Button (new Rect ((Screen.width / 2) - (buttonW / 4), (Screen.height / 3) + Screen.height / 2, buttonW/2, buttonH), text))
				Application.Quit();

		}
	} 
}
