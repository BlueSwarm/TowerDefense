using UnityEngine;
using System.Collections;

public class ProjectileUtilities : MonoBehaviour 
{
	public int damage = 20;
	public Vector3 mapLimits = new Vector3 (100, 100, 100);
	
	void Start () 
	{
	
	}
	
	void Update () 
	{
		CheckOutOfBounds ();
	}
	
	// This takes care of any loose projectiles.
	// I made the mapLimits variable so that we don't have
	// to change the script if the map is bigger.
	// All about making useful tools!!!
	void CheckOutOfBounds ()
	{
		if (Mathf.Abs (transform.position.x) >= mapLimits.x)
		{
			Destroy (this.gameObject);
		}
		else if (Mathf.Abs (transform.position.y) >= mapLimits.y)
		{
			Destroy (this.gameObject);
		}
		else if (Mathf.Abs (transform.position.z) >= mapLimits.z)
		{
			Destroy (this.gameObject);
		}
	}
	
	// OnTriggerEnter is called by the engine whenever a collider/rigidbody
	// enters a trigger. I believe, at least one of the objects, either the
	// collider or the trigger, needs to have a rigidbody attached or else
	// it won't work.
	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Enemy")
		{
			// Get script, apply effects.
			AILife ailInstance = other.gameObject.GetComponent<AILife> ();
			ailInstance.DoDamage (damage);
			
			Destroy (this.gameObject);
		}
	}
}
