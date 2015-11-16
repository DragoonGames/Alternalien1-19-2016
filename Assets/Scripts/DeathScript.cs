using UnityEngine;
using System.Collections;

public class DeathScript : MonoBehaviour {

    public string currentLevel;
    public GameObject triggerObject;
    public bool deathPossible;
    public bool objectDestroyable;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

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
