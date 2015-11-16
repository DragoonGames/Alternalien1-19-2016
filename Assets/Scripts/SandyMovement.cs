using UnityEngine;
using System.Collections;

public class SandyMovement : MonoBehaviour {

    bool isFacingRight = true;
    bool isFacingLeft = false;

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
    public float timeToScale = 0.001f;
    private float sandyTimeScale = 1f;

    void Start()
    {
        /*anim = GetComponent<Animator>();
        anim.SetBool("IsGrounded", isGrounded);
        anim.SetBool("IsFacingLeft", isFacingLeft);
        anim.SetBool("IsFacingRight", isFacingRight);
        */
        triggered = false;
        Time.timeScale = 1;
    }
    IEnumerator Jump()
    {
        isGrounded = false;
        //anim.SetBool("IsGrounded", isGrounded);
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpSpeed);

        yield return new WaitForSeconds(nextJump);

        isGrounded = true;
        //anim.SetBool("IsGrounded", isGrounded);
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
                /*anim.SetFloat("speed", moveSpeed);
                anim.SetBool("IsFacingRight", isFacingRight);
                anim.SetBool("IsFacingLeft", isFacingLeft);
                */
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
                /*anim.SetFloat("speed", moveSpeed);
                anim.SetBool("IsFacingRight", isFacingRight);
                anim.SetBool("IsFacingLeft", isFacingLeft);
                */
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
            if (Input.GetButton("Jump") && isGrounded)
            {
                StartCoroutine(Jump());
            }
            CheckDirection(speed);
            if (Input.GetKeyDown(KeyCode.F))
            {
                Trigger();
            }
        }
    }
    void Trigger()
    {
        print("trigger pressed");
        triggered = true;
        released = false;

        Time.timeScale = timeToScale;
        //Put display Icon here
    }
    void Released()
    {
        triggered = false;
        released = true;
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
