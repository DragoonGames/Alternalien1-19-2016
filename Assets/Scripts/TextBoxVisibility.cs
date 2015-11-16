using UnityEngine;
using System.Collections;

public class TextBoxVisibility : MonoBehaviour {

    public GameObject OnTextBox;
    public GameObject OffTextBox;     //Turn off previous text Overlays

    // Use this for initialization
    void Start()
    {
        OnTextBox.SetActive(false);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        OnTextBox.SetActive(true);
        OffTextBox.SetActive(false);
    }
}
