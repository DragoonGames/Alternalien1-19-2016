using UnityEngine;
using System.Collections;

public class ClearPrefs : MonoBehaviour {

	public Camera mainCamera;

	void OnGUI(){
		if (GUI.Button (new Rect ((mainCamera.pixelWidth - 100) / 2, (mainCamera.pixelHeight - 240) / 2, 100, 20), "Clear Prefs")) {
			PlayerPrefs.DeleteKey("unlockLevel2");
			print ("delete key");
		}
	}
}
