using UnityEngine;
using System.Collections;

public class AutoRotate : MonoBehaviour {

	[Range(0, 100)]
	public float X = 0.0f;
	[Range(0, 100)]
	public float Y = 0.0f;
	[Range(0, 100)]
	public float Z = 0.0f;
	public float globalSpeed = 1.0f;

	// Use this for initialization
	void Start () {
	
		globalSpeed = 1.0f;

	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(new Vector3(X * Time.smoothDeltaTime * globalSpeed,
		                             Y * Time.smoothDeltaTime * globalSpeed, 
		                             Z * Time.smoothDeltaTime * globalSpeed ) );
	}
}
