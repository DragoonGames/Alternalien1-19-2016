using UnityEngine;
using System.Collections;

public class JumperMovement : MonoBehaviour {

    bool isFacingRight = true;
    bool isFacingLeft = false;

    public float maxSpeed = 10f;
    public float jumpSpeed = 100f;
    private bool isActive = false;
    bool isGrounded = true;
    bool isJumpingOne = false;
    bool isJumpingTwo = false;

    private float jumpRate = 0.25F;
    public float nextJump = 0.0F;
    public float secondJump = 3.0F;

    //public Animator anim;
    AudioSource myAudioSource;
    //public AudioClip sandyPower;
    public AudioClip jumpersound;
    public AudioClip keycardPickup;
    Rigidbody2D myRigid;

	public Animator anim;


    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("isFacingRight", isFacingRight);
        myAudioSource = GetComponent<AudioSource>();
        myRigid = GetComponent<Rigidbody2D>();
    }
    IEnumerator Jump()
    {
        myAudioSource.Stop();
        myAudioSource.clip = jumpersound;
        myAudioSource.Play();
        if (!isGrounded && isJumpingOne) //DoubleJump
        {
            print("Jump 2");
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpSpeed);
            isJumpingOne = true;

            yield return new WaitForSeconds(nextJump);
            isJumpingOne = false;
            //isGrounded = true;
        }
        if (isGrounded)
        {
            print("Jump 1");
            isGrounded = false;
            //anim.SetBool("IsGrounded", isGrounded);
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpSpeed);

            isJumpingOne = true;
            //anim.SetBool("IsGrounded", isGrounded);
            yield return new WaitForSeconds(secondJump);
            isGrounded = true;
        }
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
            //CheckDirection(speed);
            float move = Input.GetAxis("Horizontal");
            float speed = (move * maxSpeed);
            anim.SetFloat("speed", speed);
            GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

            CheckDirection(speed);
            //Check for One Jump
            //Calls jump if button pressed, and is Grounded
            if (Input.GetKeyDown(KeyCode.Space))
            {
                print("Update Jump");
                StartCoroutine(Jump());
            }
            CheckDirection(speed);
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
        }
    }
    void SetActive()
    {
        isActive = true;
        myRigid.constraints = RigidbodyConstraints2D.None;
        myRigid.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    void SetInactive()
    {
        isActive = false;
        myRigid.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
