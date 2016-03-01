using UnityEngine;
using System.Collections;

public class ExitIntroScene : MonoBehaviour {

    AudioSource myAudio;
    public float delayAudio = 0;

	void Start(){
		StartCoroutine (EndIntro());
        myAudio = GetComponent<AudioSource>();
        myAudio.SetScheduledStartTime((double)delayAudio);
        myAudio.Play();

    }

    IEnumerator EndIntro(){
		print ("Intro Starts");
		yield return new WaitForSeconds (3);
		Application.LoadLevel(1);
	}
}
