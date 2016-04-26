using UnityEngine;
using System.Collections;

public class signScript : MonoBehaviour {

    public GUIText myGuiText;               //Gets the GUI Text aspect and adds one if not existant
    public string myText;                   //Input for specific coding only if no GUI Text component is on
    public KeyCode keyPress;                //Designers select which button they press to activate the sign                 

    bool playerCollides;                    //Check if player is colliding with box in order to open the GUI

	// Use this for initialization
	void Start ()
    {
        if (!GetComponent<GUIText>())
        {
            myGuiText = gameObject.AddComponent<GUIText>();
        }
        else
        {
            myGuiText = gameObject.GetComponent<GUIText>();
        }
    }
    void Update()
    {
        print(playerCollides);
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
                GUI.Box(new Rect(0, 0, 200, 200), "Instructions");

                // Instructions for the player go here
                GUI.Label(new Rect(10, 30, 140, 40), "Arrow Keys to Move");
                GUI.Label(new Rect(10, 60, 160, 70), "Spacebar to shoot");
                GUI.Label(new Rect(10, 90, 160, 100), "Esc to Quit the Game");

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
