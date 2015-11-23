using UnityEngine;
using System.Collections;

public class GUISample : MonoBehaviour
{
	//public Texture2D image;
	public string text = "";
	public GUISkin mySkin;
	int buttonW = 100;
	int buttonH = 16;
	
	void OnGUI() {
		GUI.skin = mySkin;
		if (gameObject.tag == "alienData") {
			print ("still alive");
			if (GUI.Button (new Rect ((Screen.width / 2) - (buttonW / 2), (Screen.height / 2) - 20, buttonW, buttonH), text))
				print ("button works");
		}
		if (gameObject.tag == "personnel") {
			if (GUI.Button (new Rect ((Screen.width / 2) - (buttonW / 2), (Screen.height / 2) , buttonW, buttonH), text))
				print ("second button works");
		}
	} 
}
