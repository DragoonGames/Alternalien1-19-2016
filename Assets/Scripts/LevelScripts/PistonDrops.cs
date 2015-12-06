using UnityEngine;
using System.Collections;

public class PistonDrops : MonoBehaviour {

    public GameObject pistonsUp;
    public GameObject pistonsDown;

	public float pistonSpeed;
    bool goingUp;
    bool goingDown;
    Random pistonRandomSpeed;
    public bool isRandom;
	// Use this for initialization
	void Start () {
        goingDown = true;
        goingUp = false;
        if (!isRandom)
        {
            /*if (pistonSpeed < 10.0f || 0 < pistonSpeed)
            {
                pistonSpeed = 1;
            }
            */
        }
        if (isRandom)
        {
            pistonSpeed = Random.Range(3.0f, 5.0f);
        }
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
			transform.Translate(new Vector3(0.0f, pistonSpeed, 0.0f));
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
            transform.Translate(new Vector3(0.0f, -pistonSpeed, 0.0f));
        }
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == pistonsDown)
        {
            goingDown = true;
            goingUp = false;
        }
        if (other.gameObject == pistonsUp)
        {
            goingUp = true;
            goingDown = false;
        }
    }
}
