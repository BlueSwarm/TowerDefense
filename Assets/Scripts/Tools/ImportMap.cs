using UnityEngine;
using System.Collections;
using System.IO;
using WyrmTale;

public class ImportMap : MonoBehaviour
{
	// This one is static, because we want to assign it from the main menu.
	// When a variable is static, we can assign it without having an instance,
	// as static means that it's the same for every instance of the class.
	// So, we can name it from another scene and keep the value in the next one!
	public static string mapName;
	
	public Transform nodePrefab;
	public Transform corePrefab;
	public Transform lightPrefab;
	public Transform spawnerPrefab;
	public Transform logicPrefab;
	public Transform viewPrefab;
	public Transform holderPrefab;
	
	// CurrentNode will be used to name our AI Path nodes.
	private int currentNode = 0;
	
	// We keep the parents so we can assign them to their children later on.
	private GameObject aiPath;
	private GameObject terrain;
	
	void Start () 
	{
		// TESTING
		// If you want to test your maps, without going through
		// the menu, change the variable and get it out of the comments.
		// Don't forget to have your files in the Maps folder.
		// mapName = "TestLevel";
		
		// As with StreamWriter from ExportMap.cs, we use StreamReader
		// in this occasion. Again, it's simple and I like it!
		StreamReader sr = new StreamReader (@".\Maps\" + mapName + ".data");
		string line = sr.ReadLine ();
		while (!sr.EndOfStream)
		{
			// This here started as a layout of what I wanted my loop
			// to look like. In the end though, I ended up implementing 
			// the methods. You will notice there is a lot of code that
			// is being used over and over again and it's not the best thing.
			// I will revisit this script again in the future and clean some stuff
			// up. It works just fine for now, so let's leave it like that.
			if (line.Contains ("AI Path"))
			{
				aiPath = CreateParent ("AI Path", sr.ReadLine ());
			}
			else if (line.Contains ("Node"))
			{
				CreatePathNode (sr.ReadLine ());
			}
			else if (line.Contains ("Core"))
			{
				CreateCore (sr.ReadLine ());
			}
			else if (line.Contains ("light"))
			{
				CreateLight (sr.ReadLine ());
			}
			else if (line.Contains ("Enemies"))
			{
				CreateParent ("Enemies", sr.ReadLine ());
			}
			else if (line.Contains ("Spawner"))
			{
				CreateSpawner (sr.ReadLine ());
			}
			else if (line.Contains ("Logic"))
			{
				CreateMapLogic (sr.ReadLine ());
			}
			else if (line.Contains ("View"))
			{
				CreatePlayerView (sr.ReadLine ());
			}
			else if (line.Contains ("Terrain"))
			{
				CreateTerrain (sr.ReadLine ());
			}
			else if (line.Contains ("Spawn Area"))
			{
				CreateSpawnArea (sr.ReadLine ());
			}
			else if (line.Contains ("Tower Holder"))
			{
				CreateTowerHolder (sr.ReadLine ());
			}
			else if (line.Contains ("Wall"))
			{
				CreateWall (sr.ReadLine ());
			}
			else
			{
				Debug.Log ("IGNORED: " + line);
			}
			
			line = sr.ReadLine ();
		}
		
		sr.Close ();
	}

	GameObject CreateParent (string name, string json)
	{
		GameObject parent = new GameObject (name);
		
		JSON js = new JSON ();
		js.serialized = json;
		
		AssignTransform (parent.transform, js);
		
		return parent;
	}

	void CreatePathNode (string json)
	{
		Transform node = (Transform) Instantiate (nodePrefab);
		node.name = "Node " + currentNode;
		currentNode ++;
		node.parent = aiPath.transform;
		
		JSON js = new JSON ();
		js.serialized = json;
		
		AssignTransform (node, js);
	}

	void CreateCore (string json)
	{
		Transform core = (Transform) Instantiate (corePrefab);
		core.name = "Core";
		
		JSON js = new JSON ();
		js.serialized = json;
		
		AssignTransform (core, js);
		
		CoreLife script = core.GetComponent<CoreLife> ();
		script.life = js.ToInt ("Life");
	}

	void CreateLight (string json)
	{
		Transform dlight = (Transform) Instantiate (lightPrefab);
		dlight.name = "Directional light";
		
		JSON js = new JSON ();
		js.serialized = json;
		
		AssignTransform (dlight, js);
		dlight.light.intensity = js.ToFloat ("Intensity");
	}
	
	void CreateSpawner (string json)
	{
		Transform spawner = (Transform) Instantiate (spawnerPrefab);
		spawner.name = "EnemySpawner";
		
		JSON js = new JSON ();
		js.serialized = json;
		
		AssignTransform (spawner, js);
		
		AISpawner script = spawner.GetComponent<AISpawner> ();
		script.waves = js.ToInt ("Waves");
		script.perWave = js.ToInt ("Per Wave");
		script.interval = js.ToInt ("Interval");
	}

	void CreateMapLogic (string json)
	{
		Transform logic = (Transform) Instantiate (logicPrefab);
		logic.name = "MapLogic";
		
		JSON js = new JSON ();
		js.serialized = json;
		
		AssignTransform (logic, js);
	}

	void CreatePlayerView (string json)
	{
		Transform view = (Transform) Instantiate (viewPrefab);
		view.name = "PlayerView";
		
		JSON js = new JSON ();
		js.serialized = json;
		
		AssignTransform (view, js);
	}

	void CreateTerrain (string json)
	{
		GameObject terrain = GameObject.CreatePrimitive (PrimitiveType.Cube);
		terrain.name = "Terrain";
		this.terrain = terrain;
		
		JSON js = new JSON ();
		js.serialized = json;
		
		AssignTransform (terrain.transform, js);	
	}

	void CreateSpawnArea (string json)
	{
		GameObject area = GameObject.CreatePrimitive (PrimitiveType.Cube);
		area.name = "Spawn Area";
		area.transform.parent = terrain.transform;
		
		JSON js = new JSON ();
		js.serialized = json;
		
		AssignTransform (area.transform, js);
	}

	void CreateTowerHolder (string json)
	{
		Transform holder = (Transform) Instantiate (holderPrefab);
		holder.name = "Tower Holder";
		holder.parent = terrain.transform;
		
		JSON js = new JSON ();
		js.serialized = json;
		
		AssignTransform (holder, js);
	}

	void CreateWall (string json)
	{
		GameObject wall = GameObject.CreatePrimitive (PrimitiveType.Cube);
		wall.name = "Wall";
		wall.transform.parent = terrain.transform;
		
		JSON js = new JSON ();
		js.serialized = json;
		
		AssignTransform (wall.transform, js);
	}
	
	void AssignTransform (Transform trans, JSON js)
	{
		trans.position = (Vector3) js.ToJSON ("position");
		trans.rotation = (Quaternion) js.ToJSON ("rotation");
		trans.localScale = (Vector3) js.ToJSON ("scale");
	}
}
