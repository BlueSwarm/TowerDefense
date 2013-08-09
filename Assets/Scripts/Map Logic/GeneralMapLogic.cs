using UnityEngine;
using System.Collections;

public class GeneralMapLogic : MonoBehaviour 
{
	public static int credits = 200;
	
	public bool finished = false;
	public bool defeat = false;
	
	private AISpawner spawnerScript;
	public bool export;
	
	void Start () 
	{
		spawnerScript = GameObject.Find ("EnemySpawner").GetComponent<AISpawner> ();
		if (gameObject.GetComponent <ExportMap> ().enabled)
		{
			export = true;
		}
	}
	
	void Update ()
	{
		spawnerScript.export = export;
			
		Exit ();
		
		if (!finished)
		{
			CheckGameState ();
		}
		else
		{
			EndScene ();
		}
	}
	
	void Exit ()
	{
		if (Input.GetKeyDown (KeyCode.Escape))
		{
			Application.LoadLevel (0);
		}
	}
	
	void CheckGameState ()
	{
		if (!GameObject.Find ("Core"))
		{
			finished = true;
			defeat = true;
		}
		else if (spawnerScript.WavesOver ())
		{
			finished = true;
			defeat = false;
		}
	}
	
	void EndScene ()
	{
		// What we want from a scene after the game has ended,
		// is to get back to the main menu. We have a special script
		// for that called EndSequence. All it needs to know is if we
		// have lost or won.
		
		EndSequence.defeat = defeat;
		gameObject.GetComponent<EndSequence> ().enabled = true;
	}
}
