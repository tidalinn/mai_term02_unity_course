using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class Map : MonoBehaviour
{
    public GameObject prefab;
    public GameObject player;
    public string url;
    public string urlBase = "https://www.google.ru/maps/"; //"https://api.mapbox.com/";
    public string service;
    public string serviceVersion;
    public string pathMapbox = "mapbox";
    public string accessToken = "pk.eyJ1IjoidGlkYWxpbm4iLCJhIjoiY2xndnlwMWNhMGNjZDNmbXVhYmQwbWE2MSJ9.h-QPnFYE2ioc4WatYwvNTA";
    public double userLatitude;
    public double userLongitude;
    public double userLatitudeLast = 0;
    public double userLongitudeLast = 0;
    public double objectLatitude;
    public double objectLongitude;
    public UserGEO userGEO;
    public ObjectGEO objectGEO;

    public void UpdateMap(string url)
    {
        if ((userLatitudeLast == 0 && userLatitudeLast == 0) ||
            (userLatitudeLast != userLatitude || userLongitudeLast != userLongitude))
        {
            StartCoroutine(LoadMap(url));

            userLatitudeLast = userLatitude;
            userLongitudeLast = userLongitude;
        }
    }

    public IEnumerator LoadMap(string url)
    {
        Debug.Log(url);

        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
            Debug.Log("WWW ERROR: " + www.error);
        else
            gameObject.GetComponent<RawImage>().texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
    }

    public string ReplaceComma(double value)
    {
        return value.ToString().Replace(",", ".");
    }
}