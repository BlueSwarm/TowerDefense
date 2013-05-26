using UnityEngine;
using System.Collections;

public class HolderUtilities : MonoBehaviour 
{
	public bool holds = false;

	private Transform tower;
	
	public void HoldTower (string tower)
	{
		// Stop particles
		foreach (Transform child in transform)
		{
			// I use foreach because I plan on having
			// more than one child. I will get their
			// transform and then I can access their
			// GameObject.
			
			if (child.gameObject.name == "Particle Effect")
			{
				child.gameObject.SetActive (false);
				break;
			}
		}
		
		// Asign the tower and create the game object
		// coming soon!
		holds = true;
	}
	
	public void RemoveTower ()
	{		
		// Destroy the tower
		GameObject.Destroy (tower);
		holds = false;
		
		// Enable particles
		// Same thing as before
		foreach (Transform child in transform)
		{			
			if (child.gameObject.name == "Particle Effect")
			{
				child.gameObject.SetActive (true);
				break;
			}
		}
	}
}
