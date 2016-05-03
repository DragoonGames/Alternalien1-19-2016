using UnityEngine;
using System.Collections;

public class EndingGame : MonoBehaviour
{
    public GameObject boss;
    public bool gameOver;
    public GameObject fireworks;
    public float fireworkTimer = 0.50f;
    public Camera mainCamera;
    public GameObject[] players;                        //Deactivate kill scripts

    public float endGameTimer;
    public string winLevel;

    public int GUIWidth;
    public int GUIHeight;
	// Use this for initialization
	void Start ()
    {
        if (boss == null)
        {
            boss = GameObject.Find("Boss");
        }
        if (GUIWidth == 0)
        {
            GUIWidth = 250;
        }
        if (GUIHeight == 0)
        {
            GUIHeight = 100;
        }
        mainCamera = Camera.main;
        if(endGameTimer <= 5)
        {
            endGameTimer = 10.0f;
        }
        players = GameObject.FindGameObjectsWithTag("Player");
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (boss == null)
        {
            gameOver = true;
        }
        if(gameOver)
        {
            fireworkTimer -= Time.deltaTime;
            endGameTimer -= Time.deltaTime;

            foreach(var go in players)
            {
                go.gameObject.tag = "Untagged";
            }
        }
        if (fireworkTimer <= 0)
        {
            fireworkTimer = 2.0f;
        }
        if(endGameTimer <= 0)
        {
            Application.LoadLevel(winLevel);
        }
    }
    void OnGUI()
    {
        if(gameOver)
        {
            GUI.Label(new Rect((Screen.width / 2) - (GUIWidth / 2), (Screen.height / 2 - GUIHeight), GUIWidth, GUIHeight), "YOU WIN!");
            if (fireworkTimer <= 0.5)
            {
                Instantiate(fireworks, new Vector3(Random.Range(mainCamera.transform.position.x - mainCamera.orthographicSize, mainCamera.transform.position.x + mainCamera.orthographicSize),
                    Random.Range(mainCamera.transform.position.y - mainCamera.orthographicSize, mainCamera.transform.position.y + mainCamera.orthographicSize), 0), Quaternion.Euler(0, 0, 0));
            }
            
        }
    }
}
