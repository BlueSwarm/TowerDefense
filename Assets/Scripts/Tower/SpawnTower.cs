using UnityEngine;
using System.Collections;

public class SpawnTower : MonoBehaviour 
{
	private GameObject towerHolder;
	
	private bool spawnCommand = false;
	private bool removeCommand = false;
	private string selectedTower;
	
	void Update () 
	{
		if (spawnCommand)
		{
			Spawn ();
			spawnCommand = false;
		}
		else if (removeCommand)
		{
			Remove ();
			removeCommand = false;
		}
	}
	
	public void SpawnCommand ()
	{
		// This is only going to be called when the GUI
		// gets the left click event.
		spawnCommand = true;	
	}
	
	public void RemoveCommand ()
	{
		// Same as SpawnCommand.
		// It depends on what we have selected.
		removeCommand = true;
	}
	
	void Spawn ()
	{
		// Early exit because there is no tower selected.
		if (selectedTower == "None")
		{
			return;
		}
		
		// Get the collider we hit with the raycast
		Collider colliderHit = GetMousePosition ();
		
		// Check if we can spawn a tower there
		if (CheckCollider (colliderHit))
		{		
			// Spawn it!
			GiveSpawnCommand ();
		}		
	}
	
	void Remove ()
	{
		// Exactly like the spawn function.		
		Collider colliderHit = GetMousePosition ();
		
		// The main difference is that we want to proceed
		// if the holder IS holding a tower.
		if (!CheckCollider (colliderHit))
		{
			GiveRemoveCommand ();
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
		
		// Early exit check
		if (!colliderHit)
		{
			return false;
		}
		
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
	
	public void SetSelectedTower (string tower)
	{
		// This is being called by the GUI to set the selected tower.
		selectedTower = tower;
	}
	
	void GiveSpawnCommand ()
	{
		HolderUtilities huInstance = towerHolder.GetComponent<HolderUtilities> ();
		huInstance.HoldTower (selectedTower);
	}
	
	void GiveRemoveCommand ()
	{
		HolderUtilities huInstance = towerHolder.GetComponent<HolderUtilities> ();
		huInstance.RemoveTower ();
	}
}
