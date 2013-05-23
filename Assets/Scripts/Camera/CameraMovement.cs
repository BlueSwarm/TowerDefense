using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour 
{
	public float speed = 5.0f;
	
	private Vector3 direction;
	
	void Start ()
	{
		direction = new Vector3 ();
	}
	
	void Update ()
	{
		DetectDirection ();
		Move ();
	}
	
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
	
	void Move ()
	{
		transform.position += direction * speed * Time.deltaTime;
	}	
}
