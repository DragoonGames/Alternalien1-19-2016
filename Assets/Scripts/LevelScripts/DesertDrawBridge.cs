using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DesertDrawBridge : MonoBehaviour {

	public float DbRotation = 85.5f;
	public bool swingDown;
	public GameObject[] accessCards;
	public int count;

	void Start(){
		DbRotation -= 89.5f;
		transform.rotation = Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, DbRotation);
		accessCards = GameObject.FindGameObjectsWithTag ("isCardKey");
		count = 0;
		swingDown = false;
	}
	
	void Update(){
		DbRotation = Mathf.Clamp (DbRotation, 1, 89.5f);

		for (int i = 0; i < accessCards.Length; i++) {

			if (accessCards [i] = null) {
				count++;
			}
			if (count == accessCards.Length) {
				swingDown = true;
			}
		}
		if (swingDown){
			DbRotation += .5f;
			transform.rotation = Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, DbRotation);
		} 
	}
}
