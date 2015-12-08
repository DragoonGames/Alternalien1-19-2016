using UnityEngine;
using System.Collections;

public class CloneMovement : MonoBehaviour {

    bool isFacingRight = true;
    bool isFacingLeft = false;

    public float maxSpeed = 10f;
    public float jumpSpeed = 100f;
    private bool isActive = false;
    bool isGrounded = true;

    private float jumpRate = 0.25F;
    public float nextJump = 0.5F;

    public Animator anim;
    public Transform[] itemTransform;
    public Sprite CloneCopyCat;
    public Sprite OriginalClone;
    private SpriteRenderer spriteRenderer;
    AudioSource myAudioSource;

    private BoxCollider2D originalCollider;
    private Vector3 storedColliderSize;
    private Vector2 storedColliderOffset;
	private Vector3 originalScale;
	private Vector3 itemTransformScale;
    public AudioClip keycardPickup;
    Rigidbody2D myRigid;
    Rigidbody2D itemRigid;

    public float inRange;
    bool isUsingPower = false;
    private bool triggered;
    private bool released;
    int index = 0;

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isFacingRight", isFacingRight);
        anim.SetBool("isUsingPower", isUsingPower);
		inRange = 10 * transform.localScale.x;
        triggered = false;

        spriteRenderer = GetComponent<SpriteRenderer>();
		originalScale = transform.localScale;
        originalCollider = gameObject.GetComponent<BoxCollider2D>();
        storedColliderSize = GetComponent<BoxCollider2D>().size;
        storedColliderOffset = GetComponent<BoxCollider2D>().offset;
        OriginalClone = transform.GetComponent<SpriteRenderer>().sprite;

        myAudioSource = GetComponent<AudioSource>();
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
    void CheckDirection(float moveSpeed)
    {
        if (isFacingRight)
        {
            anim.SetBool("isFacingRight", isFacingRight);

            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                isFacingLeft = true;
                isFacingRight = false;
                //Start of Changing Sprites
                anim.SetFloat("speed", moveSpeed);
                anim.SetBool("isFacingRight", isFacingRight);
            }

        }
        if (isFacingLeft)
        {
            anim.SetBool("isFacingRight", isFacingRight);

            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                isFacingLeft = false;
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
            if (Input.GetButton("Jump") && isGrounded)
            {
                StartCoroutine(Jump());
            }
            CheckDirection(speed);
            if (triggered && Input.GetKeyDown(KeyCode.C))
            {
                isUsingPower = false;
                Released();
            }
            /*if (Input.GetKeyDown(KeyCode.F) && !triggered)
            {
                isUsingPower = true;
                anim.SetBool("isUsingPower", isUsingPower);
                triggered = true;
            }*/
            if (!triggered)
            {
                for (int i = 0; i < itemTransform.Length; i++)
                {
                    if (Vector3.Distance(itemTransform[i].position, transform.position) < inRange)
                    {
                        print("In Range");
                        if (Input.GetKeyDown(KeyCode.F))
                        {
                            //isUsingPower = true;
                            //anim.SetBool("isUsingPower", isUsingPower);
							print("Button pressed");
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
                            //Destroy(originalCollider);
                            index = i;
                            Trigger();
                        }
                    }
                }
            }
        }
    }
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
        originalCollider.size = new Vector3(.5f, .5f, 0);
        originalCollider.offset = new Vector2(0, 0);
        //gameObject.GetComponent<BoxCollider2D>() = itemTransform[index].GetComponent<Collider2D>();
        //gameObject.AddComponent<BoxCollider2D>(clonedCollider);
    }
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
    void SetActive()
    {
        isActive = true;
        myRigid.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    void SetInactive()
    {
        isActive = false;
    }
}
