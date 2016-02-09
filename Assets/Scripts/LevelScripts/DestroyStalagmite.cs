using UnityEngine;
using System.Collections;

public class DestroyStalagmite : MonoBehaviour {

    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Stalagmite")
        {
            print("Stalagmite");
            print(other.gameObject.name);
            Destroy(other.gameObject);
        }
    }
}
