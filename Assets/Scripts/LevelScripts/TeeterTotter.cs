using UnityEngine;
using System;
using System.Collections;

public class TeeterTotter : MonoBehaviour
{
    public float changeRotation = 45.0f;        // The rotation is changed incrementally.
    public bool swingDown = false;              // The swing starts up
    private float maxRotation = 25;             
    private float minRotation = -25;            // Minimum rotation needs to be negative or the swing will be contradicted.

    void Start()
    {
        transform.rotation = Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, changeRotation);          // Initialize changeRotation
    }

    void Update()
    {
        changeRotation = Mathf.Clamp(changeRotation, minRotation, maxRotation);                                                     // Clamp the maximum and minimum Z values

        if (swingDown)
        {
            changeRotation += .5f;                                                                                                  // Incremental change in update
            transform.rotation = Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, changeRotation);      // Rotate Z only
            if (changeRotation >= maxRotation)                                                                                      // When you hit max clamp...
                swingDown = false;                                                                                                  // change direction.
        }
        if (!swingDown)
        {
            changeRotation -= .5f;                                                                                                  // Incremental change in update
            transform.rotation = Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, changeRotation);      // Rotate Z only
            if (changeRotation <= minRotation)                                                                                      // When you hit min clamp...
                swingDown = true;                                                                                                   // change direction.
        }

    }
}
