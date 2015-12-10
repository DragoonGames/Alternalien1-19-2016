using UnityEngine;
using System.Collections;

public class LennyMovement : MonoBehaviour {

    bool isFacingRight = true;
    bool isFacingLeft = false;

    public float maxSpeed = 10f;
    public float jumpSpeed = 100f;
    private bool isActive = false;
    private bool isVine = false;
    bool isGrounded = true;
    bool isUsingPower = false;

    private float jumpRate = 0.25F;
    public float nextJump = 0.0F;
    public Animator anim;
    //Variables for powers
    public GameObject[] itemTransform; //All objects to collect including players
    public GameObject[] playerObjects;  //Gather player tags
    public GameObject[] taggedObjects;  //Gather all tags for Lenny
    int[] itemTransformScalesX;
    int[] itemTransformScalesY;

    private float inRange;
    private bool triggered;
    private bool released;
    int index = 0;

    AudioSource myAudioSource;
    public AudioClip keycardPickup;
    Rigidbody2D myRigid;

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isFacingRight", isFacingRight);
        anim.SetBool("isUsingPower", isUsingPower);
        myAudioSource = GetComponent<AudioSource>();
        myRigid = GetComponent<Rigidbody2D>();
        inRange = 50.0f;
        triggered = false;
        taggedObjects = GameObject.FindGameObjectsWithTag("Lenny");
        playerObjects = GameObject.FindGameObjectsWithTag("Player");
        int taggedCount = taggedObjects.Length + playerObjects.Length - 1;
        itemTransform = new GameObject[taggedCount];
        for (int j  = 0; j < taggedObjects.Length; j++)
        {
            itemTransform[j] = taggedObjects[j];
        }
        for (int k = 0; k < 1; k++)
        {
            itemTransform[k + taggedObjects.Length] = GameObject.Find("Clone");
            itemTransform[k + 1 + taggedObjects.Length] = GameObject.Find("Sandy");
        }
    }
    IEnumerator Jump()
    {
        isGrounded = false;
        anim.SetBool("isGrounded", isGrounded);
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpSpeed);

        yield return new WaitForSeconds(nextJump);

        isGrounded = true;
        anim.SetBool("isGrounded", isGrounded);
    }
    void CheckDirection(float moveSpeed)
    {
		if (isFacingRight) {
			anim.SetBool ("isFacingRight", isFacingRight);

			if (Input.GetKeyDown (KeyCode.LeftArrow) || Input.GetKeyDown (KeyCode.A)) {
				isFacingLeft = true;
				isFacingRight = false;
				//Start of Changing Sprites
				anim.SetFloat ("speed", moveSpeed);
				anim.SetBool ("isFacingRight", isFacingRight);

			}
			//if (triggered)
				//itemTransform [index].localPosition = new Vector3 (1.5f, 0.5f, 0.0f);
            
		}
		if (isFacingLeft) {
			anim.SetBool ("isFacingRight", isFacingRight);

			if (Input.GetKeyDown (KeyCode.RightArrow) || Input.GetKeyDown (KeyCode.D)) {
				isFacingLeft = false;
				isFacingRight = true;
				//Start of Changing Sprites
				anim.SetFloat ("speed", moveSpeed);
				anim.SetBool ("isFacingRight", isFacingRight);
				if(triggered)
				{
					itemTransform[index].transform.Translate(0.5f,0f,0f);
					print("Move Child");
				}
			}
			//if (triggered)
			//itemTransform[index].localPosition = new Vector3(-1.5f, 0.5f, 0.0f);
		}
	}
    void Update()
    {
        if (isActive)
        {
            //CheckDirection(speed);
            float move = Input.GetAxis("Horizontal");
            float speed = (move * maxSpeed);
            anim.SetFloat("speed", speed);

            GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
			if(triggered)
			{
				itemTransform[index].GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
				print("Move Child");
			}
            if (isVine)
            {
                float vertical = Input.GetAxis("Vertical");
                GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, vertical * maxSpeed);
            }
            CheckDirection(speed);
            //Check for One Jump
            //Calls jump if button pressed, and is Grounded
            if (Input.GetButton("Jump") && isGrounded)
            {
                StartCoroutine(Jump());
            }
            CheckDirection(speed);
            if (!released && Input.GetKeyDown(KeyCode.C))
            {
                Released();
            }
            for (int i = 0; i < itemTransform.Length; i++)
            {
                
                if (Vector3.Distance(itemTransform[i].transform.position, transform.position) < inRange)
                {
                    if (!triggered && Input.GetKeyDown(KeyCode.F))
                    {
                        index = i;
                        Trigger();
                    }
                }
            }
            //MoveChild();
            if (Input.GetKey(KeyCode.UpArrow) && triggered)
            {
                //Rigidbody2D itemMass = itemTransform[index].GetComponent<Rigidbody2D>();
                //itemMass.mass = 0;
                //itemMass.gravityScale = 0;
                //itemTransform[index].transform.Translate(0, 2, 0);
                //move = Input.GetAxis("Vertical");
                itemTransform[index].transform.Translate(0f, 1f, 0f);
            }
			if (Input.GetKey(KeyCode.DownArrow) && triggered)
			{
				//Rigidbody2D itemMass = itemTransform[index].GetComponent<Rigidbody2D>();
				//itemMass.mass = 0;
				//itemMass.gravityScale = 0;
				//itemTransform[index].transform.Translate(0, 2, 0);
				//move = Input.GetAxis("Vertical");
				itemTransform[index].transform.Translate(0f, -1f, 0f);
			}
        }
    }
    void Trigger()
    {
        print("trigger pressed");
        triggered = true;
        released = false;

        print("in else statement");
        //Put display Icon here
        anim.SetBool("isUsingPower", isUsingPower);
        itemTransform[index].transform.parent = transform;

        print(transform.localPosition);
        print(itemTransform[index].transform.localScale);

        Rigidbody2D itemRigid = itemTransform[index].GetComponent<Rigidbody2D>();
        itemRigid.constraints = RigidbodyConstraints2D.FreezeRotation;
        //Destroy(itemRigid);
        /*if (isFacingRight)
        {
            itemTransform[index].localPosition = new Vector3(1.5f, 0.5f, 0.0f);
        }
        else if (isFacingLeft)
        {
            itemTransform[index].localPosition = new Vector3(-1.5f, 0.5f, 0.0f);
        }
        */
    }
    void Released() 
    {
        triggered = false;
        released = true;

        if (isFacingLeft)
        {
            print("Detach");
            anim.SetBool("isUsingPower", isUsingPower);
            itemTransform[index].transform.parent = null;
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
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "ClimbVine")
        {
            isVine = true;
        }
    }
    void SetActive()
    {
        isActive = true;
    }
    void SetInactive()
    {
        isActive = false;
    }
    void MoveChild()
    {
        if (triggered) //Child attached to parent
        {
            print("In MoveChild()");
            /*float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            itemTransform[index].GetComponent<Rigidbody2D>().velocity = new Vector2(horizontal * maxSpeed, vertical * maxSpeed);
            */
            Rigidbody2D LennyMass = transform.GetComponent<Rigidbody2D>();
            LennyMass.gravityScale = 0;
            LennyMass.mass = 0;
            Rigidbody2D itemMass = itemTransform[index].GetComponent<Rigidbody2D>();
            itemMass.mass = 0;
            itemMass.gravityScale = 0;
            itemTransform[index].transform.Translate(0, 2, 0);
        }
    }
}
