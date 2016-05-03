using UnityEngine;
using System.Collections;

public class LightBeams : MonoBehaviour
{
    public Transform origin;            //Use this to grab the initial light renderer to change the position size and add one based on current position
    public Transform lastNode;          //Set up the position for the next node by taking the last node and subtract the next node
    public Transform nextNode;          

    LineRenderer lineRenderer;          //Component that is grabbed from the origin which is the only GO that can have the Line Renderer component
    public int mirrorPosition;          //Must be in concurrent with light direction otherwise lights don't work

    public GameObject[] mirrors;
    Rigidbody2D mirrorRigid;

    int lightPositions;                 //Used as the index for the array of mirrors in the level
    float counter;                      //Part of the animation which doesnt work as of yet (04/04/2016)
    float dist;                         //Creates the value of the length of the line renderer that is needed to reflect and actually collide with the mirror
    public int scale;

    /* * * * * * * * * * * * * * * * * * * * * * * * * * *
     *                                                   *
     *  Once the mirror is placed it MUST be locked in   *
     *  place else the line renderer gets messed up      *
     *                                                   *
     * * * * * * * * * * * * * * * * * * * * * * * * * * */

    public bool setLaser = false;       //Turns the laser on when the mirror is placed correctly in the spot designated for it 
    public float lineDrawSpeed = 6.0f;

    /* * * * * * * * * * * * * * * * * * * * * * * * * * *
     *                                                   *
     *  Must be declared first on the origin, vertices   *
     *  CANNOT be modified during runtime, please make   *
     *  sure that you have the maximum needed, more can  *
     *  added if you feel you need more. The script is   *
     *  set up to include more if need be, just don't    *
     *  go overboard with the number of vertices         *
     *                                                   *
     * * * * * * * * * * * * * * * * * * * * * * * * * * */

    public int maxVertices;              //Subtract the current vertex from the max to set all further positions
    
    void Start()
    {
        //mirrors = GameObject.FindGameObjectsWithTag("Mirror");
        lightPositions = mirrorPosition;
        if (origin)
        {
            lineRenderer = origin.GetComponent<LineRenderer>();
        }
        if (lastNode && nextNode)           //Any node in the middle
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
        /*foreach(GameObject go in mirrors)
        {
            go.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }
        mirrors[0].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;*/
    }
    void Update()
    {
        if (setLaser)
        {
            //Part of the animation to draw the line. Still in progress or may be excluded (04/04/2016)
            if (counter < dist)
            {
                counter += .1f / lineDrawSpeed;

                float x = Mathf.Lerp(0, dist, counter);
                print(Mathf.Lerp(0, dist, counter));

                //After setting up the next position and drawing the line, Sets all the next vertices in the list temporarily so the line renderer doesn't act up
                lineRenderer.SetPosition(mirrorPosition, new Vector3(((transform.position.x - origin.position.x) / (scale*scale)), ((transform.position.y - origin.position.y) / (scale * scale)), 0));
                print("X with Scale: " + transform.position.x / scale);
                print("Y with Scale: " + transform.position.y / scale);
                for (int i = mirrorPosition; i < maxVertices; i++)
                {
                    lineRenderer.SetPosition(i, new Vector3((transform.position.x - origin.position.x), (transform.position.y - origin.position.y), 0));
                    mirrors[++i].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                }
            }
        }
        //We only want a brief time to open the laser up, so turn it off as soon as the light is drawn
        setLaser = false;
    }
}