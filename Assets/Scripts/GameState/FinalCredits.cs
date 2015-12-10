using UnityEngine;
using System.Collections;

public class FinalCredits : MonoBehaviour {

    private float offset;
    public float speed;
    public GUIStyle style;
    public Rect viewArea;

    private void Start()
    {
        this.offset = this.viewArea.height;
        viewArea = new Rect(0, 0, Screen.width, Screen.height);
        style.contentOffset= new Vector2(0, Screen.height / 2);
    }

    private void Update()
    {
        this.offset -= Time.deltaTime * this.speed;
    }

    private void OnGUI()
    {
        GUI.BeginGroup(this.viewArea);

        var position = new Rect(0, this.offset, this.viewArea.width, this.viewArea.height);
        var text = @"







Executive Producer

Sam Marmurowicz


  
  
Project Lead

Ian Ehlers




Build Master

Erin Fink




Lead Programmer

Michael Peck




Lead Designer

Garrett Poenitsch







Lead Animator

Noah Rotter








Animation

Taylor Passow
Jose Flores








Level Design

Devon Jenkins
Sam Marmurowicz
Zachary Tietyen








Programming

Erin Fink
Michael Anderson







  
Music

Jeffrey Brown
";
        GUI.Label(position, text, this.style);

       GUI.EndGroup();
    }
}
