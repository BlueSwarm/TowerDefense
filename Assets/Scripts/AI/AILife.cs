using UnityEngine;
using System.Collections;

public class AILife : MonoBehaviour 
{
	public int health = 50;
	
	void Start () 
	{
	
	}
	
	void Update () 
	{
		if (health <= 0)
		{
			Destroy (this.gameObject);
		}
	}
		
	public void DoDamage (int amount)
	{
		health -= amount;
	}
}
