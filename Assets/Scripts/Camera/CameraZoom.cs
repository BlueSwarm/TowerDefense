using UnityEngine;
using System.Collections;

public class CameraZoom : MonoBehaviour 
{
	// Same with movement but this time try a number of
	// 100+. I had a weird bug earlier where I had a value of
	// 100 and it was going fast enough, but when I changed
	// to 500 it was going slower...it went away afterwards.
	public float speed = 10.0f;
	
	// This time the direction is only counted on Y-axis so
	// we don't need a Vector3.
	private float direction;
	
	void Update ()
	{
		DetectDirection ();
		Move ();
	}
	
	// This is a lot simpler. Input.GetAxis (NAME_OF_AXIS)
	// checks if a specific axis is being used. It also returns
	// the direction but it's reversed from what we want, hence
	// the -! I'll have to refine this a bit more, cause I suspsect
	// this is the cause of the weird bug I mentioned earlier.
	void DetectDirection ()
	{
		direction = -Input.GetAxis ("Mouse ScrollWheel");
	}
	
	void Move ()
	{
		transform.position += new Vector3 (0, direction, 0) * speed * Time.deltaTime;
	}
}
