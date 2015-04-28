using UnityEngine;
using System.Collections;

public class VKColor : MonoBehaviour {

	//Renderer rend;
	public Material objMaterial;
	Color newColor;

	[Range(0, 10)]
	public float colorPower = 1.0f;

	[Range(0, 100)]
	public float speed = 5.0f;

	public bool randomColor;

	// Use this for initialization
	void Start () {
	
		//rend = GetComponent<Renderer>();

	}
	
	// Update is called once per frame
	void Update () {

		//rend.material.SetColor("_Color", Color.Lerp(rend.material.color, newColor, Time.smoothDeltaTime));
		//objMaterial.SetColor("_Color", Color.Lerp(objMaterial.color, newColor, Time.smoothDeltaTime));
		objMaterial.SetColor("_Color", Color.Lerp(objMaterial.color, newColor, Time.deltaTime * speed));

	}

	void BeatHitPitch(float Frequency) {

				
		var hZ = Frequency * colorPower; // small boost
		//Debug.Log ("Color! " + hZ);

		if (!randomColor) {

			if (hZ > 0f && hZ < 480.0f) {
				// red
				newColor = Color.red;
			
			} else if (hZ > 480.0f && hZ < 510.0f) {
				// orange 255-165-0
				newColor = new Color (1.0f, 0.65f, 0.0f, 1.0f);
			
			} else if (hZ > 510.0f && hZ < 540.0f) {
				// yellow
				newColor = Color.yellow;
			
			} else if (hZ > 540.0f && hZ < 610.0f) {
				// green
				newColor = Color.green;
			
			} else if (hZ > 610.0f && hZ < 670.0f) {
				// blue
				newColor = Color.blue;
			
			} else if (hZ > 670.0f && hZ < 750.0f) {
				// violet
				newColor = Color.magenta;
			
			} else if (hZ > 750.0f) {
				// black
				newColor = Color.white;
			
			}

		} else {

			newColor = new Color(Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f));
		}

	}

	void BeatHit(float RMS) {

		
	}
}
