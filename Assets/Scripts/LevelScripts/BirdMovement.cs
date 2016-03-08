using UnityEngine;
using System.Collections;

public class BirdMovement : MonoBehaviour
{
    public float xMinClamp;         //X factor for clamp
    public float xMaxClamp;
    Transform bird;                 //Get bird component so we can rotate
    public float speed;             //Speed at which bird moves
    public Transform eggObject;     //Object bird drops
    public float eggSpeed;          //speed of object dropped

    bool isFacingLeft;
	// Use this for initialization
	void Start ()
    {
        bird = GetComponent<Transform>();
        isFacingLeft = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (bird)
        {
            if ((bird.transform.position.x != xMinClamp) && isFacingLeft)   //moving to the left
            {
                bird.transform.position += Vector3.left * speed* Time.deltaTime;
                //isFacingLeft = false;
            }
            else if ((bird.transform.position.x != xMaxClamp) && !isFacingLeft)    //Moving to the right
            {
                bird.transform.position += Vector3.right * speed * Time.deltaTime;
                //isFacingLeft = true;
            }
            if (bird.transform.position.x <= xMinClamp)             //Change direction
            {
                isFacingLeft = false;
            }
            else if (bird.transform.position.x >= xMaxClamp)
            {
                isFacingLeft = true;
            }
            print(Time.deltaTime);

        }
    }
}
