using UnityEngine;
using System.Collections;

public class EndSequence : MonoBehaviour 
{
	public static bool defeat;
	
	public float interval = 5.0f;
	private float startTime;
	
	private Rect boxRect;
	private Rect messageRect;
	
	void Start () 
	{
		startTime = Time.time;
		
		boxRect = new Rect (Screen.width / 2 - 100, 
			Screen.height / 2 - 75, 150, 100);
		messageRect = new Rect (Screen.width / 2 - 50, 
			Screen.height / 2 - 35, 200, 100);
	}
	
	void Update () 
	{
		if (Time.time - startTime >= interval)
		{
			// 0 is the main menu scene
			Application.LoadLevel (0);
		}
	}
	
	void OnGUI ()
	{
		// A really simple gui layout. Just a box and a message in it.
		
		GUI.Box (boxRect, "");
		if (defeat)
		{
			GUI.Label (messageRect, "Defeat");
		}
		else
		{
			GUI.Label (messageRect, "Victory");
		}
	}
}
