using UnityEngine;
using System.Collections;

public class RevisedCrushMovement : MonoBehaviour {

    bool isFacingRight = true;
    bool isFacingLeft = false;

    public float maxSpeed = 10f;
    public float jumpSpeed = 100f;
    private bool isActive = false;
    bool isGrounded = true;
    bool isPunching = false;

    private float jumpRate = 0.25F;
    public float nextJump = 0.0F;
    public Animator anim;

    //Variables for powers
    public GameObject[] taggedObjects;  //Gather all tags for Lenny
    private float inRange;
    public bool triggered;
    public bool released;
    int index = 0;

    AudioSource myAudioSource;
    public AudioClip keycardPickup;
    Rigidbody2D myRigid;

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("IsGrounded", isGrounded);
        anim.SetBool("IsFacingRight", isFacingRight);
        anim.SetBool("IsPunching", isPunching);
        myAudioSource = GetComponent<AudioSource>();
        myRigid = GetComponent<Rigidbody2D>();
        inRange = 50.0f;
        triggered = false;
        released = true;
        taggedObjects = GameObject.FindGameObjectsWithTag("isBreakable");
    }
    IEnumerator Jump()
    {
        isGrounded = false;
        anim.SetBool("IsGrounded", isGrounded);
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpSpeed);

        yield return new WaitForSeconds(nextJump);

        isGrounded = true;
        anim.SetBool("IsGrounded", isGrounded);
    }
    void CheckDirection(float moveSpeed)
    {
        if (isFacingRight)
        {
            anim.SetBool("IsFacingRight", isFacingRight);

            anim.SetBool("IsFacingRight", isFacingRight);
            anim.SetBool("IsFacingLeft", isFacingLeft);

            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                isFacingLeft = true;
                isFacingRight = false;
                //Start of Changing Sprites
                anim.SetFloat("speed", moveSpeed);
                anim.SetBool("IsFacingRight", isFacingRight);
                anim.SetBool("IsFacingLeft", isFacingLeft);
            }
        }
        if (isFacingLeft)
        {
            anim.SetBool("IsFacingLeft", isFacingLeft);
            anim.SetBool("IsFacingRight", isFacingRight);

            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                isFacingLeft = false;
                isFacingRight = true;
                //Start of Changing Sprites
                anim.SetFloat("speed", moveSpeed);
                anim.SetBool("IsFacingRight", isFacingRight);
                anim.SetBool("IsFacingLeft", isFacingLeft);
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
            if (Input.GetButton("Jump") && isGrounded)
            {
                StartCoroutine(Jump());
            }
            CheckDirection(speed);
            for (int i = 0; i < taggedObjects.Length; i++)
            {
                if (Vector3.Distance(taggedObjects[i].transform.position, transform.position) < inRange)
                {
                    if (released && Input.GetKeyDown(KeyCode.F))
                    {
                        index = i;
                        Trigger();
                    }
                }
            }
        }
    }
    void Trigger()
    {
        print(index);
        print("trigger pressed");
        anim.SetBool("IsPunching", isPunching);
        Destroy(taggedObjects[index]);
        triggered = true;
        released = false;
        StartCoroutine(WaitForNextPunch());
    }
    IEnumerator WaitForNextPunch()
    {
        yield return new WaitForSeconds(nextJump);
        triggered = false;
        released = true;
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
    }
    void SetInactive()
    {
        isActive = false;
    }
}
