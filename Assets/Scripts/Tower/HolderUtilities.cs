using UnityEngine;
using System.Collections;

public class HolderUtilities : MonoBehaviour 
{
	public bool holds = false;
	
	private PrefabChest pcInstance;
	private Transform tower;
	private Transform particleEffect;
		
	void Start ()
	{
		pcInstance = GameObject.Find ("MapLogic").GetComponent<PrefabChest> ();
		
		// This implementation was suggested by arrowgamer.
		// It's way more efficient and easy for the eye
		// to just look for the particle effect once
		// and hold an instance of it in a private variable.
		foreach (Transform child in transform)
		{
			if (child.name == "Particle Effect")
			{
				this.particleEffect = child;
				break;
			}
		}
	}
	
	public void HoldTower (string tower)
	{
		// Stop particles
		particleEffect.gameObject.SetActive (false);
		
		// Asign the tower and create the game object
		// Get the prefab from the chest.
		AccesssPrefabChest (tower);
		
		// Instantiate the tower.
		// Instantiate takes the current prefab and creates a
		// game object of it at the given location with the
		// given rotation. Because we don't want to change the
		// rotation but we have to give a new rotation to the
		// function, we simply use Quaternion.identity, which
		// is no rotation!
		Vector3 towerPosition = transform.position + new Vector3 (0, 3, 0);
		this.tower = (Transform) Instantiate (this.tower, towerPosition, Quaternion.identity);
		
		holds = true;
	}
	
	private void AccesssPrefabChest (string tower)
	{		
		if (tower == "Tower 1")
		{
			this.tower = pcInstance.towerPrefab;
		}
	}
	
	public void RemoveTower ()
	{		
		// Destroy the tower
		if (!holds)
		{
			return;
		}
		GameObject.Destroy (tower.gameObject);
		holds = false;
		
		// Enable particles
		particleEffect.gameObject.SetActive (true);
	}
}
