using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ControlScript : MonoBehaviour {
	public GameObject[] aliens;
    public GameObject[] accessCards;
    //public List<GameObject> accessCardsList;
	public int accessCardsToGet;
	public int lockedAliens = 0;
	private int activeAlien = 0;
	private int alienCap;
	public GameObject exitGateway;
	public bool firstLevel;

    void SetActiveAlien() {
		foreach(GameObject alien in aliens)
			alien.transform.SendMessage ("SetInactive");

		aliens [activeAlien].SendMessage ("SetActive");
		target = aliens [activeAlien].transform;
	}

	void Start () {
		alienCap = aliens.Length - lockedAliens;
		SetActiveAlien ();
		target = firstTarget;
        //accessCardsList.AddRange(accessCards);
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (Input.GetKeyDown(KeyCode.Q))
        {
			activeAlien++;
			activeAlien %= alienCap;
			SetActiveAlien();
		}
		if (Input.GetKeyDown (KeyCode.E))
        {
			activeAlien += alienCap-1; // (modulo friendly equivalent to --)
			activeAlien %= alienCap;
			SetActiveAlien();
		}
        UnlockExit();
	}

	void UnlockExit()
	{
        if (accessCardsToGet == 0)
        {
            if (exitGateway.gameObject != null)
                Destroy(exitGateway);
        }
        accessCardsToGet = accessCards.Length;
        for (int i=0; i < accessCards.Length; i++)
        {
            if (accessCards[i].gameObject == null)
            {
                accessCardsToGet--;
            }
        }
		
    }
	void UnlockAliens() {
		alienCap = aliens.Length;
	}

	/*
     This camera smoothes out rotation around the y-axis and height.
     Horizontal Distance to the target is always fixed.
     
     There are many different ways to smooth the rotation but doing it this way gives you a lot of control over how the camera behaves.
     
     For every of those smoothed values we calculate the wanted value and the current value.
     Then we smooth it using the Lerp function.
     Then we apply the smoothed values to the transform's position.
     */
	
	// The target we are following
	public Transform firstTarget;
	Transform target;
	// The distance in the x-z plane to the target
	public float distance = 100.0f;
	// the height we want the camera to be above the target
	public float height = 25.0f;
	// How much we 
	public float heightDamping = 25.0f;
	public float rotationDamping = 25.0f;
    public float widthDamping = 25.0f;
	
	void  LateUpdate ()
	{
		// Early out if we don't have a target
		if (!target)
			return;
		if (!firstLevel) {
			// Calculate the current rotation angles
			// wantedRotationAngle = target.eulerAngles.y;
			float wantedHeight = target.position.y - height;
			//float currentRotationAngle = transform.eulerAngles.y;
			float currentHeight = transform.position.y;
		
			// Damp the rotation around the y-axis
			//currentRotationAngle = Mathf.LerpAngle (currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
		
			// Damp the height
			currentHeight = Mathf.Lerp (currentHeight, wantedHeight, heightDamping * Time.deltaTime);
		
			// Convert the angle into a rotation
			//Quaternion currentRotation = Quaternion.Euler (0, currentRotationAngle, 0);
		
			// Set the position of the camera on the x-z plane to:
			// distance meters behind the target
			transform.position = target.position;
			transform.position -= Vector3.forward * distance;
		
			// Set the height of the camera
			transform.position = new Vector3 (transform.position.x, currentHeight, transform.position.z);
		
			// Always look at the target
			transform.LookAt (target);
		}
	}
}
