using UnityEngine;
using System.Collections;

public class TowerAttack : MonoBehaviour 
{
	// As always, our general info.
	// We need range, force to aply on the projectile,
	// cooldown in order to wait between attacks and
	// of course the projectile itself.
	public float range = 10;
	public float shootForce = 50;
	public float cooldown = 1;
	public Rigidbody projectile;
	
	// lastAttack keeps the Time.time we last attacked.
	// This way, doing Time.time - lastAttack, is enough
	// to see if we are still in cooldown.
	private float lastAttack;
	private Transform enemies;
	private Transform currentEnemy;

	void Start () 
	{
		enemies = (Transform) GameObject.Find ("Enemies").transform;
		lastAttack = -cooldown;
	}
	
	void Update () 
	{	
		// currentEnemy starts as null (of course).
		// Everytime we have an enemy locked and he gets
		// out of range, we make currentEnemy null.
		if (!currentEnemy)
		{
			FindNextEnemy ();
		}
		
		if (currentEnemy) 
		{
			if (InRange ())
			{
				Attack ();
			}
		}
	}
	
	// pretty simple function, like the one in HolderUtilities.
	// We look all the enemies and we check if any of them are in range.
	// If someone is in range, we lock the target and shoot him!
	// WARNING: This works fine with 1 enemy. I haven't tried for multiple
	// enemies. I want to build the enemy spawner first.
	void FindNextEnemy ()
	{
		foreach (Transform enemy in enemies)
		{
			if (Vector3.Distance (enemy.position, transform.position) <= range)
			{
				currentEnemy = enemy;
				break;
			}
		}
	}
	
	bool InRange ()
	{
		if (Vector3.Distance (currentEnemy.position, transform.position) <= range)
		{
			return true;
		}
		else
		{
			currentEnemy = null;
			return false;
		}
	}
	
	// I explain what is a rigidbody in the UpdateNotes.
	// The script is pretty easy to understand.
	// Like we did before when we instantiated a prefab,
	// we prepare the spawn position and we just spawn it.
	// The only difference this time is that we give our rigidbody
	// some velocity, so it starts moving.
	void Attack ()
	{
		if (Time.time - lastAttack > cooldown)
		{
			Vector3 spawnPosition = transform.position;
			Vector3.MoveTowards (spawnPosition, currentEnemy.position, 5);
			Rigidbody newProjectile = (Rigidbody) Instantiate (projectile, spawnPosition, Quaternion.identity);
			
			newProjectile.transform.LookAt (currentEnemy.position + currentEnemy.forward);
			newProjectile.velocity = newProjectile.transform.forward * shootForce;
			
			lastAttack = Time.time;
		}
	}
}
