using UnityEngine;
using System.Collections;

public class SimpleMenuGUI : MonoBehaviour 
{
	private int widthMid;
	private int heightMid;
	
	private string selected;
	
	private Rect listRect;
	private Rect playRect;
	private Rect exitRect;
	
	void Start () 
	{
		widthMid = Screen.width / 2;
		heightMid = Screen.height / 2;
		selected = "None";
		
		playRect = new Rect (Screen.width - 150, 50, 100, 50);
		exitRect = new Rect (Screen.width - 150, 125, 100, 50);
	}
	
	void Update () 
	{
		
	}
	
	void OnGUI ()
	{
		if (GUI.Button (playRect, "Play"))
		{
			
		}
		
		if (GUI.Button (exitRect, "Exit"))
		{
			Application.Quit ();
		}
	}
}
