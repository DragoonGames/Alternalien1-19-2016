using UnityEngine;
using System.Collections;

public class DestroyBoss : MonoBehaviour
{
    public GameObject boss;
    //public int count;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Lenny")
        {
            boss.GetComponent<bossScript>().switchCount++;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Lenny")
        {
            boss.GetComponent<bossScript>().switchCount--;
        }
    }
}
