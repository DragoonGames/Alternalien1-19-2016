using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DropStalagmites : MonoBehaviour {

    public GameObject objectToDrop;
    public GameObject objectToDestroy;

    public bool isTrigger;
    public bool onDestroy;
    public bool onlyOnPlayer;

    public List<string> exemptTags;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        //print("Tag is: " + other.gameObject.tag + " GameObject is " + other.gameObject.name);
        //print(exemptTags.Contains(other.gameObject.tag));
        if (!exemptTags.Contains(other.gameObject.tag))
        {
            //print(exemptTags.Contains(other.gameObject.tag));
            if (isTrigger)
            {
                if (objectToDrop != null)
                {
                    Rigidbody2D objectRigidbody;
                    if (!objectToDrop.GetComponent<Rigidbody2D>())
                    {
                        objectRigidbody = objectToDrop.AddComponent<Rigidbody2D>();
                    }
                    else
                    {
                        objectRigidbody = objectToDrop.GetComponent<Rigidbody2D>();
                        objectRigidbody.mass = 5;
                        objectRigidbody.gravityScale = 5;
                    }
                }
            }
            /*else
            {
                if (isTrigger)
                {
                    if (objectToDrop != null)
                    {
                        Rigidbody2D objectRigidbody;
                        if (!objectToDrop.GetComponent<Rigidbody2D>())
                            objectRigidbody = objectToDrop.AddComponent<Rigidbody2D>();
                        else
                        {
                            objectRigidbody = objectToDrop.GetComponent<Rigidbody2D>();
                            objectRigidbody.mass = 5;
                            objectRigidbody.gravityScale = 5;
                        }
                    }
                }
            }
            */
        }
    }
    void OnDestroyObject()
    {
        if (onDestroy)
        {
            if (objectToDestroy == null)
            {
                //print("Drop on Destroy");
                Rigidbody2D objectRigidbody = objectToDrop.AddComponent<Rigidbody2D>();
                objectRigidbody.mass = 5;
                objectRigidbody.gravityScale = 5;
            }
        }
    }
}
