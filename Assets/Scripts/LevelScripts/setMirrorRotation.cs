using UnityEngine;
using System.Collections;

public class setMirrorRotation : MonoBehaviour
{
    public bool flipped;

    SpriteRenderer rend;

    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (flipped)
        {
            rend.flipX = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}
