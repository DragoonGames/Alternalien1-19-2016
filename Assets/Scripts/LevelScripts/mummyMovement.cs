using UnityEngine;
using System.Collections;

public class mummyMovement : MonoBehaviour {

    public float xMinClamp;             //X factor for clamp
    public float xMaxClamp;
    Transform mummy;                    //Get mummy component so we can rotate
    public float speed;                 //Speed at which bird moves

    //public float maxSpeed = 100f;
    public float xTrackSmooth = 10.0f;
    bool attack;

    bool Attack {
        get { return attack; }
        set { attack = value; }
    }

    Rigidbody2D myRigid;
    bool isFacingLeft;

    // Use this for initialization
    void Start()
    {
        mummy = GetComponent<Transform>();
        isFacingLeft = true;
        if (!GetComponent<Rigidbody2D>())
        {
            print("No Rigidbody");
            myRigid = this.gameObject.AddComponent<Rigidbody2D>();

        }
        else
        {
            myRigid = this.gameObject.GetComponent<Rigidbody2D>();
        }
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

        float targetX = transform.position.x;
        if ((mummy.transform.position.x >= xMinClamp) && isFacingLeft)   //moving to the left
        {
            mummy.transform.position += Vector3.left * speed * Time.deltaTime;
            //targetX = Mathf.Lerp(transform.position.x, xMinClamp, xTrackSmooth);
            //maxSpeed *= -1;
            //isFacingLeft = false;
        }
        else if ((mummy.transform.position.x <= xMaxClamp) && !isFacingLeft)    //Moving to the right
        {
            mummy.transform.position += Vector3.right * speed * Time.deltaTime;
            //targetX = Mathf.Lerp(transform.position.x, xMaxClamp, xTrackSmooth);
            //maxSpeed *= -1;
            //isFacingLeft = true;
        }
        if (mummy.transform.position.x <= xMinClamp)             //Change direction
        {
            isFacingLeft = false;
        }
        else if (mummy.transform.position.x >= xMaxClamp) {
            isFacingLeft = true;
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "safeZone")
        {
            Attack = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "safeZone")
        {
            Attack = true;
        }
    }
    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Player") {
            if (Attack) {
                Destroy(col.gameObject);
            }
        }
    }
}
