using UnityEngine;
using System.Collections;

public class WallSpikes : MonoBehaviour {

    public float timer          = 0.5f;
    public float speed          = 15.0f;
    public float maxDistance    = 10.0f;
    public float mass           = 0.175f;        //Determines how fast the object moves, use in concurrent with Gravity Scale
    public float gravityScale   = 0;

    private bool trap           = false;
    private Vector3 originalPos;
    public bool Horizontal;

    Rigidbody2D myRigid;
    void Start() {
        originalPos = transform.position;
    }
	// Update is called once per frame
	void Update ()
    {
        /*if (transform.position.x >= maxDistance)
        {       // If spike is past the max point/ wall
            trap = false;                       // the movement is no longer active
            transform.position = originalPos;   // Reset position.
        }*/
    }
    public void Kill()          //This will be called by the boss since this is his ability so we move everything from Update to this
    {
        if (!Horizontal)
        {
            //transform.Translate(Vector2.down * speed);   // Translate the gameObject to the right.
            if (!gameObject.GetComponent<Rigidbody2D>())
            {
                gameObject.AddComponent<Rigidbody2D>();
            }
            gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.down * speed * transform.localScale.x);
            //gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.down * speed * transform.localScale.x);
            myRigid = gameObject.GetComponent<Rigidbody2D>();
            myRigid.mass = mass;
            myRigid.gravityScale = gravityScale;
            //myRigid.constraints = RigidbodyConstraints2D.FreezePositionX;
        }
        else
        {
            //transform.Translate(Vector2.down * speed);
            if (!gameObject.GetComponent<Rigidbody2D>())
            {
                gameObject.AddComponent<Rigidbody2D>();
            }
            //gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * speed * transform.localScale.x);
            gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * speed * transform.localScale.x);
            myRigid = gameObject.GetComponent<Rigidbody2D>();
            myRigid.mass = mass;
            myRigid.gravityScale = gravityScale;
            //myRigid.constraints = RigidbodyConstraints2D.FreezePositionY;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")           //Call Death Script
        {

        }
        if (other.gameObject.tag == "Respawn")          //Relocate the Spikes
        {
            print("Hit Spikes");
            // If spike is past the max point/ wall
            transform.position = originalPos;   // Reset position.
            Destroy(myRigid);
        }
    }
}
