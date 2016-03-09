using UnityEngine;
using System.Collections;

public class BirdMovement : MonoBehaviour
{
    public float xMinClamp;         //X factor for clamp
    public float xMaxClamp;
    Transform bird;                 //Get bird component so we can rotate
    public float speed;             //Speed at which bird moves
    public Rigidbody2D eggObject;     //Object bird drops

    //public float maxSpeed = 100f;
    public float xTrackSmooth = 10.0f;

    Rigidbody2D myRigid;
    bool isFacingLeft;
    public float timer = 0.0f;
    public int count = 1;
    public float eggTimer;
    public bool dropTest = false;
    // Use this for initialization
    void Start()
    {
        bird = GetComponent<Transform>();
        isFacingLeft = true;
        if (!GetComponent<Rigidbody2D>())
        {
            print("No Rigidbody");
            myRigid = this.gameObject.AddComponent<Rigidbody2D>();
            myRigid.mass = 0;
            myRigid.gravityScale = 0;
        }
        else
        {
            myRigid = this.gameObject.GetComponent<Rigidbody2D>();
            myRigid.mass = 0;
            myRigid.gravityScale = 0;
        }
        if(eggTimer <= 0)
        {
            eggTimer = 1;
        }
        //StartCoroutine(thisSucks());
    }

    // Update is called once per frame
    void Update()
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
         *                                                                                                       *
         *  This doesn't work because the values must be set in the script, so they can only have one speed for  *
         *  every instance of this script, and it really needs to be able to be changed in the inspector. If you *
         *  can find a solution that'll fit this scenario, feel free to change it.                               *
         *                                                                                                       *
         * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
            /* while(bird)
            {
                timer -= Time.deltaTime;

                float localSpeed = maxSpeed;
                if (timer <= 0) {
                    print(maxSpeed);
                    //myRigid.velocity = new Vector2(maxSpeed * Time.deltaTime, GetComponent<Rigidbody>().velocity.y);
                    myRigid.velocity = new Vector2(maxSpeed * Time.deltaTime, 0.0f);
                    if(transform.position.x <= xMinClamp)
                        maxSpeed = localSpeed;
                    if (transform.position.x >= xMaxClamp)
                        maxSpeed = -localSpeed;
                    timer = 5;
                    yield return null;
                }

                //yield return null;
                //
                */

        float targetX = transform.position.x;
        if ((bird.transform.position.x >= xMinClamp) && isFacingLeft)   //moving to the left
        {
            bird.transform.position += Vector3.left * speed * Time.deltaTime;
            //targetX = Mathf.Lerp(transform.position.x, xMinClamp, xTrackSmooth);
            //maxSpeed *= -1;
            //isFacingLeft = false;
        }
        else if ((bird.transform.position.x <= xMaxClamp) && !isFacingLeft)    //Moving to the right
        {
            bird.transform.position += Vector3.right * speed * Time.deltaTime;
            //targetX = Mathf.Lerp(transform.position.x, xMaxClamp, xTrackSmooth);
            //maxSpeed *= -1;
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
        if (dropTest)
        {
            StartCoroutine(DropEgg());
        }
    }
    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "Player")
        {
            //Player hits target range
            dropTest = true;
        }
        else
            dropTest = false;
    }
    IEnumerator DropEgg()
    {
        dropTest = false;

        print("Drop Egg");
        Rigidbody2D eggClone;
        eggClone = Instantiate(eggObject, transform.position, transform.rotation) as Rigidbody2D;
        if (isFacingLeft)
        {
            eggClone.velocity = transform.TransformDirection(Vector3.left * speed + Vector3.down * speed);
        }
        else if (!isFacingLeft)
        {
            eggClone.velocity = transform.TransformDirection(Vector3.right * speed + Vector3.down * speed);

        }
        yield return new WaitForSeconds(eggTimer);
    }
}