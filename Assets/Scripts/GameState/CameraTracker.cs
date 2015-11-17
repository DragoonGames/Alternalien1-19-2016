using UnityEngine;
using System.Collections;

public class CameraTracker : MonoBehaviour {

	//How far char can move away from camera before it follows
	public float xCharRange = 1.0f;
	public float yCharRange = 1.0f;
	//smothing of camera movement
	public float xTrackSmooth = 10.0f;
	public float yTrackSmooth = 10.0f;
	//Boundaries our camera can move in X axis
	public Vector2 maxXLevel;
	public Vector2 minXLevel;

	public Vector2 maxYLevel;
	public Vector2 minYLevel;

	//Hold our Camera Track Point Transform
	public Transform xCameraTrackPoint;
	public Transform yCameraTrackPoint;

	// Update is called once per frame
	void FixedUpdate () {
		TrackPlayer ();
	}
	//Getting the distance our char is away from camera if we went past the set range we will allow
	bool CheckXCharRange(){
		return Mathf.Abs(transform.position.x - xCameraTrackPoint.position.x)> (xCharRange);
	}

	bool CheckYCharRange(){
		return Mathf.Abs(transform.position.y - yCameraTrackPoint.position.y)> (yCharRange);
	}

	//Camera tracking main action
	void TrackPlayer(){
		//Variable to store the positional info of our player camera.
		float targetX = transform.position.x;
		float targetY = transform.position.y;

		//If char is out of range then move camera to follow char until in range again.
		if (CheckXCharRange()) {
			targetX = Mathf.Lerp (transform.position.x, xCameraTrackPoint.position.x, xTrackSmooth * Time.deltaTime);
		}
		if (CheckYCharRange()) {
			targetY = Mathf.Lerp (transform.position.y, yCameraTrackPoint.position.y, yTrackSmooth * Time.deltaTime);
		}
		//Clamp our camera's range of movement
		targetX = Mathf.Clamp (targetX, minXLevel.x, maxXLevel.x);
		targetY = Mathf.Clamp (targetY, minYLevel.y, maxYLevel.y);

		//Move our Camera after all calculations are done with above..
		transform.position = new Vector3 (targetX, transform.position.y, transform.position.z);
	}
}
