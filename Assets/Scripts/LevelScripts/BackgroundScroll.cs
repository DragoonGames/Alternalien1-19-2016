using UnityEngine;
using System.Collections;

public class BackgroundScroll : MonoBehaviour
{
    Renderer rend;
    public float scrollSpeed;

	// Use this for initialization
	void Start ()
    {
        rend = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        rend.material.SetTextureOffset("_MainTex", new Vector2(0, Time.time * scrollSpeed));
    }
}
