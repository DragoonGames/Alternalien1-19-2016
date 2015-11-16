using UnityEngine;
using System.Collections;

public class LennyMovement : MonoBehaviour {

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
    public bool alwaysPickup = false;
    public Transform[] itemTransform;
    int[] itemTransformScalesX;
    int[] itemTransformScalesY;
    private float inRange;
    private bool triggered;
    private bool released;
    int index = 0;

    void Start()
    {
        /*anim = GetComponent<Animator>();
        anim.SetBool("IsGrounded", isGrounded);
        anim.SetBool("IsFacingLeft", isFacingLeft);
        anim.SetBool("IsFacingRight", isFacingRight);
        */
        inRange = 50.0f;
        triggered = false;
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
            if (triggered)
                itemTransform[index].localPosition = new Vector3(1.5f, 0.5f, 0.0f);
            
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
            if (triggered)
                itemTransform[index].localPosition = new Vector3(-1.5f, 0.5f, 0.0f);
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
            if (!released && Input.GetKeyDown(KeyCode.C))
            {
                Released();
            }
            for (int i = 0; i < itemTransform.Length; i++)
            {
                
                if (Vector3.Distance(itemTransform[i].position, transform.position) < inRange)
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
                itemTransform[index].Translate(0, 1, 0);
            }
        }
    }
    void Trigger()
    {
        print("trigger pressed");
        triggered = true;
        released = false;

        if (alwaysPickup)
        {
            print("in if statement");
            //Handle picking the item here (i.e. parent/child move to empty GO
        }
        else
        {
            print("in else statement");
            //Put display Icon here
            itemTransform[index].transform.parent = transform;
            print(transform.localPosition);
            print(itemTransform[index].transform.localScale);
            Rigidbody2D itemRigid = itemTransform[index].GetComponent<Rigidbody2D>();
            Destroy(itemRigid);
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
    }
    void Released()
    {
        triggered = false;
        released = true;

        if(!alwaysPickup)
        {
            if (isFacingLeft)
            {
                print("Detach");
                itemTransform[index].transform.parent = null;
            }
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
