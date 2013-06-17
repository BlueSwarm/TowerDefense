using UnityEngine;
using System.Collections;

// Please make sure to check the Update 4 under Update Notes.
// The explanation is over ther, because the code doesn't need
// any explanations. Only the idea behind it.

public class AIMovement : MonoBehaviour 
{
	public float speed = 5;
	
	private GameObject path;
	
	private Transform node;
	private int nodeNumber = 0;
	
	void Start () 
	{
		path = GameObject.Find ("AI Path");
		GetNextNode ();
	}
	
	void Update () 
	{
		if (EndOfPath ())
		{
			DestroyObject ();
			return;
		}
		
		if (ReachedNode ())
		{
			GetNextNode ();
		}
		
		Move ();
	}
	
	bool EndOfPath ()
	{
		return !node;
	}
	
	void DestroyObject ()
	{
		Destroy (this.gameObject);
	}
	
	bool ReachedNode ()
	{
		bool reached = false;
		
		float xDiff = Mathf.Abs (transform.position.x - node.position.x);
		float zDiff = Mathf.Abs (transform.position.z - node.position.z);
		
		if (xDiff <= 0.1f && zDiff <= 0.1f)
		{
			reached = true;
			nodeNumber++;
		}
		
		return reached;		
	}
	
	void GetNextNode ()
	{
		this.node = null;
		
		foreach (Transform node in path.transform)
		{
			if (node.name == "Node " + nodeNumber)
			{
				this.node = node;
				break;
			}
		}
	}
	
	void Move ()
	{
		transform.LookAt (node);
		transform.position += transform.forward * speed * Time.deltaTime;
	}
}
