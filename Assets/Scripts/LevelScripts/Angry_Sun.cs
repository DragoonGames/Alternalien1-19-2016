using UnityEngine;
using System.Collections;

public class Angry_Sun : MonoBehaviour {

    bool isActive = false;//Start the Burning
    Animator anim;
    public float sunTimer;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        if (sunTimer == 0)
        {
            sunTimer = 5;
        }
	}
	
	// Update is called once per frame
	void Update () {
        //Other movement scripts go in here.
        //otherwise we do an IEnumerator for timer
        if (isActive)
        {
            StartCoroutine(BurnTheDesert());
        }
	}
    void OnTriggerStay2D(Collider2D other)
    {
        if (isActive)
        {
            if (other.gameObject.tag == "Player")
            {
                Destroy(other.gameObject);
            }
        }
    }
    IEnumerator BurnTheDesert()
    {
        isActive = false;
        yield return new WaitForSeconds(sunTimer);
    }
}
