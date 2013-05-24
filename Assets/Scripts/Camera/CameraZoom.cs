using UnityEngine;
using System.Collections;

public class CameraZoom : MonoBehaviour 
{
	// Same with movement but this time try a number of
	// 250+.
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
	// the -!
	void DetectDirection ()
	{
		direction = -Input.GetAxis ("Mouse ScrollWheel");
		
		if (direction > 0)
		{
			direction = 1;
		}
		else if (direction < 0)
		{
			direction = -1;
		}
	}
	
	void Move ()
	{
		transform.position += new Vector3 (0, direction, 0) * speed * Time.deltaTime;
	}
}
