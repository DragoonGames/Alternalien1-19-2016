using UnityEngine;
using System.Collections;
using System.Threading;

public class SandyMovement : MonoBehaviour {

    private System.Threading.Thread m_thread = null;

    bool isFacingRight = true;

    public float maxSpeed = 100f;
    public float jumpSpeed = 11500f;
    public float gravityScale = 50.0f;
    private bool isActive = false;
    bool isGrounded = true;

    private float jumpRate = 0.25F;
    float nextJump = 0.75F;
    public Animator anim;

    //Variables for powers
    private bool triggered;
    private bool released;
    bool isUsingPower = false;
    public float timeToScale = 0.001f;
    private float sandyTimeScale = 1f;

	//PistonDrops pistonDropStuff;
	public GameObject[] pistons;
    float[] pistonSavedSpeeds;
    AudioSource myAudioSource;
    public AudioClip sandyPower;
    public AudioClip keycardPickup;
    Rigidbody2D myRigid;

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isFacingRight", isFacingRight);
        anim.SetBool("isUsingPower", isUsingPower);
        /*m_thread = new System.Threading.Thread(Run);
        m_thread.Start();*/
        myAudioSource = GetComponent<AudioSource>();
        triggered = false;
        Time.timeScale = 1;
		pistons = GameObject.FindGameObjectsWithTag ("Piston");
        myRigid = GetComponent<Rigidbody2D>();
        gravityScale = myRigid.gravityScale;
    }
    /*private void AnimationBoolControl(float speed, bool facingRight, bool power, bool ground)
    {
        /*print(speed);
        print(facingRight);
        print(power);
        print(ground);
        if (speed != 0)
        {
            anim.SetFloat("speed", speed);
        }
        anim.SetFloat("speed", speed);
        anim.SetBool("isFacingRight", facingRight);
        anim.SetBool("isUsingPower", power);
        anim.SetBool("isGrounded", ground);
    }*/
    /*private void Run()
    {
        AnimationBoolControl(0.0f, isFacingRight, isUsingPower, isGrounded);
    }*/
    IEnumerator Jump()
    {
        isGrounded = false;
        //AnimationBoolControl(0, isFacingRight, isUsingPower, isGrounded);
        anim.SetBool("isGrounded", isGrounded);
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpSpeed);

        yield return new WaitForSeconds(nextJump);

        isGrounded = true;
        //(0, isFacingRight, isUsingPower, isGrounded);
        anim.SetBool("isGrounded", isGrounded);
    }
    void CheckDirection(float moveSpeed)
    {
        if (isFacingRight)
        {
            anim.SetBool("isFacingRight", isFacingRight);

            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                isFacingRight = false;
                //Start of Changing Sprites
                anim.SetFloat("speed", moveSpeed);
                anim.SetBool("isFacingRight", isFacingRight);
            }

        }
        if (!isFacingRight)
        {
            anim.SetBool("isFacingRight", isFacingRight);

            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                isFacingRight = true;
                //Start of Changing Sprites
                anim.SetFloat("speed", moveSpeed);
                anim.SetBool("isFacingRight", isFacingRight);
            }
        }
    }
    void Update()
    {
        if (isActive)
        {
            float move = Input.GetAxis("Horizontal");
            float speed = (move * maxSpeed);
            anim.SetFloat("speed", speed);
            GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

            CheckDirection(speed);
            //Check for One Jump
            //Calls jump if button pressed, and is Grounded
            if (Input.GetButton("Jump") && isGrounded)
            {
                StartCoroutine(Jump());
            }
            CheckDirection(speed);
            if (Input.GetKeyDown(KeyCode.F) && !isUsingPower)
            {
                isUsingPower = true;
                //AnimationBoolControl(speed, isFacingRight, isUsingPower, isGrounded);
                anim.SetBool("isUsingPower", isUsingPower);
                pistonSavedSpeeds = new float[pistons.Length];
                for (int i = 0; i < pistons.Length; i++)
                {
					//print (pistons[i].GetComponent<PistonDrops>().pistonSpeed);
                    pistonSavedSpeeds[i] = pistons[i].GetComponent<PistonDrops>().pistonSpeed;
					pistons[i].GetComponent<PistonDrops>().pistonSpeed = 0.1F;
                }
                Trigger();
            }
            if (triggered && Input.GetKeyDown(KeyCode.C))
            {
                isUsingPower = false;
                //AnimationBoolControl(speed, isFacingRight, isUsingPower, isGrounded);
                anim.SetBool("isUsingPower", isUsingPower);
                Released();
            }
        }
    }
    void Trigger()
    {
        print("trigger pressed");
        triggered = true;
        released = false;
        myAudioSource.Stop();
        myAudioSource.clip = sandyPower;
        myAudioSource.Play(8820);     //Test for 5 seconds
        //Time.timeScale = timeToScale;
		for (int i = 0; i < pistons.Length; i++)
		{
			pistons [i].GetComponent<PistonDrops> ().pistonSpeed = 0.1f;
		}
    }
    void Released()
    {
        triggered = false;
        released = true;
        myAudioSource.Stop();
        for (int i = 0; i < pistons.Length; i++)
        {
			pistons[i].GetComponent<PistonDrops>().pistonSpeed = pistonSavedSpeeds[i];
        }
    }
    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.tag == "isCardKey")
        {
            myAudioSource.Stop();
            myAudioSource.clip = keycardPickup;
            myAudioSource.Play(8820);       //Test for 5 seconds
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
        if (other.gameObject.tag == "UpWind")         //This signals that we are on a wind vortex
        {
            print("In Up Wind");
            myRigid.AddForce(Vector2.up * maxSpeed * (transform.localScale.x / 2));
            myRigid.gravityScale = 0;
            /*if (other.gameObject.GetComponent<CapsuleDirection>().vertical)
            {
                myRigid.AddForce(Vector2.up * maxSpeed * Time.deltaTime * (transform.localScale.x / 2));
            }*/
        }
        else if (other.gameObject.tag == "LeftWind")
        {
            myRigid.AddForce(Vector2.left * maxSpeed * transform.localScale.y);
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                myRigid.constraints = RigidbodyConstraints2D.None;
            }
            if (myRigid.gravityScale == 0)
            {
                myRigid.constraints = RigidbodyConstraints2D.FreezePositionY;
                
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "UpWind")         //This signals that we are on a wind vortex
        {
            myRigid.gravityScale = gravityScale;
        }
        if (other.gameObject.tag == "LeftWind")
        {
            myRigid.constraints = RigidbodyConstraints2D.FreezeRotation;
            if(Input.GetKeyDown(KeyCode.DownArrow))
            {
                myRigid.gravityScale = gravityScale;
            }
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
}
