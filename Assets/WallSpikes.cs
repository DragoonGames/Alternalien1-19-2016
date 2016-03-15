using UnityEngine;
using System.Collections;

public class WallSpikes : MonoBehaviour {

    public float timer          = 0.5f;
    public float speed          = 5.0f;
    public float maxDistance    = 10.0f;

    private bool trap           = false;
    private Vector3 originalPos;

    void Start() {
        originalPos = transform.position;
    }
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;                // Deprecate time;
        if (timer <= 0) {                       // When the timer ends...
            trap = true;                        // The trap is activated               
            timer = 6.0f;                       // Reset timer;
        }

        if (trap) {
            transform.Translate(Vector2.right * Time.deltaTime * speed);   // Translate the gameObject to the right.
        }

        if (transform.position.x >= maxDistance) {       // If spike is past the max point/ wall
            trap = false;                       // the movement is no longer active
            transform.position = originalPos;   // Reset position.
        }
    }
}
