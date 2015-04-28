using UnityEngine;
using System.Collections;

public class RotateAround : MonoBehaviour {
	
	public Transform rotationPoint;
	public float speed;
	
	// Use this for initialization
	void Start () {

		//speed = 10;
	
	}
	
	// Update is called once per frame
	void Update () {
    	transform.RotateAround (rotationPoint.position, Vector3.up, speed * Time.deltaTime);
	}
}
