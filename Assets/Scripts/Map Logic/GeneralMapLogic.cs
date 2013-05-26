using UnityEngine;
using System.Collections;

public class GeneralMapLogic : MonoBehaviour 
{

	void Start () 
	{
	
	}
	
	void Update () 
	{
		Exit ();
	}
	
	void Exit ()
	{
		if (Input.GetKeyDown (KeyCode.Escape))
		{
			Application.Quit ();
		}
	}
}
