#pragma strict

var scrollSpeed : float;

function Update () 
{
    GetComponent.<Renderer>().material.SetTextureOffset("_MainTex", Vector2(0, Time.time * scrollSpeed));
}