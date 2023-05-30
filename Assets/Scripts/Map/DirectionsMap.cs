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
        //service = "directions";
        //serviceVersion = "v5";

        player = GameObject.FindGameObjectWithTag("Player");

        objectGEO = prefab.GetComponent<ObjectGEO>();
        userGEO = player.GetComponent<UserGEO>();

        objectLatitude = objectGEO.Latitude;
        objectLongitude = objectGEO.Longitude;
    }

    void Update()
    {
        userLatitude = 54.902017901828245;
        userLongitude = 37.361819744110115;

        ObjectPlacement objectPlacement = player.GetComponent<ObjectPlacement>();
        //double distance = objectPlacement.distance;
        //string measure = objectPlacement.measure;

        url = urlBase + "dir/" + "\'" + ReplaceComma(userLatitude) + "," + ReplaceComma(userLongitude) + "\'" + 
              "/" + ReplaceComma(objectLatitude) + "," + ReplaceComma(objectLongitude) + "/" +
              "am=t" + "/";

        // https://www.google.ru/maps/dir/'54.89903207840987,37.333860397338874'/54.9019771,37.3617049/@54.9008153,37.3375318,15z/am=t/data=!4m9!4m8!1m5!1m1!1s0x0:0x7698b90aa89785a2!2m2!1d37.3338604!2d54.8990321!1m0!3e2?entry=ttu



        // https://www.google.ru/maps/dir/'54.89903207840987,37.333860397338874'/54.9019771,37.3617049/@54.9008153,37.3375318,15z/data=!3m1!4b1!4m7!4m6!1m3!2m2!1d37.3338604!2d54.8990321!1m0!3e2?entry=ttu
        // https://www.google.ru/maps/dir/'54.89903207840987,37.333860397338874'/54.9019771,37.3617049/@54.9001943,37.3305441,14z/data=!3m1!4b1!4m6!4m5!1m3!2m2!1d37.3338604!2d54.8990321!1m0?entry=ttu
        /*

        /*
        url = urlBase + service + "/" + serviceVersion + "/" + pathMapbox + "/" + routingProfile + "/" + 
              ReplaceComma(userLongitude) + "," + ReplaceComma(userLatitude) + ";" +
              ReplaceComma(objectLongitude) + "," + ReplaceComma(objectLatitude) +
              "?alternatives=" + alternatives.ToString() + "&geometries="  + geometries + 
              "&language=" + language + "&overview=" + overview + "&steps=" + steps.ToString() +
              "&access_token=" + accessToken;
        */

        UpdateMap(url);
    }
}