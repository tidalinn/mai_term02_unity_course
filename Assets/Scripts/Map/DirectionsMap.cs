using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class DirectionsMap : Map
{
    public enum profile { traffic, driving, walking, cycling };
    public profile routingProfile = profile.walking;
    Boolean alternatives = true;
    string geometries = "geojson";
    string language = "ru";
    string overview = "simplified";
    Boolean steps = true;

    void Start()
    {
        service = "directions";
        serviceVersion = "v5";
    }

    void Update()
    {
        userLatitude = 54.898995062680655;
        userLongitude = 37.33393013477326;

        url = urlBase + service + "/" + serviceVersion + "/" + pathMapbox + "/" + routingProfile + "/" + 
              ReplaceComma(userLongitude) + "," + ReplaceComma(userLatitude) + ";" +
              ReplaceComma(objectLongitude) + "," + ReplaceComma(objectLatitude) +
              "?alternatives=" + alternatives.ToString() + "&geometries="  + geometries + 
              "&language=" + language + "&overview=" + overview + "&steps=" + steps.ToString() +
              "&access_token=" + accessToken;
    

        UpdateMap(url);
    }
}