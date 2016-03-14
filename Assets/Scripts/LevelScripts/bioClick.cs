using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class bioClick : MonoBehaviour {

	public SpriteRenderer image;

    private bool isFullScreen;                                      // Whether or not image is full size.
    private Quaternion storedRotation;                              // Original transform rotation.
    private Vector3 storedPosition;                                 // Original Transform position.
    private Vector3 storedScale;                                    // Original Transform scale.

    void Start() {
        image = GetComponent<SpriteRenderer>();
        isFullScreen = false;
        storedRotation = GetComponent<Transform>().rotation;
        storedPosition = GetComponent<Transform>().position;
        storedScale = GetComponent<Transform>().localScale;
	}

	void OnMouseDown() 
	{
        if (!isFullScreen) {                                        // If not full screen...
            transform.position = new Vector3(0, 0, -9);             // Change position, scale, & rotation to full screen.
            transform.localScale = new Vector3(20, 20, 0);
            transform.rotation = new Quaternion(0, 0, 0, 0);
            isFullScreen = true;                                    
        }

        else {                                                      // If full screen is true...
            isFullScreen = false;                                   // Set to false...
            transform.position = storedPosition;
            transform.localScale = storedScale;                     // Reset original values.
            transform.rotation = storedRotation;
        }
    }
}
