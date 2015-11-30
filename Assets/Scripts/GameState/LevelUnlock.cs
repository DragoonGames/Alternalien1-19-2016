using UnityEngine;
using System.Collections;

public class LevelUnlock : MonoBehaviour {

	public Camera mainCamera;
	public string[] levels;
	public GUISkin mySkin;
	int buttonW = 100;
	int buttonH = 25;
	bool label = false;
	void OnGUI()
	{
		GUI.skin = mySkin;
		mySkin.button.fontSize = (int)(buttonH / 1.5);
		if (GUI.Button (new Rect ((mainCamera.pixelWidth - 100) / 2, (mainCamera.pixelHeight - 180) / 2, buttonW, buttonH), "Tutorial")) {
			Application.LoadLevel(levels[0]);
		}
		if (GUI.Button (new Rect ((mainCamera.pixelWidth - 100) / 2, (mainCamera.pixelHeight - 100) / 2, buttonW, buttonH * 2), "Second\nTutorial")) {
			if (PlayerPrefs.HasKey("unlockLevel2")) {
				Application.LoadLevel (levels[1]);
			}
			else 
			{
				label = true;
			}
		}
		if (GUI.Button (new Rect ((mainCamera.pixelWidth - 100) / 2, (mainCamera.pixelHeight + 20) / 2, buttonW, buttonH), "Level 3")) {
			if (PlayerPrefs.HasKey("unlockLevel3")) {
				print ("Has unlockLevel3 key");
				//Application.LoadLevel (levels[2]);
			}
			else 
			{
				label = true;
			}
		}
		if (label)
		{
			GUI.Label(new Rect ((mainCamera.pixelWidth - 100) / 2, (mainCamera.pixelHeight - 20) / 2, 140, 40), "This level is not\t\t  unlocked yet");
		}
	}
}
