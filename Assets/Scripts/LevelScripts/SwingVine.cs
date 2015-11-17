using UnityEngine;
using System.Collections;

public class SwingVine : MonoBehaviour {
	public float zRotation;
	public bool swingRight = true;
	
	void Update(){
		zRotation = Mathf.Clamp (zRotation, 225, 315);
		if (swingRight) {
			zRotation += .5f;
			transform.rotation = Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, zRotation);
			if(zRotation >= 315)
				swingRight = false;
		} 
		else{
			zRotation -= .5f;
			transform.rotation = Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, zRotation);
			if (zRotation <= 225)
				swingRight = true;
		}
	}
}
