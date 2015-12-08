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
		print (accessCards [0]);
		print (accessCards [3]);
	}
	
	void Update(){
		DbRotation = Mathf.Clamp (DbRotation, 1, 89.5f);

			if (count == accessCards.Length) {
				swingDown = true;
			}
		if (swingDown){
			DbRotation += .5f;
			transform.rotation = Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, DbRotation);
		} 
	}
	void DropDown()
	{
		for (int i = 0; i < accessCards.Length; i++) {
			count = 0;
			if (accessCards [i] = null) {
				count++;
			}
			if (count == accessCards.Length) {
				swingDown = true;
			}
		}
	}
}
