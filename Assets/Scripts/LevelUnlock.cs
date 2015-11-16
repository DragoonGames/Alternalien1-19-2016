using UnityEngine;
using System.Collections;

public class LevelUnlock : MonoBehaviour {

	public Camera mainCamera;
	public string[] levels;
	bool label = false;
	void OnGUI()
	{
		if (GUI.Button (new Rect ((mainCamera.pixelWidth - 100) / 2, (mainCamera.pixelHeight - 180) / 2, 100, 20), "Level 1")) {
			Application.LoadLevel(levels[0]);
		}
		if (GUI.Button (new Rect ((mainCamera.pixelWidth - 100) / 2, (mainCamera.pixelHeight - 130) / 2, 100, 20), "Level 2")) {
			if (PlayerPrefs.HasKey("unlockLevel2")) {
				print ("Has unlockLevel2 key");
				//Application.LoadLevel (levels[1]);
			}
			else 
			{
				label = true;
			}
		}
		if (GUI.Button (new Rect ((mainCamera.pixelWidth - 100) / 2, (mainCamera.pixelHeight - 80) / 2, 100, 20), "Level 3")) {
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
