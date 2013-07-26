using UnityEngine;
using System.Collections;
using System.IO;

public class SimpleMenuGUI : MonoBehaviour 
{
	private int widthMid;
	private int heightMid;
	
	private string selected;
	
	private Rect playRect;
	private Rect exitRect;
	
	private Vector2 scrollPos = Vector2.zero;
	private Rect scrollRect;
	private Rect viewRect;
	private Rect boxRect;
	
	private string[] files;
	private StreamReader sr;
	
	void Start () 
	{
		// Like in the last GUI script we did (GeneralMapGUI.cs)
		// we start by preparing some variables.
		// Getting the middle points of the screen and getting
		// most of the rects. This time, we also get the files
		// in the Maps\ folder.
		
		widthMid = Screen.width / 2;
		heightMid = Screen.height / 2;
		selected = "None";
		
		playRect = new Rect (Screen.width - 150, 50, 100, 50);
		exitRect = new Rect (Screen.width - 150, 125, 100, 50);
		scrollRect = new Rect (10, 10, Screen.width - 300, Screen.height - 20);
		
		files = Directory.GetFiles (@".\Maps\");
		viewRect = new Rect (0, 0, Screen.width - 370, 70 * files.Length);
		boxRect = new Rect (10, 10, Screen.width - 300, 60 * files.Length);
	}
	
	void OnGUI ()
	{
		// Right away we start with something new in this OnGUI () function.
		// ScrollView is a great way to have a larger area show in a
		// smaller area by scrolling through the larger one. You can imagine
		// that if we have 100+ maps, only cinema screens would be able to
		// have space for all the buttons that we need to place. So, we
		// make a scroll view and we can now scroll through a nice list!
		// Don't forget to EndScrollView () when you are done!
		scrollPos = GUI.BeginScrollView (scrollRect, scrollPos, viewRect);
		// Background
		GUI.Box (boxRect, @"\Maps\");
		
		// We go through the file names and show up a button with a name
		// and a label with the description.
		Rect nameRect = new Rect (20, 20, 150, 50);
		Rect descrRect = new Rect (200, 30, Screen.width - 300, 50);
		for (int i = 0; i < files.Length; i++)
		{
			if (files[i].Contains (".dat"))
			{
				string mapName = files[i].Remove (files[i].LastIndexOf ('.'));
				mapName = mapName.Substring (mapName.IndexOf ("\\", 2) + 1);
				if (GUI.Button (nameRect, mapName))
				{
					selected = mapName;
				}
				
				sr = new StreamReader (files[i]);
				string descr;
				if (sr.ReadLine ().Contains ("Description"))
				{
					descr = sr.ReadLine ();
				}
				else
				{
					descr = "Description not found.";
				}
				
				GUI.Label (descrRect, descr);
				sr.Close ();
				
				nameRect.y += 70;
				descrRect.y += 70;
			}
		}
		
		GUI.EndScrollView ();
		
		// Here is the final part of the functionality of the main menu.
		// After we have selected the map and press Play, the game loads
		// up the ImportMap scene after we attach the right name in the
		// importer.
		if (GUI.Button (playRect, "Play"))
		{
			if (selected != "None")
			{
				ImportMap.mapName = selected;
				Application.LoadLevel ("ImportMap");
			}
		}
		
		if (GUI.Button (exitRect, "Exit"))
		{
			Application.Quit ();
		}
	}
}
