using UnityEngine;
using System.Collections;

public class ExitIntroScene : MonoBehaviour {

<<<<<<< HEAD
    AudioSource myAudio;
    public float delayAudio = 0;

	void Start(){
		StartCoroutine (EndIntro());
        myAudio = GetComponent<AudioSource>();
        myAudio.SetScheduledStartTime((double)delayAudio);
        myAudio.Play();

    }
=======
	public Animator anim;
	public float timer;
	public float pauseTimer;

	void Start(){
		StartCoroutine (EndIntro());
		//bkgEnd.GetComponent<SpriteRenderer>().enabled = false;
		//anim = GetComponent<Animator> ();
	}
>>>>>>> refs/remotes/origin/FixingAnimations

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
