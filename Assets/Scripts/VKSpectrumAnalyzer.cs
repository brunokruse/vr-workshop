using UnityEngine;
using System.Collections;

public class VKSpectrumAnalyzer : MonoBehaviour {

	// Decibels (GetOutputData)
	// RMS: Root mean square
	// Hertz (GetSpectrumData)

	// main settings
	//float refValue = 0.1f; 	 // RMS value for 0 dB
	float sampleRate;		 	 // Audio sample rate
	float threshold = 0.02f; 	 // minimum amplitude to extract pitch
	int precision = 256; 	 	 // qSamples && number of bars
	public float logScale = 8;	 // object scaler

	// spectrums
	float[] samplesL;	// Left Channel
	float[] samplesR;	// Right Channel
	float[] spectrum;	// Frequency spectrum
	public float rmsValue; 		// sound level - RMS -> currently being used for scaling
	public float dbValue; 		// sound level - dB
	public float pitchValue; 	// sound level Hz -> currently being used for colors
	public float lowValue;		// low
	public float midValue;		// mids
	public float highValue;		// highs

	// beat detection
	public float decay = 0.03f; 		// used to adjust rate of the beat fires
	public float beatThreshold = 0.01f; // minimum amount of RMS to fire a beat
	public float beatThresholdMin; 		//  min
	public float fireRate;				// max fire rate of the beat detect / to adjust for accuracy
	private float lastFire;				// keep track of the last time we fired
	public string beatType;				// detecting high, mids and lows

	// viz objects
	public GameObject bar;		// our generic prefab for visualizing our data
	GameObject[] barsSpectrum;	// array of bars for spectrum
	GameObject[] barsOutput;	// array of bars for output RMS power

	// broadcast options
	public bool drawFrequency = false;	// enables/disables the frequency bars
	public bool drawOutput = false;		// enables/disables the RMS / Power bars
	public bool sendPitch = false;		// broadcasts pitch to all listeners
	public bool sendBeat = true;		// broadcasts beat to all listeners
	public bool drawDebug = false;		// displays debug information to console
	

	// Use this for initialization
	void Start () {

		// analysis
		samplesL = new float[precision];
		samplesR = new float[precision];
		spectrum = new float[precision];
		sampleRate = AudioSettings.outputSampleRate;

		// beats
		beatThresholdMin = beatThreshold;
		//fireRate = 0.1f; // maximum rate of fire to avoid false positives

		barsSpectrum = new GameObject[precision];
		barsOutput = new GameObject[precision];


		// instantiate the viz objects
		for (int i = 0; i < precision; i++) {
			barsSpectrum[i] = GameObject.Instantiate(bar, new Vector3(-16.0f + (Mathf.Log((float)i + 1.0f) * logScale), 0.0f, 10.0f), Quaternion.identity) as GameObject;
			barsSpectrum[i].transform.localScale = new Vector3((logScale) / ((float)i + 1.0f), 1.0f, 1.0f);
		}


		for (int i = 0; i < precision; i++) {
			barsOutput[i] = GameObject.Instantiate(bar, new Vector3(16.0f - (i * (32.0f / (float)precision)), 0.0f, -40.0f), Quaternion.identity) as GameObject;	
			barsOutput[i].transform.localScale = new Vector3(32.0f / (float)precision, 1.0f, 1.0f);
		}

	}
	
	// Update is called once per frame
	void Update () {

		// 1. RMS VIZ ----
		getOutputData ();
		for (int i = 0; i < precision; i++)
		{
			if (drawOutput) {
				barsOutput[i].transform.localScale = Vector3.Lerp(barsOutput[i].transform.localScale, new Vector3(32.0f / (float)precision, (samplesL[i] + samplesR[i]) * 10.0f, 1.0f), Time.smoothDeltaTime * 16.0f);
			} else {
				barsOutput[i].transform.localScale = Vector3.zero;
			}

			rmsValue += Mathf.Pow((samplesL[i] + samplesR[i]) / 1.5f, 2.0f);
		}
		rmsValue = Mathf.Sqrt(rmsValue / (float)precision);


		// 2. PITCH VIZ ----
		getSpectrumData ();
		for (int i = 0; i < precision; i++) 
		{
			if (drawFrequency) {
				barsSpectrum[i].transform.localScale = Vector3.Lerp(barsSpectrum[i].transform.localScale, new Vector3(barsSpectrum[i].transform.localScale.x, spectrum[i] * 16.0f * ((float)i + 1.0f), 1.0f), Time.smoothDeltaTime * 16.0f);
			} else {
				barsSpectrum[i].transform.localScale = Vector3.zero;
			}
		}

		// 3. DETECT BEATS ----
		// index caps: low 85, mid 170, high 256
		//getSpectrumDataForRange ("low", 0, 85);
		//getSpectrumDataForRange ("mid", 85, 170);
		//getSpectrumDataForRange ("high", 170, 256);
		detectBeats ();

		// print the RMS for debugging purposes
		//if (drawDebug) { Debug.Log ("RMS: " + rmsValue); }
	}

