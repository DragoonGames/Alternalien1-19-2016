using UnityEngine;
using System.Collections;

public class CloneMovement : MonoBehaviour {

    //Determine direction Clone is facing. Also set the bool in anim controller
    bool isFacingRight = true;
    bool isFacingLeft = false;

    //Standard speed set here, can be modified if need be
    public float maxSpeed = 100f;
    public float jumpSpeed = 15000f;
    float gravityScale = 50.0f;

    //Handled by the control script to detemine active alien
    private bool isActive = false;
    //Jump boolean
    bool isGrounded = true;

    //Delay to jump again
    private float jumpRate = 0.25F;
    float nextJump = 0.75F;

    //Access the animation controller
    public Animator anim;

    //Create an array of objects that Clone can clone into
    public GameObject[] itemTransform;
    //store the sprite of the object within range
    public Sprite CloneCopyCat;
    //Copy of the original clone so to undo the clone process
    public Sprite OriginalClone;
    //Require the renderer to change the sprites
    private SpriteRenderer spriteRenderer;
    //Call the audiosource to change/play audio
    AudioSource myAudioSource;

    //Stores the orinal collider by getting it in Start()
    private BoxCollider2D originalCollider;
    //Stores the size of the collider by getting the originalCollider
    private Vector3 storedColliderSize;
    //Stores the offset of the collider by getting the originalCollider
    private Vector2 storedColliderOffset;
    //Stores the original scale of Clone
	private Vector3 originalScale;
    //Gets and stores the scale of the object within range
	private Vector3 itemTransformScale;
    //Stores the audio clip for the keycard sound
    public AudioClip keycardPickup;
    //Stores the rigidbody by getting the initial in Start()
    Rigidbody2D myRigid;
    //Gets and Stores the rigidbody within range
    Rigidbody2D itemRigid;

    //Size to determine range of objects to clone
    public float inRange;
    //Is Clone using his power?
    bool isUsingPower = false;
    //Yes, Clone is using power
    private bool triggered;
    //No, Clone is not using power
    private bool released;
    //Index of object in array to clone into. Needs to be global and not local
    int index = 0;

    //Called 
    void Start()
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
         * Gets the animator controller from component list      *
         * if no controller is present, no animation will play.  *
         * Could consider just writing it in if there are none   *
         * present in case a designer deletes it accidently      *
         *                                                       *
         * Also set the initial values for the controller by     *
         * using the SetBool and the SetFloat for the different  *
         * states and the speed, respectively                    *
         * * * * * * * * * * * * * * * * * * * * * * * * * * * * */ 
        anim = GetComponent<Animator>();
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isFacingRight", isFacingRight);
        anim.SetBool("isUsingPower", isUsingPower);
		inRange = 10 * transform.localScale.x;

        //Set the initial trigger to false
        triggered = false;

        /* Obtain all of the original clone objects as listed above
         * This will be used for when Clone deactivates his power */
        spriteRenderer = GetComponent<SpriteRenderer>();
		originalScale = transform.localScale;
        originalCollider = gameObject.GetComponent<BoxCollider2D>();
        storedColliderSize = GetComponent<BoxCollider2D>().size;
        storedColliderOffset = GetComponent<BoxCollider2D>().offset;
        OriginalClone = transform.GetComponent<SpriteRenderer>().sprite;

