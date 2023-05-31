using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class NaviGatorUrl : Url
{
    string urlBase = "https://navi-gator.online";

    void Update()
    {
        string urlUpdate = GenerateUrl(objectLatitude, objectLongitude);

        if (url != urlUpdate)
        {
            url = urlUpdate;
        }
    }
    
    private string GenerateUrl(
        double latitude,
        double longitude
    ) {
        return urlBase + "/" + "?" + "l=" + ReplaceComma(latitude) + "," + ReplaceComma(longitude);
    }
}