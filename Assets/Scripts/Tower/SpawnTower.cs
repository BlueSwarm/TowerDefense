using UnityEngine;
using System.Collections;

public class SpawnTower : MonoBehaviour 
{
	private GameObject towerHolder;
	
	void Update () 
	{
		if (SpawnCommand ())
		{
			Spawn ();
		}
	}
	
	bool SpawnCommand ()
	{
		// Checking for mouse left click.
		// This is going to activate only
		// in the frame in which we left clicked.
		return Input.GetMouseButtonDown (0);
	}
	
	void Spawn ()
	{
		// In the future we are going to first check if we have a tower
		// selected and if we don't then we won't bother raycasting.
		// I won't do it till we have a GUI for testing reasons.
		
		// Get the collider we hit with the raycast
		Collider colliderHit = GetMousePosition ();
		
		// Check if we can spawn a tower there
		if (CheckCollider (colliderHit))
		{
			// Get the tower
			string tower = GetSelectedTower ();
		
			// Spawn it!
			GiveSpawnCommand (tower);
		}		
	}
	
	Collider GetMousePosition ()
	{
		// This may look weird or complex at first,
		// but it is really simple.
		Collider colliderHit = null;
		
		// Ray can be described as a Vector that has it's origin point
		// on the screen and it's direction inside.
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		
		// Physics.Raycast will get our ray and start moving on it for
		// 1000 units. At the first collision it will stop and store
		// the results in hitInfo!
		RaycastHit hitInfo;
		if (Physics.Raycast (ray, out hitInfo, 1000))
		{
			// Point is where the ray hit the first collider.
			// This is the mouse position in the world!
			colliderHit = hitInfo.collider;
		}		
		
		return colliderHit;
	}
	
	bool CheckCollider (Collider colliderHit)
	{
		// We will just check if the collider has the tag "TowerHolder"
		// and then check if we can actually spawn there.
		if (colliderHit.tag == "TowerHolder")
		{
			// Keep a copy of the transform.
			towerHolder = colliderHit.gameObject;
			
			// The line pretty much explains everything.
			HolderUtilities huInstance = colliderHit.gameObject.GetComponent<HolderUtilities> ();
			return !huInstance.holds;
		}
		
		return false;
	}
	
	string GetSelectedTower ()
	{
		// This method is going to look into the GUI script
		// where we'll have the selected tower stored.
		return string.Empty;
	}
	
	void GiveSpawnCommand (string tower)
	{
		HolderUtilities huInstance = towerHolder.GetComponent<HolderUtilities> ();
		huInstance.HoldTower (tower);
	}
}
