using UnityEngine;
using UnityEngine.Android;
using System.Collections;
using TMPro;

public class GEOLocation : MonoBehaviour
{
    public TextMeshProUGUI message;
    public TextMeshProUGUI location;

    private void Start()
    {
            if (!Permission.HasUserAuthorizedPermission (Permission.FineLocation))
            {
                Permission.RequestUserPermission (Permission.FineLocation);
            }
            StartCoroutine(StartLocationService());
    }

    private IEnumerator StartLocationService()
    {
            if (!Input.location.isEnabledByUser)
            {
                message.text = "User has not enabled location";
                yield break;
            }

            Input.location.Start();

            while(Input.location.status == LocationServiceStatus.Initializing)
            {
                yield return new WaitForSeconds(1);
            }

            if (Input.location.status == LocationServiceStatus.Failed)
            {
                message.text = "Unable to determine device location";
                yield break;
            }

            location.text = "Latitude : " + Input.location.lastData.latitude + " Longitude : " + Input.location.lastData.longitude + " Altitude : " + Input.location.lastData.altitude;
    }
}