        /* Populates the array of all gameobjects that Lenny can use to levitate
         * since they are in the same levels together */
        itemTransform = GameObject.FindGameObjectsWithTag("Lenny");
        //Instantiate the audio source
        myAudioSource = GetComponent<AudioSource>();
        //Grab the original rigidbody
        myRigid = GetComponent<Rigidbody2D>();
        gravityScale = myRigid.gravityScale;
    }
    /* IEnumerator used a timer essentially to return a value or to kill a 
     * method's resources after a certain amount of time to reduce CPU load */
    IEnumerator Jump()
    {
        //Is jumping
        isGrounded = false;
        //Set animation bool to true
        anim.SetBool("isGrounded", isGrounded);
        //Actual jump mechanic
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpSpeed);

        //Delay for next jump
        yield return new WaitForSeconds(nextJump);

        //Is on the ground
        isGrounded = true;
        //Set the bool back to false
        anim.SetBool("isGrounded", isGrounded);
    }
    //Primary source for left/right
    void CheckDirection(float moveSpeed)
    {
        if (isFacingRight)
        {
            //Set the animation controller to face right
            anim.SetBool("isFacingRight", isFacingRight);

            //Turning Left
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                isFacingLeft = true;
                isFacingRight = false;
                //Set speed to indicate left movement
                anim.SetFloat("speed", moveSpeed);
                anim.SetBool("isFacingRight", isFacingRight);
            }

        }
        if (isFacingLeft)
        {
            //Set the animation controller to face left
            //Don't be distracted by the facing right below
            anim.SetBool("isFacingRight", isFacingRight);

            //Turning Right
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                isFacingLeft = false;
                isFacingRight = true;
                //Set speed to indicate left movement
                anim.SetFloat("speed", moveSpeed);
                anim.SetBool("isFacingRight", isFacingRight);
            }
        }
    }
    //Uodate is called every frame
    void Update()
    {
        //If active alien
        if (isActive)
        {
            print(myRigid.gravityScale);
            //Movement mechanic
            float move = Input.GetAxis("Horizontal");
            float speed = (move * maxSpeed);
            //Set speed to indicate running
            anim.SetFloat("speed", speed);
            //Actual movement mechanic
            GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

            //Check directions in case need to turn
            CheckDirection(speed);
            //Check for One Jump
            //Calls jump if button pressed, and is Grounded
            if (Input.GetButton("Jump") && isGrounded)
            {
                StartCoroutine(Jump());
            }
            CheckDirection(speed);

            
            //Is using power and turns it off
            if (triggered && Input.GetKeyDown(KeyCode.C))
            {
                isUsingPower = false;
                Released();
            }
            //If not using power
            if (!triggered)
            {
                //For loop to go through all objects
                for (int i = 0; i < itemTransform.Length; i++)
                {
                    //Check distance between clone and objects to see if within range
                    if (Vector3.Distance(itemTransform[i].transform.position, transform.position) < inRange)
                    {
                        //Actual key to activate power
                        if (Input.GetKeyDown(KeyCode.F))
                        {
                            //isUsingPower = true;
                            //anim.SetBool("isUsingPower", isUsingPower);

                            //Changes clones components to that of its copy
                            CloneCopyCat = itemTransform[index].GetComponent<SpriteRenderer>().sprite;
							itemTransformScale = itemTransform[index].GetComponent<Transform>().localScale;
                            if (!itemTransform[index].GetComponent<Rigidbody2D>())
                            {
                               itemRigid = itemTransform[index].gameObject.AddComponent<Rigidbody2D>();
                            }
                            else
                            {
                                itemRigid = itemTransform[index].GetComponent<Rigidbody2D>();
                            }
                            //Grabs the index of the object to ensure success
                            index = i;
                            Trigger();
                        }
                    }
                }
            }
        }
    }
    //Actual method to start the changing of sprites
    void Trigger()
    {
        triggered = true;
        released = false;
        //isUsingPower = true;
        //anim.SetBool("isUsingPower", isUsingPower);
        anim.enabled = false;
        print("Anim" + anim.enabled);

        //Put display Icon here
		this.gameObject.GetComponent<SpriteRenderer> ().sprite = CloneCopyCat;
		transform.localScale = itemTransformScale;
        //spriteRenderer.sprite = CloneCopyCat;
        originalCollider.size = itemTransform[index].GetComponent<BoxCollider2D>().size;
        originalCollider.offset = itemTransform[index].GetComponent<BoxCollider2D>().offset;
        //gameObject.GetComponent<BoxCollider2D>() = itemTransform[index].GetComponent<Collider2D>();
        //gameObject.AddComponent<BoxCollider2D>(clonedCollider);
    }

    //Release object and return to normal state
    void Released()
    {
        triggered = false;
        released = true;

        anim.enabled = true;
        anim.SetBool("isUsingPower", isUsingPower);
        gameObject.GetComponent<SpriteRenderer>().sprite = OriginalClone;

		transform.localScale = originalScale;

        originalCollider.size = storedColliderSize;
        originalCollider.offset = storedColliderOffset;
        if (itemTransform[index].gameObject.tag != "Player")
        {
            itemRigid.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.tag == "isCardKey")
        {
            myAudioSource.Stop();
            myAudioSource.clip = keycardPickup;
            myAudioSource.Play();
            Destroy(c.gameObject);
            if (GameObject.Find("Sand_Bridge"))
            {
                GameObject.Find("Sand_Bridge").GetComponent<DesertDrawBridge>().count++;

            }
        }
    }

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
     *                                                                               *
     *   This requires the character to hit the trigger that has to have a tag of    *
     *   "Wind" in order for it to actually trigger the add force. When the player   *
     *   hits the horizontal trigger, I had to actually stop the y position so the   *
     *   player won't be able to go above the horizontal trigger, so spikes there    *
     *   would be completely useless.                                                *
     *                                                                               *
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Wind")         //This signals that we are on a wind vortex
        {
            myRigid.gravityScale = 0;
            if (other.gameObject.GetComponent<CapsuleDirection>().vertical)
            {
                myRigid.AddForce(Vector2.up * maxSpeed * Time.deltaTime * (transform.localScale.x / 2));
                
            }
            else if (!other.gameObject.GetComponent<CapsuleDirection>().vertical)
            {
                myRigid.AddForce(Vector2.left * maxSpeed * transform.localScale.x);
                if (myRigid.gravityScale == 0)
                {
                    myRigid.constraints = RigidbodyConstraints2D.FreezePositionY;
                }
            }
            
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Wind")         //This signals that we are on a wind vortex
        {
            myRigid.gravityScale = gravityScale;
        }
        if (myRigid.gravityScale != 0)
        {
            myRigid.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
    void SetActive()
    {
        isActive = true;
        //myRigid.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    void SetInactive()
    {
        isActive = false;
    }
}
