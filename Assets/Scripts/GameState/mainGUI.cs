using UnityEngine;
using System.Collections;

public class mainGUI : MonoBehaviour {

	public string levelSelect;
	public string nextLevel;
	public string mainMenu;
	public Camera mainCamera;

	public bool optionButtonPressed = false;
	// Use this for initialization

	void OnGUI()
	{
		if (!optionButtonPressed) {
			if (GUI.Button (new Rect ((mainCamera.pixelWidth - 100) / 2, (mainCamera.pixelHeight - 180) / 2, 100, 20), "Next Level")) {
				Application.LoadLevel (nextLevel);
			}
			if (GUI.Button (new Rect ((mainCamera.pixelWidth - 100) / 2, (mainCamera.pixelHeight - 120) / 2, 100, 20), "Level Select")) {
				Application.LoadLevel (levelSelect);
			}
			if (GUI.Button (new Rect ((mainCamera.pixelWidth - 100) / 2, (mainCamera.pixelHeight - 60) / 2, 100, 20), "Character Bios")) {
				//Character Bios
			}
            if (GUI.Button(new Rect((mainCamera.pixelWidth - 100) / 2, (mainCamera.pixelHeight - 0) / 2, 100, 20), "Options")) { 
                optionButtonPressed = true;
            }
        }
        else if (optionButtonPressed) {
			if (GUI.Button (new Rect ((mainCamera.pixelWidth - 100) / 2, (mainCamera.pixelHeight - 180) / 2, 100, 20), "Video Settings")) {
				//Video Settings
			}
			if (GUI.Button (new Rect ((mainCamera.pixelWidth - 100) / 2, (mainCamera.pixelHeight - 120) / 2, 100, 20), "Audio Settings")) {
				//Audio Settings
			}
			if (GUI.Button (new Rect ((mainCamera.pixelWidth - 100) / 2, (mainCamera.pixelHeight - 60) / 2, 100, 20), "Controls")) {
				
			}
			if (GUI.Button (new Rect ((mainCamera.pixelWidth - 100) / 2, (mainCamera.pixelHeight - 0) / 2, 100, 20), "Back")) {
				optionButtonPressed = false;
			}
			//PlayerPrefs.SetInt("LevelUnlocked")
		}
	}
}
