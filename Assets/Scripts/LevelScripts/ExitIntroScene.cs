﻿using UnityEngine;
using System.Collections;

public class ExitIntroScene : MonoBehaviour {

	public Animator anim;
	public float timer;
	public float pauseTimer;

	void Start(){
		StartCoroutine (EndIntro());
		//bkgEnd.GetComponent<SpriteRenderer>().enabled = false;
		//anim = GetComponent<Animator> ();
	}

	IEnumerator EndIntro(){
		print ("Intro Starts");
		yield return new WaitForSeconds (timer);
		anim.enabled = false;
		yield return new WaitForSeconds (pauseTimer);
		//bkgEnd.GetComponent<SpriteRenderer>().enabled = true;
		//yield return new WaitForSeconds (3);
		Application.LoadLevel(1);
	}
}
