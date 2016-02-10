using UnityEngine;
using System.Collections;
using System.Threading;

public class SandyMovement : MonoBehaviour {

    private System.Threading.Thread m_thread = null;

    bool isFacingRight = true;

    public float maxSpeed = 100f;
    public float jumpSpeed = 11500f;
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
        m_thread = new System.Threading.Thread(Run);
        m_thread.Start();
        myAudioSource = GetComponent<AudioSource>();
        triggered = false;
        Time.timeScale = 1;
		pistons = GameObject.FindGameObjectsWithTag ("Piston");
        myRigid = GetComponent<Rigidbody2D>();
    }
    private void AnimationBoolControl(float speed, bool facingRight, bool power, bool ground)
    {
        /*print(speed);
        print(facingRight);
        print(power);
        print(ground);*/
        if (speed != 0)
        {
            anim.SetFloat("speed", speed);
        }
        anim.SetBool("isFacingRight", facingRight);
        anim.SetBool("isUsingPower", power);
        anim.SetBool("isGrounded", ground);
    }
    private void Run()
    {
        AnimationBoolControl(0, isFacingRight, isUsingPower, isGrounded);
    }
    IEnumerator Jump()
    {
        isGrounded = false;
        AnimationBoolControl(0, isFacingRight, isUsingPower, isGrounded);
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpSpeed);
        yield return new WaitForSeconds(nextJump);
        isGrounded = true;
        AnimationBoolControl(0, isFacingRight, isUsingPower, isGrounded);
    }
    void CheckDirection(float moveSpeed)
    {
        if (!isFacingRight)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                isFacingRight = true;
                AnimationBoolControl(moveSpeed, isFacingRight, isUsingPower, isGrounded);
            }
        }
        if (isFacingRight)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                isFacingRight = false;
                AnimationBoolControl(moveSpeed, isFacingRight, isUsingPower, isGrounded);
            }
        }
    }
    void Update()
    {
        if (isActive)
        {
            Run();
            //CheckDirection(speed);
            float move = Input.GetAxis("Horizontal");
            float speed = (move * maxSpeed);
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
                AnimationBoolControl(speed, isFacingRight, isUsingPower, isGrounded);
                pistonSavedSpeeds = new float[pistons.Length];
                for (int i = 0; i < pistons.Length; i++)
                {
					//print (pistons[i].GetComponent<PistonDrops>().pistonSpeed);
                    pistonSavedSpeeds[i] = pistons[i].GetComponent<PistonDrops>().pistonSpeed;
					pistons[i].GetComponent<PistonDrops>().pistonSpeed = .1F;
                }
                Trigger();
            }
            if (triggered && Input.GetKeyDown(KeyCode.C))
            {
                isUsingPower = false;
                AnimationBoolControl(speed, isFacingRight, isUsingPower, isGrounded);
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
    void SetActive()
    {
        isActive = true;
    }
    void SetInactive()
    {
        isActive = false;
    }
}
