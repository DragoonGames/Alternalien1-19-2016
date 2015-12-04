using UnityEngine;
using System.Collections;

public class pistonMovement : MonoBehaviour {

	IEnumerator pistonSwitch;

	void Start(){
		pistonSwitch = pistonMove ();
		StartCoroutine (pistonSwitch);
	}

	// Update is called once per frame
	void Update () {
	/*	while (up){
			count++;
			transform.Translate (Vector3.up * Time.deltaTime * 0.5f);

			print (count);
			if(count >= 10)
				up = false;
		}
		while (up == false) {
			count--;
			transform.Translate(Vector3.down * Time.deltaTime * 0.5f);
			print (count);
			if(count <= -0.5f)
				up = true;
		}*/
	}

	public IEnumerator pistonMove(){
		print ("start Coroutine");
		int count = 0;
		while (count <= 10){
			print (count);
			GameObject.Find("piston").transform.Translate (Vector3.up * Time.deltaTime);
			count++;
		}
		yield return null;
		count++;
		while(count > 10 && count <= 20){
			GameObject.Find("piston").transform.Translate(Vector3.down * Time.deltaTime);
			count++;
		}
		yield return null;

		StartCoroutine(pistonSwitch);
	}
}
