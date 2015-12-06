using UnityEngine;
using System.Collections;

public class climbVine : MonoBehaviour
{
    public GameObject vineTop;
    public GameObject vineGate;
    public bool canClimb;
    GameObject[] player;

    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player");
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Vine")
        {
            canClimb = true;
        }
        if (collider.gameObject == vineTop)
        {

        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Vine")
        {
            canClimb = false;
        }
    }

    void Update() {
        if (Input.GetKey(KeyCode.UpArrow) && canClimb == true)
            transform.position = Vector3.Lerp(transform.position, vineTop.transform.position, Time.deltaTime);
    }
}
