using UnityEngine;
using System.Collections;

public class PistonDrops : MonoBehaviour {

    public GameObject pistonsUp;
    public GameObject pistonsDown;

    bool goingUp;
    bool goingDown;

	// Use this for initialization
	void Start () {
        goingDown = true;
        goingUp = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (goingUp) // change value of y
        {
            /*print(transform.position.y);
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
            moveDirection = transform.TransformDirection(moveDirection);
            //Multiply it by speed
            moveDirection *= speed;
            moveDirection.y += gravity * Time.time;
            */
        }
        else if (goingDown)
        {
            //print(transform.position.y);
            //print(Time.deltaTime);
            //moveDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
            //moveDirection = transform.TransformDirection(moveDirection);
            //Multiply it by speed
            //moveDirection *= speed;
            //moveDirection.y -= gravity * Time.time;
            transform.Translate(new Vector3(0.0f, -0.005f, 0.0f));
        }
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        print(other.gameObject.name);
        if (other.gameObject == pistonsDown)
        {
            print(other.gameObject.name);
            goingDown = true;
            goingUp = false;
        }
        if (other.gameObject == pistonsUp)
        {
            print(other.gameObject.name);
            goingUp = true;
            goingDown = false;
        }
    }
}
