using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationCreate : MonoBehaviour
{
    private double currentLat;
    private double currentLon;
    public Text statusText;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FetchLocationData());
    }

    private IEnumerator FetchLocationData(){
        // Check if user has location service enabled
        if (!Input.location.isEnabledByUser)
            yield break;

        // Start service before querying location
        Input.location.Start();
        statusText.text = "Fetching Location..";

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            statusText.text = "Location Timed out";
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            statusText.text = "Unable to determine device location";
            yield break;
        }
        else{
            currentLat = Input.location.lastData.latitude;
            currentLon = Input.location.lastData.longitude;
            statusText.text = currentLat.ToString() + "," + currentLon.ToString();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
