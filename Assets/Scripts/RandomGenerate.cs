using UnityEngine;
using System.Collections;

public class RandomGenerate : MonoBehaviour {

	public GameObject prefab;
	public float count = 100;

	// Use this for initialization
	void Start () {

		for (int x = 0; x < count; x++) {


			GameObject myCube = Instantiate(prefab, Random.insideUnitSphere * 100, Quaternion.identity) as GameObject;
			myCube.transform.parent = transform;

			//Transform t = Instantiate(prefab) as Transform; // instantiate prefab and get its transform
			//t.parent = transform; // group the instance under the spawner
			//t.localPosition =  Random.insideUnitSphere * 100; //Vector3.zero; // make it at the exact position of the spawner
			//t.localRotation = Quaternion.identity; // same for rotation
			//t.gameObject.name = "My Awesome Instance!";

			//obj.transform.localScale = Vector3.one;
			//GameObject obj = (GameObject)Resources.Load ("ColorCube", typeof(GameObject));
			//obj.transform.localScale = Vector3.one;
			//obj.transform.position = Random.insideUnitSphere * 100;

			// Add the instance in the hierarchy
			
			// Find the instantiate prefab and asign the parent


			//Instantiate(prefab, Random.insideUnitSphere * 100, Quaternion.identity);

			//t.transform.parent = GameObject.Find ("SpectrumAnalyzer").transform;

			//Instantiate(prefab, new Vector3(Random(-100,100), Random(-100,100), Random(-100,100)), Quaternion.identity);
			
		}

	}

	// Update is called once per frame
	void Update () {
	
	}
}
