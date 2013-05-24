using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour 
{
	// The initial speed in which the camera moves.
	// I made it public so that I can change it directly from the editor.
	// Try experimenting around with the numbers.
	// I found out that 50+ is a good start.
	public float speed = 5.0f;
	
	// Storing the moving direction in a Vector3 ensures a simple move method.
	// The only component from x,y,z that will be != 0 is going to be the
	// desired direction. Also, y will always be 0.
	private Vector3 direction;
	
	void Start ()
	{
		direction = new Vector3 ();
	}
	
	// Side note: I like putting simple things, like 1 line of code into
	// separate functions. It helps me design the algorithm by just putting
	// function names in order in the Update function.
	void Update ()
	{
		DetectDirection ();
		Move ();
	}
	
	// Pretty straight forward function. It resets the direction to 0,
	// gets the mouse position and checks to see if we are at the edge of the screen.
	// If so, we change the direction!
	void DetectDirection ()
	{
		direction = Vector3.zero;
		
		Vector3 cursorPos = Input.mousePosition;
		
		if (cursorPos.x == 0)
		{
			direction.x = -1;
		}
		else if (cursorPos.x >= Screen.width - 10)
		{
			direction.x = 1;
		}
		
		if (cursorPos.y == 0)
		{
			direction.z = -1;
		}
		else if (cursorPos.y >= Screen.height - 10)
		{
			direction.z = 1;
		}
	}
	
	// Move only consists of 1 line of code which as the name says,
	// moves the camera transform.
	// transform is an instance of Transform for the game object that
	// we attached the script on. That is handled by the Unity Engine
	// so we don't have to declare anything!
	// If you open the Unity editor and you choose any object,
	// you'll notice on the top of the inspector a section called
	// transform. This is what this instance holds.
	// We don't need physics movement for the camera, so we just
	// adjust it's current position by adding the direction,
	// multiplied by our speed and by Time.deltaTime.
	// The delta time is the time passed between this frame and the last.
	// This ensures a smooth movement, when the game has some frame issues.
	void Move ()
	{
		transform.position += direction * speed * Time.deltaTime;
	}	
}
