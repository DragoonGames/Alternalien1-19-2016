using UnityEngine;
using System.Collections;

public class GeyserBridgeBuilding : MonoBehaviour {

    public GameObject Geyser;
    public GameObject Bridge;

    public Rigidbody2D FireballRigidbody;
    public Rigidbody2D IceBallRigidbody;

    public bool isGeyser = true;
    public bool isBridge = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        print(other.gameObject.name);
        if (isGeyser) //Turn to Bridge
        {
            if (other.gameObject.name == "Iceball(Clone)") //Ice Hits Geyser
            {
                print("Turn to Bridge");
                isGeyser = false;
                isBridge = true;
                Bridge.gameObject.SetActive(true);
                Geyser.gameObject.SetActive(false);
            }
        }
        else if (isBridge) //Melt to Geyser
        {
            if (other.gameObject.name == "Fireball(Clone)") //Fire hits bridge
            {
                print("Turn to Geyser");
                isBridge = false;
                isGeyser = true;
                Geyser.gameObject.SetActive(true);
                Bridge.gameObject.SetActive(false);
            }
        }
    }

    /*void OnCollisionEnter2D(Collision2D other)
    {
        print(other.gameObject.name);
        if (isGeyser) //Turn to Bridge
        {
            if (other.gameObject.name == "Iceball(Clone)") //Ice Hits Geyser
            {
                print("Turn to Bridge");
                isGeyser = false;
                isBridge = true;
                Destroy(Geyser);
                Bridge.gameObject.SetActive(true);
                Geyser.gameObject.SetActive(false);
            }
        }
        else if (isBridge) //Melt to Geyser
        {
            if (other.gameObject.name == "Fireball(Clone)") //Fire hits bridge
            {
                print("Turn to Geyser");
                isBridge = true;
                isGeyser = false;
                Destroy(Geyser);
                Geyser.gameObject.SetActive(true);
                Bridge.gameObject.SetActive(false);
            }
        }
    }
    */
}
