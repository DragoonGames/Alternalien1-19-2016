using UnityEngine;
using System.Collections;

public class SandyMovement : MonoBehaviour {

    bool isFacingRight = true;

    public float maxSpeed = 10f;
    public float jumpSpeed = 100f;
    private bool isActive = false;
    bool isGrounded = true;

    private float jumpRate = 0.25F;
    public float nextJump = 0.0F;
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
        myAudioSource = GetComponent<AudioSource>();
        triggered = false;
        Time.timeScale = 1;
		pistons = GameObject.FindGameObjectsWithTag ("Piston");
        myRigid = GetComponent<Rigidbody2D>();
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
    void CheckDirection()
    {
		float moveSpeed;
        if (!isFacingRight)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
				moveSpeed = 8f;
				isFacingRight = true;
                //Start of Changing Sprites
                anim.SetFloat("speed", moveSpeed);
                anim.SetBool("isFacingRight", isFacingRight);
            }
			if(Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D)){
				moveSpeed = 0.0f;
				anim.SetFloat("speed", moveSpeed);
			}
        }
        if (isFacingRight)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
				moveSpeed = -8f;
				isFacingRight = false;
                //Start of Changing Sprites
                anim.SetFloat("speed", moveSpeed);
                anim.SetBool("isFacingRight", isFacingRight);
            }
			if(Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A)){
				moveSpeed = 0.0f;
				anim.SetFloat("speed", moveSpeed);
			}
        }
    }
    void Update()
    {

        if (isActive)
        {
            //CheckDirection(speed);
            float move = Input.GetAxis("Horizontal");
            //float speed = (move * maxSpeed);
            //anim.SetFloat("speed", speed);
            GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

			CheckDirection(); // was CheckDirection(speed);
            //Check for One Jump
            //Calls jump if button pressed, and is Grounded
            if (Input.GetButton("Jump") && isGrounded)
            {
                StartCoroutine(Jump());
            }
            CheckDirection();
            if (Input.GetKeyDown(KeyCode.F))
            {
                isUsingPower = true;
				pistonSavedSpeeds = new float[pistons.Length];
                for (int i = 0; i < pistons.Length; i++)
                {
					print (pistons[i].GetComponent<PistonDrops>().pistonSpeed);
                    pistonSavedSpeeds[i] = pistons[i].GetComponent<PistonDrops>().pistonSpeed;
					pistons[i].GetComponent<PistonDrops>().pistonSpeed = .1F;
                }
                Trigger();
            }
            if (triggered && Input.GetKeyDown(KeyCode.C))
            {
                isUsingPower = false;
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
        myAudioSource.Play();
        anim.SetBool("isUsingPower", isUsingPower);
        //Time.timeScale = timeToScale;
		for (int i = 0; i < pistons.Length; i++)
		{
			pistons [i].GetComponent<PistonDrops> ().pistonSpeed = 0.1f;
		}
        //Put display Icon here
    }
    void Released()
    {
        triggered = false;
        released = true;
        myAudioSource.Stop();
        anim.SetBool("isUsingPower", isUsingPower);
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
            myAudioSource.Play();
            Destroy(c.gameObject);
			if (GameObject.Find("Sand_Bridge"))
			{
				GameObject.Find("Sand_Bridge").GetComponent<DesertDrawBridge>().count++;

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
