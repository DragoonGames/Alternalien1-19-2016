using UnityEngine;
using System.Collections;

public class unfairMarioSpikes : MonoBehaviour {

    public GameObject trigger;
    public GameObject destroyableBlock;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Destroy(destroyableBlock);
        }
    }
}
