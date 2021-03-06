using UnityEngine;
using System.Collections;

public class AISpawner : MonoBehaviour 
{
	// A lot of info needed here in order to make this
	// our general spawner. Of course we'll need to make
	// changes if we are to put more enemies.
	public int waves;
	public int perWave;
	public int interval;
	
	public bool export;
	
	public Transform enemyPrefab;
	private Transform enemyParent;
	
	private int currentWave;
	private int spawned;
	private float lastSpawn;
	private bool spawning;
	
	void Start () 
	{
		export = false;
		
		enemyParent = GameObject.Find ("Enemies").transform;
		currentWave = 1;
		spawning = false;
		lastSpawn = Time.time - interval;
		spawned = 0;
	}
	
	void Update () 
	{
		// Early exit if we export the map.
		if (export)
		{
			return;
		}
		
		// As you may have noticed I have the habit of writing
		// some kind of algorithm in my Update function.
		// I think this is a good way of creating an image
		// of what a script does and of course making it easy
		// understand. This is as simple as it looks to be:
		// are we spawning enemies? is the interval cooldown over?
		// if yes then spawn another one.
		// if no, check if the wave is over. If it is and we are 
		// still playing this level, spawn another wave :)
		if (spawning && Time.time - lastSpawn >= interval)
		{
			SpawnNextEnemy ();
		}
		else
		{
			if (CheckWaveOver () && currentWave <= waves)
			{
				spawning = true;
				currentWave++;
				spawned = 0;
			}
		}
	}
		
	bool CheckWaveOver ()
	{
		// At first I didn't think this would work but it looks
		// like Unity updates the childCount whenever we add/remove
		// the parent in the child info.
		return enemyParent.childCount == 0 && !spawning;
	}
	
	void SpawnNextEnemy ()
	{
		// Like with every other instantiate we prepare the position
		// and we use the "no rotation" variable inside Quaternion.
		// I know this was a useless comment but I really want you to
		// understand that this is important. This is the only way you
		// can make stuff you created beforehand appear in your scene
		// through scripting.
		Vector3 spawnPosition = transform.position;
		Transform enemy = (Transform) Instantiate (enemyPrefab, spawnPosition, Quaternion.identity);
		enemy.parent = enemyParent;
		spawned++;
		
		if (spawned == perWave)
		{
			spawning = false;
		}
		
		lastSpawn = Time.time;
	}
	
	public bool WavesOver ()
	{
		// We will also check if there are enemies around.
		// This will be the way to check if we actually 
		// won the map.
		if (waves < currentWave && CheckWaveOver ())
		{
			return true;
		}
		return false;
	}
}
