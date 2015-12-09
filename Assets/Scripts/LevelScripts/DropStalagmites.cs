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
                Rigidbody2D objectRigidbody;
                if (!objectToDrop.GetComponent<Rigidbody2D>())
                {
                    objectRigidbody = objectToDrop.AddComponent<Rigidbody2D>();
                }
                else
                    objectRigidbody = objectToDrop.GetComponent<Rigidbody2D>();
                objectRigidbody.mass = 5;
                objectRigidbody.gravityScale = 5;
				objectRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
				objectRigidbody.constraints = RigidbodyConstraints2D.FreezePositionX;
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
