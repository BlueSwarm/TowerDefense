using UnityEngine;
using System.Collections;

public class AILife : MonoBehaviour 
{
	public int health = 50;
	public int creditsOnDeath = 30;
	
	void Update () 
	{
		if (health <= 0)
		{
			GeneralMapLogic.credits += creditsOnDeath;
			Destroy (this.gameObject);
		}
	}
		
	public void DoDamage (int amount)
	{
		health -= amount;
	}
}
