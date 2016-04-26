using UnityEngine;
using System.Collections;

public class LockNextMirror : MonoBehaviour
{
    public GameObject nextMirror;               //This will unlock the next mirror so you can use it

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        //if (nextMirror)
        //{
            if (GetComponent<Rigidbody2D>().constraints == RigidbodyConstraints2D.FreezeAll)         //Locked into place
            {
                nextMirror.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            }
        //}

	}
}
