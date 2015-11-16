using UnityEngine;
using System.Collections;

public class SceneLoader : MonoBehaviour {
    public string levelSelect;
    public string nextLevel;
    public string mainMenu;
	public Camera mainCamera;
	private bool win = false;
    /*void OnTriggerEnter2D(Collider2D other)
    {
        Application.LoadLevel(nextLevel);
    }
    */

	void OnTriggerEnter2D(Collider2D other)
	{
        if (other.gameObject.tag == "Player")
		    win = true;
	}
	
	void OnGUI()
	{
		//print ("Render");

		if (win) {
			if (GUI.Button (new Rect ((mainCamera.pixelWidth - 100)/2, (mainCamera.pixelHeight - 120)/2, 100, 20), "Next Level")){
				Application.LoadLevel(nextLevel);
			}
			if (GUI.Button (new Rect((mainCamera.pixelWidth - 100)/2, (mainCamera.pixelHeight - 60)/2, 100, 20), "Level Select")){
				Application.LoadLevel(levelSelect);
			}
			if (GUI.Button (new Rect ((mainCamera.pixelWidth - 100)/2, (mainCamera.pixelHeight - 0)/2, 100, 20), "Main Menu")){
				Application.LoadLevel(mainMenu);
			}
			
		}
	}
}