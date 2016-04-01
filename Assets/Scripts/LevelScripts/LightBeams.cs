using UnityEngine;
using System.Collections;

public class LightBeams : MonoBehaviour
{
    public Transform origin;            //Use this to grab the initial light renderer to change the position size and add one based on current position
    public Transform lastNode;
    public Transform nextNode;
    LineRenderer lineRenderer;
    public int mirrorPosition;          //Must be in concurrent with light direction otherwise lights don't work


    int lightPositions;
    float counter;
    float dist;

    public bool setLaser = false;
    public float lineDrawSpeed = 6.0f;
    public int maxVertices;              //Subtract the current vertex from the max to set all further positions
    
    void Start()
    {
        print("Mirror position is: " + mirrorPosition);
        lightPositions = mirrorPosition;
        if (origin)
        {
            lineRenderer = origin.GetComponent<LineRenderer>();
        }
        if (lastNode && nextNode)
        {
            dist = Vector2.Distance(lastNode.position, nextNode.position);
        }
        else if (lastNode && !nextNode)     //Last Node
        {
            dist = Vector2.Distance(lastNode.position, transform.position);
        }
        else                //Origin
        {
            lineRenderer = GetComponent<LineRenderer>();
            //lineRenderer.SetPosition(mirrorPosition, transform.position);
        }
    }
    void Update()
    {
        if (setLaser)
        {
            if (counter < dist)
            {
                counter += .1f / lineDrawSpeed;

                float x = Mathf.Lerp(0, dist, counter);
                print(Mathf.Lerp(0, dist, counter));
                //Vector3 pointA = lastNode.position;
                //Vector3 pointB = nextNode.position;

                //Vector3 pointAlongLine = x * Vector3.Normalize(pointB - pointA) + pointA;

                lineRenderer.SetPosition(mirrorPosition, new Vector3((transform.position.x - origin.position.x), (transform.position.y - origin.position.y), 0));
                for (int i = ++mirrorPosition; i < maxVertices; i++)
                {
                    lineRenderer.SetPosition(i, new Vector3((transform.position.x - origin.position.x), (transform.position.y - origin.position.y), 0));
                }
            }
        }
        setLaser = false;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Mirror")
        {
            setLaser = true;
            //lineRenderer.SetPosition(lightPositions, transform.position);
        }
    }
}