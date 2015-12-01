using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class bioClick : MonoBehaviour {

	public SpriteRenderer image;
    //bool isDisabled;
    bool isFullScreen;
    Quaternion storedRotation;
    Vector3 storedPosition;
    Vector3 storedScale;

	Vector3 newPosition;
	void Start(){
        image = GetComponent<SpriteRenderer>();
        //isDisabled = false;
        isFullScreen = false;
        storedRotation = GetComponent<Transform>().rotation;
        storedPosition = GetComponent<Transform>().position;
        storedScale = GetComponent<Transform>().localScale;
	}
    void Update()
    {

    }
	void OnMouseDown() 
	{
        if (!isFullScreen)
        {
            print(gameObject);
            //image.enabled = false;
            transform.position = new Vector3(0, 0, -9);
            transform.localScale = new Vector3(20, 20, 0);
            transform.rotation = new Quaternion(0, 0, 0, 0);
            isFullScreen = true;
            //isDisabled = true;
        }
        else
        {
            print(gameObject);
            isFullScreen = false;
            transform.position = storedPosition;
            transform.localScale = storedScale;
            transform.rotation = storedRotation;
            //image.enabled = true;
            //isDisabled = false;
        }
    }
}
