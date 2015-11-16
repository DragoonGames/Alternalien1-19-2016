using UnityEngine;
using System.Collections;

public class FallingBoulders : MonoBehaviour {

    public Rigidbody2D boulderRigid;
    Vector3 startingPosition;
    public float timeToRun;
    public int count = 1;
    public bool isFalling = true;

    // Use this for initialization
    void Start () {
        startingPosition = new Vector3(transform.position.x,
            transform.position.y, 0.0f);
        if (timeToRun == 0 || timeToRun > 10)
        {
            timeToRun = 3;
        }
        print(startingPosition);
	}
	IEnumerator IndianaJones()
    {
        while (count >= 1)
        {
            Rigidbody2D boulderClone;
            boulderClone = Instantiate(boulderRigid, startingPosition, transform.rotation) as Rigidbody2D;
            print(count);
            count--;
            yield return new WaitForSeconds(timeToRun);
            count++;
            Destroy(boulderClone.gameObject, 5.0f);
        }
        isFalling = true;
    }
    IEnumerator KeepRunning()
    {
        isFalling = false;
        StartCoroutine(IndianaJones());
        yield return new WaitForSeconds(timeToRun);
    }
	// Update is called once per frame
	void FixedUpdate () {
        if (isFalling)
            StartCoroutine(KeepRunning());
    }
}
