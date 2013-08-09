using UnityEngine;
using System.Collections;
using System.IO;
using WyrmTale;

// After a bit of thinking and chatting with my good friend Arrowgamer,
// I decided to do what he suggested. Write down in a single file, all the info
// I need to create the map at runtime. The format that I was going to use
// would be something similar to JSON and when he suggested it, I read about it
// right away. Then I found this script called JSON.cs which is included in
// the External Libraries folder. It's a Unity version of miniJSON (I am sure
// it's not called that but it makes more sense that way :P)
//
// So, a quick explanation of ExportMap.cs:
// We go through the Game Objects of the map and keep their Transforms
// and everything else we need, like settings. For example, in AI Path
// we only needed the Transform (we could even do without that, but let's
// keep it for now) as it's just a parent. But we couldn't just keep the
// Transform of Enemy Spawner as every map would have different settings.
// After we take a JSON format of the game object, we serialize it and
// write it down in our file.
public class ExportMap : MonoBehaviour 
{
		
	void Start ()
	{
		// There are a lot of ways to write data in files
		// and StreamWriter is my favorite. It's really simple
		// and straightforward.
		// Here we open our stream and we are ready to write down stuff.
		// Just keep in mind that we have to close the stream after we are done.
		string mapName = Application.loadedLevelName;
		StreamWriter sw = new StreamWriter (@".\Maps\" + mapName + ".data");
		
		JSON js;
		string json;
		
		// AI Path
		GameObject nextObject = GameObject.Find ("AI Path");
		sw.WriteLine ("AI Path:\n" + TransformToJSON (nextObject).serialized);
		
		// Path Nodes
		foreach (Transform child in nextObject.transform)
		{
			json = TransformToJSON (child.gameObject).serialized;
			sw.WriteLine (child.gameObject.name + ":\n" + json);
		}
		
		// Core
		nextObject = GameObject.Find ("Core");
		js = TransformToJSON (nextObject);
		js["Life"] = nextObject.GetComponent<CoreLife> ().life;
		sw.WriteLine ("Core:\n" + js.serialized);
		
		// Directional light
		nextObject = GameObject.Find ("Directional light");
		js = TransformToJSON (nextObject);
		js["Intensity"] = nextObject.light.intensity;
		sw.WriteLine ("Directional light:\n" + js.serialized);
		
		// Enemies
		nextObject = GameObject.Find ("Enemies");
		sw.WriteLine ("Enemies:\n" + TransformToJSON (nextObject).serialized);
		
		// Enemy Spawner
		nextObject = GameObject.Find ("EnemySpawner");
		js = TransformToJSON (nextObject);
		AISpawner spawnerScript = nextObject.GetComponent<AISpawner> ();
		js["Waves"] = spawnerScript.waves;
		js["Per Wave"] = spawnerScript.perWave;
		js["Interval"] = spawnerScript.interval;
		sw.WriteLine ("Enemy Spawner:\n" + js.serialized);
		
		// Map Logic
		nextObject = GameObject.Find ("MapLogic");
		js = TransformToJSON (nextObject);
		js["Credits"] = GeneralMapLogic.credits;
		sw.WriteLine ("Map Logic:\n" + js.serialized);
				
		// Player View
		nextObject = GameObject.Find ("Player View");
		sw.WriteLine ("Player View:\n" + TransformToJSON (nextObject).serialized);
		
		// Terrain
		nextObject = GameObject.Find ("Terrain");
		sw.WriteLine ("Terrain:\n" + TransformToJSON (nextObject).serialized);
		
		// Terrain children (spawn area, tower holders and walls)
		foreach (Transform child in nextObject.transform)
		{
			if (child.name == "Spawn Area")
			{
				sw.WriteLine ("Spawn Area:\n" + TransformToJSON (child.gameObject).serialized);
			}
			else if (child.name == "Tower Holder")
			{
				sw.WriteLine ("Tower Holder:\n" + TransformToJSON (child.gameObject).serialized);
			}
			else
			{
				sw.WriteLine ("Wall:\n" + TransformToJSON (child.gameObject).serialized);
			}
		}
		
		sw.Close ();
	}
	
	JSON TransformToJSON (GameObject go)
	{
		JSON js = new JSON ();
		js["position"] = (JSON) go.transform.position;
		js["rotation"] = (JSON) go.transform.rotation;
		js["scale"] = (JSON) go.transform.localScale;
		return js;
	}
}
