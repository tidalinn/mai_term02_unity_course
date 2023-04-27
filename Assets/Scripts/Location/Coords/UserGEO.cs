using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Android;

public class UserGEO : GEO
{
    public TextMeshProUGUI message;
    public TextMeshProUGUI coords;
    bool isUpdating;

    void Update()
    {
        if (!isUpdating)
        {
            StartCoroutine(GetLocation());
            isUpdating = !isUpdating;
        }
    }

    IEnumerator GetLocation()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
            Permission.RequestUserPermission(Permission.CoarseLocation);
        }

        // Check if user has location service enabled
        if (!Input.location.isEnabledByUser)
            yield return new WaitForSeconds(5);

        // Start service before querying location
        Input.location.Start();

        int maxWait = 10;

        // Wait until service initializes
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            message.text = "Timed out";
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            message.text = "Unable to determine device location";
            yield break;
        }
        else
        {
            message.text = "Success";
            Latitude = Input.location.lastData.latitude;
            Longitude = Input.location.lastData.longitude;

            //Latitude = 54.89895033529577;
            //Longitude = 37.33385235071183;
            
            coords.text = "Lat: " + Latitude.ToString() + "\nLon: " + Longitude.ToString();
        }

        // Stop service if there is no need to query location updates continuously
        isUpdating = !isUpdating;
        Input.location.Stop();
    }
}