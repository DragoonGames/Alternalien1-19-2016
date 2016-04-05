using UnityEngine;
using System.Collections;

public class TestLightBeam : MonoBehaviour
{
    /*#pragma strict
    @script RequireComponent(LineRenderer)
    var dist : int; //max distance for beam to travel.
    var lr : LineRenderer;
    var winTag : String; // i was using for minigame, if laser touches this tag , win
    var reftag :String; //tag it can reflect off.
    var limit : int = 100; // max reflections
    private var verti  :int = 1; //segment handler don't touch.
    private var iactive :boolean;
    private var currot : Vector3;
    private var curpos : Vector3;*/
    
    public int distance;
    public LineRenderer lineRender;
    public string tagString;
    public int vertLimit;

    int verti = 1;
    bool isActive;
    Vector2 currentPos;
    Vector2 currentRot;
    RaycastHit2D hit;

    void Start()
    {
        lineRender = GetComponent<LineRenderer>();
        DrawLaser();
    }

    void Update()
    {
        
    }
    void DrawLaser()
    {
        verti = 1;
        isActive = true;
        currentRot = transform.forward;
        currentPos = transform.position;
        lineRender.SetVertexCount(1);
        lineRender.SetPosition(0, transform.position);
        
        //while (isActive)
        //{
            verti++;
            
            lineRender.SetVertexCount(verti);
            if (Physics2D.Raycast(currentPos, currentRot, distance))
            {
                verti++;
                currentPos = hit.point;
                currentRot = Vector3.Reflect(currentRot, hit.normal);
                lineRender.SetPosition(verti - 1, hit.point);
                /*if (hit.transform.gameObject.tag != tagString)
                {
                    isActive = false;
                }*/
            }
            else
            {
                //verti++;
                //isActive = false;
                lineRender.SetPosition(verti - 1, currentPos + 100 * currentRot);
            }
            if (verti > vertLimit)
            {
                //isActive = false;
            }
        //}
    }
}
