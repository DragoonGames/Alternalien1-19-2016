using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class bioClick : MonoBehaviour {

	public Texture image;
	private Texture _image;

	void Start(){
		_image.enabled = false;
	}

	void Update () {
		if (Input.GetMouseButtonUp (0)) {
			if(_image.enabled = false)
				_image.enabled = true;
			else if (_image.enabled = true)
				_image.enabled = false;
		}
	}

	void OnGUI(){
		_image = GUI.DrawTexture(new Rect (0, 0, 60, 60), image, ScaleMode.ScaleToFit);
	}
}
