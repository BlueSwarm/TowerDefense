using UnityEngine;
using System.Collections;

public class GeneralMapGUI : MonoBehaviour 
{
	private SpawnTower stInstance;
	
	private int widthMid;
	private int heightMid;
	
	private string selected;
	private Rect selectedRect;
	
	private Rect tower1Rect;
	private Rect removeRect;
	private Rect nameRect;
	
	void Start () 
	{
		// This time the Start() function is really useful to us.
		// I really like preparing all the rects that the GUI
		// functions need. I also like keeping a copy of the
		// half point of the screen which will probably come
		// handy later on. If not, we can always remove it!
		// Check the OnGUI () for more on Rects.
		stInstance = gameObject.GetComponent<SpawnTower> ();
		
		widthMid = Screen.width / 2;
		heightMid = Screen.height / 2;
		
		selected = "None";
		selectedRect = new Rect (10, 10, 100, 50);
		
		tower1Rect = new Rect (10, Screen.height - 110, 100, 100);
		removeRect = new Rect (Screen.width - 110, Screen.height - 110, 100, 100);
		int nameLenght = 10 * ImportMap.mapName.Length;
		nameRect = new Rect (Screen.width - 25 - nameLenght, 10, nameLenght, 50);
	}
	
	void Update () 
	{
		Deselect ();	
	}
	
	void OnGUI ()
	{
		// This function is being called once per event. This means
		// it can be called multiple times in one frame. Only in here
		// we can call the GUI class functions and no where else.
		
		// Each function needs a Rect. Rects hold 4 variables.
		// 2 for position (x,y) and 2 for size (width, height).
		// This defines where and how big the selected GUI
		// element will be.
		
		// I am pretty sure with each frame this method is called once
		// at the very start of the rendering proccess, so that it 
		// can draw everything we asked on the screen.
		// If there is an event, it comes in and checks a few things.
		// Things like, did we press any of the buttons? Did we type
		// anything in a text box/text area? Did we move a slider?
		// In general, this is a pretty cool way to implement a gui.
		
		// Later on we are probably going to add a GUI skin as well.
		// It will allow us to mess around with the way everything
		// is shown.
		
		// Selected info Label
		GUI.Label (selectedRect, selected);
		
		// Map name info label
		GUI.Label (nameRect, ImportMap.mapName);
		
		// Tower buttons
		if (GUI.Button (tower1Rect, "Tower 1"))
		{
			selected = "Tower 1";
		}
		
		if (GUI.Button (removeRect, "Remove Tower"))
		{
			selected = "Remove Tower";			
		}
		
		// GUI events
		Event e = Event.current;
		if (e.isMouse && e.button == 0)
		{
			stInstance.SetSelectedTower (selected);
			if (selected == "Remove Tower")
			{
				stInstance.RemoveCommand ();
			}
			else
			{
				stInstance.SpawnCommand ();
			}
		}
	}
	
	void Deselect ()
	{
		if (Input.GetMouseButtonDown (1))
		{
			selected = "None";
		}
	}
}
