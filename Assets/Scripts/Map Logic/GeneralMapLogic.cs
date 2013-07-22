using UnityEngine;
using System.Collections;

public class GeneralMapLogic : MonoBehaviour 
{
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
			EndSequence ();
		}
	}
	
	void Exit ()
	{
		if (Input.GetKeyDown (KeyCode.Escape))
		{
			Application.Quit ();
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
	
	void EndSequence ()
	{
		// Here we call all the methods we need in order to 
		// end the map and move on (go back to main menu or load
		// next map). For now we just post it in the log.
		if (defeat)
		{
			Debug.Log ("You lose!");
		}
		else
		{
			Debug.Log ("You win!");
		}
	}
}
