using UnityEngine;
using System.Collections;

public class DeathScript : MonoBehaviour {

    public string currentLevel;
    public GameObject triggerObject;
    public bool deathPossible;
    public bool objectDestroyable;
    public GameObject[] players;
    public bool screwedLevels;

	// Use this for initialization
	void Start () {
        players = GameObject.FindGameObjectsWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i< players.Length; i++)
        {
            if (screwedLevels)
            {
                if (players[i] == null)   //Death
                {
                    Application.LoadLevel(currentLevel);
                    Time.timeScale = 1;
                }
            }
        }
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (deathPossible)
            {
                //Player Dies

                //Show Pause screen that displays death.
                //Maybe include a timer for any animations for death
                print("Death");

                Application.LoadLevel(currentLevel);
            }
        }
        if (other.gameObject.tag == "isBreakable")
        {
            if (objectDestroyable)
            {
                Destroy(other.gameObject);
                Destroy(triggerObject);
            }
        }
    }
}
