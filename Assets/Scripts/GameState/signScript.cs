using UnityEngine;
using System.Collections;

public class signScript : MonoBehaviour {

    public string[] myText;                   //Input for specific coding only if no GUI Text component is on

    bool playerCollides;                    //Check if player is colliding with box in order to open the GUI

	// Use this for initialization
	void Start ()
    {
        
    }
    void Update()
    {
        
    }
    void OnGUI()
    {
        if (playerCollides)
        {
            //Player Collides
            //print("Player Left");

            // Make a group at the center of the screen.
            GUI.BeginGroup(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 200));

            // Make a box to see the group on the screen.
            GUI.Box(new Rect(0, 0, 200, 200), "Hint");

            // Instructions for the player go here
            for (int i = 0; i < myText.Length; i++)
            {
                GUI.Label(new Rect(10, (i * 30) + 30, 140, 40), myText[i]);
            }
            // End the group that we started from above
            GUI.EndGroup();
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerCollides = true;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerCollides = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerCollides = false;
        }
    }

}
