using UnityEngine;
using System.Collections;

public class AutoRotate : MonoBehaviour {

	public float X = 0.0f;
	public float Y = 0.0f;
	public float Z = 0.0f;
	public float globalSpeed = 1.0f;

	// Use this for initialization
	void Start () {
	
		globalSpeed = 1.0f;

	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(new Vector3(Random.Range(X * 0.1f, X) * Time.deltaTime * globalSpeed, Random.Range(Y * 0.1f, Y) * Time.deltaTime * globalSpeed, Random.Range(Z * 0.1f, Z) * Time.deltaTime * globalSpeed ) );
	}
}
