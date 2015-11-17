using UnityEngine;
using System.Collections;

public class PauseScript : MonoBehaviour {

	public bool gamePaused = false;

	public float pauseBoxWidth = 250;
	public float pauseBoxHeight = 150;

	public float resumeButtonWidth = 250;
	public float resumeButtonHeight = 200;
	public float resumeButtonWidthOffset = 100;
	public float resumeButtonHeightOffset = 100;

	public float quitButtonWidth = 250;
	public float quitButtonHeight = 200;
	public float quitButtonWidthOffset = 100;
	public float quitButtonHeightOffset = 100;

	public Texture pauseTexture;
	public Texture resumeButtonTexture;
	public Texture quitButton;

	public GUIStyle myStyle;

	void OnGUI()
	{
		if (gamePaused) {
			/*GUI.Box(new Rect((Screen.width / 2) - (pauseBoxWidth / 2),
			                 (Screen.height / 2) - (pauseBoxHeight / 2),
			                 pauseBoxWidth, pauseBoxHeight), pauseTexture, myStyle);
                             */
			if(GUI.Button(new Rect(((Screen.width / 2) - (resumeButtonWidth / 2))-resumeButtonWidthOffset,
			                       ((Screen.height / 2) - (resumeButtonHeight / 2)) - resumeButtonHeightOffset,
			                       resumeButtonWidth, resumeButtonHeight),"Resume"))
			{
				Time.timeScale = 1;
                gamePaused = false;
			}
			if (GUI.Button(new Rect(((Screen.width / 2) - (quitButtonWidth / 2)) - quitButtonWidthOffset,
			                        ((Screen.height / 2) - (quitButtonHeight / 2)) - quitButtonHeightOffset,
			                        resumeButtonWidth, resumeButtonHeight),"Quit"))
				Application.LoadLevel(0);
		}
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gamePaused = true;
            Time.timeScale = 0;
        }
	}
}
