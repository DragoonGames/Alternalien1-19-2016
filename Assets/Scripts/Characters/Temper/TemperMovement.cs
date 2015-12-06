using UnityEngine;
using System.Collections;

public class TemperMovement : MonoBehaviour 
{

    public float maxSpeed = 10f;
    public float jumpSpeed = 100f;
    private bool isActive = false;

    public Rigidbody2D fireballRigid;
    public Rigidbody2D iceballRigid;
    public float shotPower = 80;

    bool isFacingRight = true;
    bool isFacingLeft = false;
    bool fireMode = true;
    bool iceMode = false;
    bool isGrounded = true;
    private bool isVine = false;

    private float jumpRate = 0.25F;
    public float nextJump = 5.0F;

    public Sprite leftFireSprite, rightFireSprite;
    public Sprite leftIceSprite, rightIceSprite;

    Rigidbody2D temperRigid;
    Rigidbody2D myRigid;

    AudioSource myAudioSource;
    public AudioClip fireballSound;
    public AudioClip iceballSound;
    public AudioClip switchToIceSound;
    public AudioClip switchToFireSound;
    public AudioClip keycardPickup;

    void Start()
    {
        temperRigid = GetComponent<Rigidbody2D>();
        myAudioSource = GetComponent<AudioSource>();
        myRigid = GetComponent<Rigidbody2D>();
    }
    IEnumerator Jump()
    {
        //isGrounded = false;
        print(temperRigid.gravityScale);
        //anim.SetBool("IsGrounded", isGrounded);
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpSpeed);
        temperRigid.gravityScale += 5;
        yield return new WaitForSeconds(nextJump);

        StartCoroutine(StopJumping());
        print(isGrounded);
        //isGrounded = true;
        //anim.SetBool("IsGrounded", isGrounded);
    }
    IEnumerator StopJumping()
    {
        isGrounded = false;
        temperRigid.gravityScale = 15;
        yield return new WaitForSeconds(0.5f);
        isGrounded = true;
    }
    void LaunchFireIceBall()
    {
        if (fireMode)
        {
            if (isFacingRight)
            {
                Rigidbody2D fireClone;
                myAudioSource.Stop();
                myAudioSource.clip = fireballSound;
                myAudioSource.Play();
                fireClone = Instantiate(fireballRigid, transform.position, transform.rotation) as Rigidbody2D;
                fireClone.velocity = transform.TransformDirection(Vector3.right * 120 + Vector3.up * 35);
                
                Destroy(fireClone.gameObject, 15.0f);
            }
            else if (isFacingLeft)
            {
                Rigidbody2D fireClone;
                myAudioSource.Stop();
                myAudioSource.clip = fireballSound;
                myAudioSource.Play();
                fireClone = Instantiate(fireballRigid, transform.position, transform.rotation) as Rigidbody2D;
                fireClone.velocity = transform.TransformDirection(Vector3.left * 120 + Vector3.up * 35);

                Destroy(fireClone.gameObject, 15.0f);
            }
        }
        else if (iceMode)
        {
            if (isFacingRight)
            {
                Rigidbody2D iceClone;
                myAudioSource.Stop();
                myAudioSource.clip = iceballSound;
                myAudioSource.Play();
                iceClone = Instantiate(iceballRigid, transform.position, transform.rotation) as Rigidbody2D;
                iceClone.velocity = transform.TransformDirection(Vector3.right * 120 + Vector3.up * 35);

                Destroy(iceClone.gameObject, 15.0f);
            }
            else if (isFacingLeft)
            {
                Rigidbody2D iceClone;
                myAudioSource.Stop();
                myAudioSource.clip = iceballSound;
                myAudioSource.Play();
                iceClone = Instantiate(iceballRigid, transform.position, transform.rotation) as Rigidbody2D;
                iceClone.velocity = transform.TransformDirection(Vector3.left * 120 + Vector3.up * 35);

                Destroy(iceClone.gameObject, 15.0f);
            }

        }
    }
    void CheckDirection()
    {
        if (fireMode)
        {
            if (isFacingRight)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
                {
                    isFacingLeft = true;
                    isFacingRight = false;
                    //Start of Changing Sprites
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = leftFireSprite;
                }

            }
            if (isFacingLeft)
            {
                if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
                {
                    isFacingLeft = false;
                    isFacingRight = true;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = rightFireSprite;
                }
            }
        }
        else if (iceMode)
        {
            if (isFacingLeft)
            {
                if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
                {
                    isFacingLeft = false;
                    isFacingRight = true;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = rightIceSprite;
                }
            }
            else if (isFacingRight)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
                {
                    isFacingLeft = true;
                    isFacingRight = false;
                    //Start of Changing Sprites
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = leftIceSprite;
                }

            }
        }
    }
    void CheckTemperature()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (fireMode)
            {
                myAudioSource.Stop();
                myAudioSource.clip = switchToIceSound;
                myAudioSource.Play();
                fireMode = false;
                iceMode = true;
                if (isFacingLeft)
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = leftIceSprite;
                }
                else if (isFacingRight)
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = rightIceSprite;
                }
            }
            else if (iceMode)
            {
                myAudioSource.Stop();
                myAudioSource.clip = switchToFireSound;
                myAudioSource.Play();
                iceMode = false;
                fireMode = true;
                if (isFacingLeft)
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = leftFireSprite;
                }
                else if (isFacingRight)
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = rightFireSprite;
                }

            }
        }
    }
    void Update()
    {
        if (isActive)
        {
            float move = Input.GetAxis("Horizontal");
            float speed = (move * maxSpeed);
            //anim.SetFloat("speed", speed);
            GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

            if (isVine)
            {
                float vertical = Input.GetAxis("Vertical");
                GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, vertical * maxSpeed);
            }
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                StartCoroutine(Jump());
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                LaunchFireIceBall();
            }
            CheckDirection();
            CheckTemperature();
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
        myRigid.constraints = RigidbodyConstraints2D.None;
        myRigid.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    void SetInactive()
    {
        isActive = false;
        myRigid.constraints = RigidbodyConstraints2D.FreezePositionX;
    }
}
