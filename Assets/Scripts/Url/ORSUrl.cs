using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class ORSUrl : Url
{
    string urlBase = "https://maps.openrouteservice.org";
    int zoom = 18;

    void Update()
    {
        userLatitude = userGEO.Latitude;
        userLongitude = userGEO.Longitude;
        
        string urlUpdate = GenerateUrl(userLatitude, userLongitude);

        if (url != urlUpdate)
        {
            url = urlUpdate;
        }
    }
    
    private string GenerateUrl(
        double latitude,
        double longitude
    ) {
        return urlBase + "/" + "#" + "/place" + "/@" + ReplaceComma(longitude) + "," + ReplaceComma(latitude) + "," + zoom;
    }
}