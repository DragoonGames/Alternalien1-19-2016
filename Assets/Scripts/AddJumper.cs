using UnityEngine;
using System.Collections;

public class AddJumper : MonoBehaviour {

	// Use this for initialization
	void OnTriggerEnter2D(Collider2D other){
            Camera.main.SendMessage("UnlockAliens");
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
