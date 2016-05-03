﻿using UnityEngine;
using System.Collections;

public class RightDrawBridge : MonoBehaviour 
{
	public float DbRotation = 0.0f;
	public bool swingDown;
	public GameObject[] accessCards;
	public int count;
	
	void Start(){
		//DbRotation += 89.5f;
		transform.rotation = Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, DbRotation);
		accessCards = GameObject.FindGameObjectsWithTag ("isCardKey");
		count = 4;
		swingDown = false;
	}
	
	void Update(){
		DbRotation = Mathf.Clamp (DbRotation, 270, 89.5f);
		
		if (count == accessCards.Length) {
			swingDown = true;
		}
		if (swingDown){
			DbRotation -= .5f;
			transform.rotation = Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, DbRotation);
		} 
	}
	void DropDown()
	{
		for (int i = 0; i < accessCards.Length; i++) {
			count = 4;
			if (accessCards [i] = null) {
				count++;
			}
			if (count == accessCards.Length) {
				swingDown = true;
			}
		}
	}
}