	void getSpectrumData() {

		// Pass the spectrum data to the spectrum array
		GetComponent<AudioSource>().GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);


		// LOW RANGE
		float maxV = 0;
		int maxN = 0;
		for (int i = 0; i < precision; i++){ // find max 
			if (spectrum[i] > maxV && spectrum[i] > threshold){
				maxV = spectrum[i];
				maxN = i; // maxN is the index of max
			}
		}


		// get the frequency of the max
		float freqN = maxN; // pass the index to a float variable
		if (maxN > 0 && maxN < precision-1){ // interpolate index using neighbours
			var dL = spectrum[maxN-1]/spectrum[maxN];
			var dR = spectrum[maxN+1]/spectrum[maxN];
			freqN += 0.5f * (dR * dR - dL * dL);
		}


		//pitchValue = freqN * AudioSettings.outputSampleRate / precision; // convert index to frequency based on sample rate
		pitchValue = freqN * (sampleRate / 2) / precision;

		if (pitchValue > 0) {
			Debug.Log (pitchValue);
			Debug.Log (maxN);
		}
	}

	void getSpectrumDataForRange(string type, int lowR, int highR) {

		GetComponent<AudioSource>().GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);

		float maxV = 0;
		int maxN = 0;
		for (int i = lowR; i < highR; i++){ // find max 
			if (spectrum[i] > maxV && spectrum[i] > threshold){
				maxV = spectrum[i];
				maxN = i; // maxN is the index of max
			}
		}

		// get the frequency of the max
		float freqN = maxN; // pass the index to a float variable
		if (maxN > lowR && maxN < highR - 1){ // interpolate index using neighbours
			var dL = spectrum[maxN-1]/spectrum[maxN];
			var dR = spectrum[maxN+1]/spectrum[maxN];
			freqN += 0.5f * (dR * dR - dL * dL);
		}
		

		if (type == "low")
			lowValue = freqN * sampleRate / ( precision / 3);
		if (type == "mid")
			midValue = freqN * sampleRate / ( precision / 3);
		if (type == "high")
			highValue = freqN * sampleRate / ( precision / 3);

		//Debug.Log ("LOW: " + lowValue + " MID: " + midValue + " HIGH: " + highValue); 
	
	}

	void getOutputData() {

		// Left and Right channel audio
		GetComponent<AudioSource>().GetOutputData(samplesL, 0); // fill array with samples
		GetComponent<AudioSource>().GetOutputData(samplesR, 1); // fill array with samples

		/*
		 * BK: still working on this
		 * 
		float sum = 0;

		for (int i = 0; i < qSamples; i++){
			sum += samplesL[i] * samplesR[i]; // sum squared samples
		}

		rmsValue = Mathf.Sqrt(sum/qSamples); 			// rms = square root of average
		dbValue = 20 * Mathf.Log10(rmsValue/refValue); 	// calculate dB

		if (dbValue < -160) { 
			dbValue = -160; // clamp it to -160dB min
		}
		*/

	}

	void detectBeats() {

		/*
		 * simple way of detecting spikes in RMS value to qualify as 'beats'

			1. Track a threshold volume level.
			2. If the current volume exceeds the threshold then you have a beat. Set the new threshold to the current volume.
			3. Reduce the threshold ovedwr time, using the Decay Rate.
			4. Wait for the Hold Time before detecting for the next beat. This can help reduce false positives.

		*/


		if (rmsValue > beatThreshold && Time.time > fireRate + lastFire) {
			beatThreshold = rmsValue;

			if (sendBeat)
				BroadcastMessage ("BeatHit", rmsValue);

			// send the pitch
			if (pitchValue != 0 && sendPitch) {
				BroadcastMessage ("BeatHitPitch", pitchValue);
				if (drawDebug) { Debug.Log ("PITCH: " + pitchValue); } // debug log
			}

			lastFire = Time.time;

		} else {
		
		}

		if (beatThreshold >= beatThresholdMin) {
			beatThreshold -= decay;
		}

	}


}
