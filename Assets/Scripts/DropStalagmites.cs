using UnityEngine;
using System.Collections;

public class DropStalagmites : MonoBehaviour {

    public GameObject objectToDrop;
    public GameObject objectToDestroy;

    public bool isTrigger;
    public bool onDestroy;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        if (isTrigger)
        {
            if (objectToDrop != null)
            {
                Rigidbody2D objectRigidbody = objectToDrop.AddComponent<Rigidbody2D>();
                objectRigidbody.mass = 5;
                objectRigidbody.gravityScale = 5;
            }
        }
    }
    void OnDestroyObject()
    {
        if (onDestroy)
        {
            if (objectToDestroy == null)
            {
                print("Drop on Destroy");
                Rigidbody2D objectRigidbody = objectToDrop.AddComponent<Rigidbody2D>();
                objectRigidbody.mass = 5;
                objectRigidbody.gravityScale = 5;
            }
        }
    }
}
