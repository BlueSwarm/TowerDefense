using UnityEngine;
using System.Collections;

public class CameraZoom : MonoBehaviour 
{
	public float speed = 10.0f;
	
	private float direction;
	
	void Start () 
	{
		
	}
	
	void Update ()
	{
		DetectDirection ();
		Move ();
	}
	
	void DetectDirection ()
	{
		direction = -Input.GetAxis ("Mouse ScrollWheel");
	}
	
	void Move ()
	{
		transform.position += new Vector3 (0, direction, 0) * speed * Time.deltaTime;
	}
}
