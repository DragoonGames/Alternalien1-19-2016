using UnityEngine;
using System.Collections;

public class BridgeMaking : MonoBehaviour {

    public GameObject DestroyedObject;
    public GameObject EnableObject;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (DestroyedObject == null)
        {
            print("Destroyed");
            EnableObject.SetActive(true);
        }
	}
}
