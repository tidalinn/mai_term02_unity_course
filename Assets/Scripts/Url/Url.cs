using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using TMPro;

public class Url : MonoBehaviour
{
    public GameObject prefab;
    public GameObject player;
    public UserGEO userGEO;
    public ObjectGEO objectGEO;
    public double userLatitude;
    public double userLongitude;
    public double objectLatitude;
    public double objectLongitude;
    public string url;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        objectGEO = prefab.GetComponent<ObjectGEO>();
        userGEO = player.GetComponent<UserGEO>();

        objectLatitude = objectGEO.Latitude;
        objectLongitude = objectGEO.Longitude;

        userLatitude = userGEO.Latitude;
        userLongitude = userGEO.Longitude;
    }

    public string ReplaceComma(double value)
    {
        return value.ToString().Replace(",", ".");
    }
}