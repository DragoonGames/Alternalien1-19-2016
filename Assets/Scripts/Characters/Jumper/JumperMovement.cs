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
    AudioSource jumperSound;

    void Start()
    {
        //anim = GetComponent<Animator>();
        //anim.SetBool("IsFacingLeft", isFacingLeft);
        //anim.SetBool("IsFacingRight", isFacingRight);
        jumperSound = GetComponent<AudioSource>();
    }
    IEnumerator Jump()
    {
        jumperSound.Play();
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
            //anim.SetBool("IsFacingRight", isFacingRight);
            //anim.SetBool("IsFacingLeft", isFacingLeft);

            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                isFacingLeft = true;
                isFacingRight = false;
                //Start of Changing Sprites
                //anim.SetFloat("speed", moveSpeed);
                //anim.SetBool("IsFacingRight", isFacingRight);
                //anim.SetBool("IsFacingLeft", isFacingLeft);
            }

        }
        if (isFacingLeft)
        {
            //anim.SetBool("IsFacingLeft", isFacingLeft);
            //anim.SetBool("IsFacingRight", isFacingRight);

            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                isFacingLeft = false;
                isFacingRight = true;
                //Start of Changing Sprites
                //anim.SetFloat("speed", moveSpeed);
                //anim.SetBool("IsFacingRight", isFacingRight);
                //anim.SetBool("IsFacingLeft", isFacingLeft);
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
            //anim.SetFloat("speed", speed);
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
            Destroy(c.gameObject);
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
