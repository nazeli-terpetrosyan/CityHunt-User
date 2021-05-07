using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Kudan.AR.Samples;
using System;


public class DetectLocation : MonoBehaviour {
	
	private float distanceFromTarget = 1.5f;
	private double proximity = 0.5;
	private float dLatitude, dLongitude;
	public float tLatitude = 40.202657f, tLongitude = 44.492338f;
	private bool enableByRequest = true;
	public int maxWait = 10;
	public bool ready = false;
	public Text text;
	public SampleApp sa;

	public bool Card = false;

	void Start(){
		StartCoroutine (getLocation ());
	}

	IEnumerator getLocation(){
		LocationService service = Input.location;
		if (!enableByRequest && !service.isEnabledByUser) {
			Debug.Log("Location Services not enabled by user");
			yield break;
		}
		service.Start();
		while (service.status == LocationServiceStatus.Initializing && maxWait > 0) {
			yield return new WaitForSeconds(1);
			maxWait--;
		}
		if (maxWait < 1){
			Debug.Log("Timed out");
			yield break;
		}
		if (service.status == LocationServiceStatus.Failed) {
			Debug.Log("Unable to determine device location");
			yield break;

		} else {
			text.text = "Target Location : "+dLatitude + ", "+dLongitude+"\nMy Location: " + service.lastData.latitude + ", " + service.lastData.longitude;
			dLatitude = service.lastData.latitude;
			dLongitude = service.lastData.longitude;
		}
		//service.Stop();
		ready = true;
		startCalculate ();
	}


	void Update(){

	}


	public void startCalculate(){
        //Haversince formula
        double R = 6371e3; //circumference of Earth in meters
        
        double φ1 = dLatitude * System.Math.PI/180; // φ, λ in radians
        double φ2 = tLatitude * System.Math.PI/180;
        double Δφ = (tLatitude-dLatitude) * System.Math.PI/180;
        double Δλ = (tLongitude-dLongitude) * System.Math.PI/180;

        double a = System.Math.Sin(Δφ/2) * Math.Sin(Δφ/2) +
                System.Math.Cos(φ1) * System.Math.Cos(φ2) *
                System.Math.Sin(Δλ/2) * System.Math.Sin(Δλ/2);
        double c = 2 * System.Math.Atan2(System.Math.Sqrt(a), System.Math.Sqrt(1-a));

        proximity = R * c; // in metres

		if (proximity <= distanceFromTarget) {
			text.text = text.text + "\nDistance : " + proximity.ToString ();
			text.text += "\nTarget Detected";
			Card = true;
			sa.StartClicked ();
		} else {
			text.text = text.text + "\nDistance : " + proximity.ToString ();
			text.text += "\nTarget not detected, too far!";
		}
	}
}