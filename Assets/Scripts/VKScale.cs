using UnityEngine;
using System.Collections;

public class VKScale : MonoBehaviour {

	[Range(0, 10)]
	public float scalePower = 1.8f;
	public float scaleOG;
	Vector3 tmpScale;
	//Vector3 startScale;

	// Use this for initialization
	void Start () {

		//startScale = transform.localScale;
		scaleOG = scalePower;
	}
	
	// Update is called once per frame
	void Update () {
		transform.localScale = Vector3.Lerp(transform.localScale, tmpScale, Time.smoothDeltaTime * 16.0f);

	}

	void BeatHit(float RMS) {
		// Debug.Log ("Pow! " + RMS * scalePower);
		tmpScale =  new Vector3(1 + RMS * scalePower, 1 + RMS * scalePower, 1 + RMS * scalePower);
		//tmpScale += startScale;
	}

	void BeatHitPitch(float inPitch) {
		
	}

}
