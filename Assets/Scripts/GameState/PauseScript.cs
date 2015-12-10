using UnityEngine;
using System.Collections;

public class PauseScript : MonoBehaviour {

	public bool gamePaused = false;

	public float pauseBoxWidth = 250;
	public float pauseBoxHeight = 150;

	public float resumeButtonWidth = 100;
	public float resumeButtonHeight = 25;
    public float resumeButtonWidthOffset = 0;
	public float resumeButtonHeightOffset = 25;

    public float restartButtonWidth = 0;
    public float restartButtonHeight = 0;
    public float restartButtonWidthOffset = 50;
    public float restartButtonHeightOffset = 0;

    public float quitButtonWidth = 0;
	public float quitButtonHeight = -75;
    public float quitButtonWidthOffset = 50;
	public float quitButtonHeightOffset = 0;

	public Texture pauseTexture;
	public Texture resumeButtonTexture;
	public Texture quitButton;
    public string currentLevel;
    public string mainMenu;
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
            if (GUI.Button(new Rect(((Screen.width / 2) - (restartButtonWidth / 2)) - restartButtonWidthOffset,
                                    ((Screen.height / 2) - (restartButtonHeight / 2)) - restartButtonHeightOffset,
                                    resumeButtonWidth, resumeButtonHeight), "Restart Level"))
            {
                GameObject activeAlien = Camera.main.GetComponent<ControlScript>().firstTarget.gameObject;
                Destroy(activeAlien);
                Application.LoadLevel(currentLevel);
                Time.timeScale = 1;
                
            }

            if (GUI.Button(new Rect(((Screen.width / 2) - (quitButtonWidth / 2)) - quitButtonWidthOffset,
                                    ((Screen.height / 2) - (quitButtonHeight / 2)) - quitButtonHeightOffset,
                                    resumeButtonWidth, resumeButtonHeight), "Quit"))
            {
                Application.LoadLevel(0);
                Time.timeScale = 1;
            }
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
