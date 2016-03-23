using UnityEngine;
using System.Collections;

public class LightBeams : MonoBehaviour
{
    public int laserDistance = 100; //max raycasting distance
    public int laserLimit = 10; //the laser can be reflected this many times
    public LineRenderer laserRenderer; //the line renderer

    // Use this for initialization
    void Start ()
    {
        laserRenderer = GetComponent<LineRenderer>();
        DrawLaser();
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
    void DrawLaser()
    {
        int laserReflected = 1; //How many times it got reflected
        int vertexCounter = 1; //How many line segments are there

        bool loopActive = true; //Is the reflecting loop active?
        Vector2 laserDirection = transform.right; //direction of the next laser
        Vector2 lastLaserPosition = transform.position; //origin of the next laser

        laserRenderer.SetVertexCount(1);
        laserRenderer.SetPosition(0, transform.position);

        while (loopActive)
        {
            RaycastHit2D hit = Physics2D.Raycast(lastLaserPosition, laserDirection, laserDistance);
            {
                print(hit);
                laserReflected++;
                vertexCounter += 3;
                laserRenderer.SetVertexCount(vertexCounter);
                laserRenderer.SetPosition(vertexCounter - 3, Vector3.MoveTowards(hit.point, lastLaserPosition, 0.01f));
                laserRenderer.SetPosition(vertexCounter - 2, hit.point);
                laserRenderer.SetPosition(vertexCounter - 1, hit.point);
                lastLaserPosition = hit.point;
                laserDirection = Vector3.Reflect(laserDirection, hit.normal);
            }
            /*else {
                laserReflected++;
                vertexCounter++;
                laserRenderer.SetVertexCount(vertexCounter);
                laserRenderer.SetPosition(vertexCounter - 1, lastLaserPosition + (laserDirection.normalized * laserDistance));

                loopActive = false;
            }*/
            if (laserReflected > laserLimit)
                loopActive = false;
        }
    }
    /*IEnumerator FireMahLazer()
    {
        //Debug.Log("Running");
        mLineRenderer.enabled = true;
        int laserReflected = 1; //How many times it got reflected
        int vertexCounter = 1; //How many line segments are there
        bool loopActive = true; //Is the reflecting loop active?

        Vector3 laserDirection = transform.forward; //direction of the next laser
        Vector3 lastLaserPosition = transform.localPosition; //origin of the next laser

        mLineRenderer.SetVertexCount(1);
        mLineRenderer.SetPosition(0, transform.position);
        RaycastHit hit;

        while (loopActive)
        {

            if (Physics.Raycast(lastLaserPosition, laserDirection, out hit, laserDistance) && hit.transform.gameObject.tag == bounceTag)
            {

                Debug.Log("Bounce");
                laserReflected++;
                vertexCounter += 3;
                mLineRenderer.SetVertexCount(vertexCounter);
                mLineRenderer.SetPosition(vertexCounter - 3, Vector3.MoveTowards(hit.point, lastLaserPosition, 0.01f));
                mLineRenderer.SetPosition(vertexCounter - 2, hit.point);
                mLineRenderer.SetPosition(vertexCounter - 1, hit.point);
                mLineRenderer.SetWidth(.1f, .1f);
                lastLaserPosition = hit.point;
                laserDirection = Vector3.Reflect(laserDirection, hit.normal);
            }
            else {

                Debug.Log("No Bounce");
                laserReflected++;
                vertexCounter++;
                mLineRenderer.SetVertexCount(vertexCounter);
                Vector3 lastPos = lastLaserPosition + (laserDirection.normalized * laserDistance);
                Debug.Log("InitialPos " + lastLaserPosition + " Last Pos" + lastPos);
                mLineRenderer.SetPosition(vertexCounter - 1, lastLaserPosition + (laserDirection.normalized * laserDistance));

                loopActive = false;
            }
            if (laserReflected > maxBounce)
                loopActive = false;
        }

        if (Input.GetKey("space") && timer < 2)
        {
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
            StartCoroutine("FireMahLazer");
        }
        else {
            yield return null;
            mLineRenderer.enabled = false;
        }
    }*/
}