using UnityEngine;
using System.Collections;

public class SlimeMovement : MonoBehaviour 
{
	public float maxSpeed = 10f;
	public float jumpSpeed = 1000f;
	private bool isActive = false;
	public GameObject[] leftWalls;
	public GameObject[] rightWalls;
	enum SlimeDirection
	{
		isLeft,
		isRight,
		isUpsideDown,
		isNormal
	};
	bool isLeft = false;
	bool isUpsideDown = false;
	float move = 0;

	void Start(){

	}
	void Jump() {
		GetComponent<Rigidbody2D>().AddForce (Vector2.up * jumpSpeed);
	}
	
	void FixedUpdate () {

		if (isActive && !isLeft) {
			move = Input.GetAxis("Horizontal");
			GetComponent<Rigidbody2D>().velocity = new Vector2 (move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
			}

		if(isActive && isLeft){
			//float moveUp = Input.GetAxis("Vertical");
			move = Input.GetAxis("Vertical");
			GetComponent<Rigidbody2D>().velocity = new Vector2 (GetComponent<Rigidbody2D>().velocity.x, move * maxSpeed);
			if (Input.GetButtonDown ("Jump"))
				Jump ();
		}
	}
	void SetActive() {
		isActive = true;
	}
	void SetInactive() {
		isActive = false;
	}
	void OnTriggerStay2D(Collider2D other) {
		if (other.gameObject.tag == "Wall") {
			//Climb wall to the left
			if (Input.GetKeyDown (KeyCode.UpArrow) && !isLeft && !isUpsideDown) {
				transform.Rotate (0, 0, 270);
				//GetComponent<Rigidbody2D>().useGravity = true;
				isLeft = true;
			}
			//Walk on ceiling from left wall
			if (Input.GetKeyDown (KeyCode.RightArrow) && isLeft) {
				transform.Rotate (0, 0, 270);
				print ("Upside Down");
				//GetComponent<Rigidbody2D>().useGravity = false;
				isLeft = false;
				isUpsideDown = true;
				Physics2D.gravity = Vector3.zero;
			}
			//Detach from left Wall
			if (Input.GetKeyDown (KeyCode.DownArrow) && isLeft) {
				transform.Rotate (0, 0, 90);
				Physics2D.gravity = new Vector3 (1, 1, 0);
				isLeft = false;
			}
			if (Input.GetKeyDown (KeyCode.DownArrow) && isUpsideDown) {
				transform.Rotate (0, 0, 180);
				print ("Right Side Up");
				Physics2D.gravity = new Vector2 (0, -10);
				isUpsideDown = false;
			}
		}
	}
	void OnTriggerExit2D(Collider2D other){
		if (isUpsideDown) {
			transform.Rotate(0,0,180);
			isUpsideDown = false;
			Physics2D.gravity = new Vector2(0,-10);
		}
	}
}
