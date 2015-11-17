using UnityEngine;
using System.Collections;

public class AddTemper : MonoBehaviour {

    public GameObject UnlockObject;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (UnlockObject.gameObject == null)
        {
            Camera.main.SendMessage("UnlockAliens");
        }
	}
}
