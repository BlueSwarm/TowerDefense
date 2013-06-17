using UnityEngine;
using System.Collections;

public class CoreLife : MonoBehaviour 
{
	public int life = 30;
		
	void Update () 
	{
		CheckLife ();
	}
	
	void OnTriggerEnter (Collider other)
	{
		// Later on, with more enemies in the game,
		// each enemy will be doing different amounts
		// of damage to the core.
		if (other.tag == "Enemy")
		{
			life -= 5;
			Destroy (other.gameObject);
			Debug.Log (life);
		}
	}
	
	void CheckLife ()
	{
		if (life <= 0)
		{
			Destroy (this.gameObject);
			// Also, notify the game that we actually lost.
		}
	}
}
