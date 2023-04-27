using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class StaticImagesMap : Map
{
    public float zoom = 11f;
    public int bearing = 0;
    public int pitch = 60;
    public enum style { Light, Dark, Streets, Outdoors };
    public style mapStyle = style.Outdoors;
    public int resolution = 2;
    public int width = 400;
    public int height = 400;
    string[] styleStr = new string[] { "light-v10", "dark-v10", "streets-v11", "outdoors-v11" };
    public string userDotColor = "ff0000";
    public string objectDotColor = "00ff11";

    void Start()
    {
        service = "styles";
        serviceVersion = "v1";

        player = GameObject.FindGameObjectWithTag("Player");

        objectGEO = prefab.GetComponent<ObjectGEO>();
        userGEO = player.GetComponent<UserGEO>();

        objectLatitude = objectGEO.Latitude;
        objectLongitude = objectGEO.Longitude;
    }

    void Update()
    {
        userLatitude = userGEO.Latitude;
        userLongitude = userGEO.Longitude;

        ObjectPlacement objectPlacement = player.GetComponent<ObjectPlacement>();
        double distance = objectPlacement.distance;
        string measure = objectPlacement.measure;

        if (measure == "m" && distance < 500)
            zoom = 15f;
        if (measure == "m" && distance < 100)
            zoom = 18f;

        url = urlBase + service + "/" + serviceVersion + "/" + pathMapbox + "/" + 
              styleStr[(int)mapStyle] + "/static" + "/" +
              "pin-s+" + userDotColor + "(" + ReplaceComma(userLongitude) + "," + ReplaceComma(userLatitude) + ")" + "," +
              "pin-s+" + objectDotColor + "(" + ReplaceComma(objectLongitude) + "," + ReplaceComma(objectLatitude) + ")" + "/" +
              ReplaceComma(userLongitude) + "," + ReplaceComma(userLatitude) + "," + zoom + "," + bearing + "," + pitch + "/" +
              width + "x" + height + "@" + resolution + "x" + "?" + 
              "access_token=" + accessToken;    

        UpdateMap(url);
    }
}