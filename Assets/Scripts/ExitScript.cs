using UnityEngine;
using System.Collections;

public class ExitScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        print(other.gameObject.name);
        if (other.gameObject.tag == "Player")
        {
            print(other.gameObject.name);
            Destroy(other.gameObject);
        }
    }
}
