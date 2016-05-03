using UnityEngine;
using System.Collections;

public class LockNextMirror : MonoBehaviour
{
    public GameObject nextMirror;               //This will unlock the next mirror so you can use it
    SpriteRenderer rend;

	// Use this for initialization
	void Start ()
    {
        rend = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (nextMirror)
        {
            if (rend.flipX)         //Flipped by Lenny
            {
                nextMirror.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            }
            else                    //Not Flipped by Lenny
            {
                nextMirror.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }

	}
}
