using UnityEngine;
using System.Collections;

public class ResetMirrors : MonoBehaviour
{
    LineRenderer lineRenderer;          //Component that is grabbed from the origin which is the only GO that can have the Line Renderer component
    public GameObject[] allMirrors;
    public int maximumBends;
    public LightBeams myBeams;
    public GameObject origin;

	// Use this for initialization
	void Start ()
    {
        origin = GameObject.FindGameObjectWithTag("Origin");
        allMirrors = GameObject.FindGameObjectsWithTag("Mirror");
        myBeams = origin.GetComponent<LightBeams>();
        maximumBends = myBeams.maxVertices;
        lineRenderer = origin.GetComponent<LineRenderer>();             //Use this to reset all the lights if chosen
    }
	
	// Update is called once per frame
	void Update ()
    {

	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            for(int i = 0; i < allMirrors.Length; i++)
            {
                allMirrors[i].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                lineRenderer.SetPosition(i, Vector3.zero);
            }
        }
    }
}
