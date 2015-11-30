using UnityEngine;
using System.Collections;

public class Elevator : MonoBehaviour {

	public int floors;
	public float floorHeight;
	public float speed;

	private float startHeight;
	private bool moving;
	private float targetHeight;
	private float distancePerFrame;
	
	void Start () {
		distancePerFrame = speed / 60;
		startHeight = transform.position.y;
	}

	void setHeight(float height) {
		transform.position = new Vector2 (transform.position.x, height);
	}

	void FixedUpdate () {
		if (moving) {
			if(Mathf.Abs(transform.position.y - targetHeight) < distancePerFrame) {
				setHeight (targetHeight);
				moving = false;
			} else {
				if(targetHeight < transform.position.y)
					setHeight(transform.position.y - distancePerFrame);
				else
					setHeight(transform.position.y + distancePerFrame);
			}
		}
	}

	public void gotoFloor(int floor) {
		moving = true;
		targetHeight = startHeight + floorHeight * (floor-1);
	}
}
