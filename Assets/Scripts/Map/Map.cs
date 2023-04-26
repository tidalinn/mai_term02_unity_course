using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class Map : MonoBehaviour
{
    string url;
    string urlBase = "https://api.mapbox.com/styles/v1/mapbox/";
    public string accessToken = "pk.eyJ1IjoidGlkYWxpbm4iLCJhIjoiY2xndnlwMWNhMGNjZDNmbXVhYmQwbWE2MSJ9.h-QPnFYE2ioc4WatYwvNTA";
    public float latitude; // = 54.899043f;
    public float longitude; // = 37.333929f;
    public float latitudeLast = 0f;
    public float longitudeLast = 0f;
    public float zoom = 18.0f;
    public int bearing = 0;
    public int pitch = 0;
    public enum style { Light, Dark, Streets, Outdoors, Satellite, SatelliteStreets };
    public style mapStyle = style.Streets;
    public enum resolution { low = 1, high = 2 };
    public resolution mapResolution = resolution.high;
    public int mapWidth = 800;
    public int mapHeight = 600;
    string[] styleStr = new string[] { "light-v10", "dark-v10", "streets-v11", "outdoors-v11", "satellite-v9", "satellite-streets-v11" };
    public bool mapIsLoading = false;
    public Rect rect;
    public bool updateMap = true;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator GetMapbox()
    {
        url = urlBase + styleStr[(int)mapStyle] + "/static/" + 
              ReplaceComma(longitude) + "," + ReplaceComma(latitude) + "," + 
              zoom + "," + bearing + "," + pitch + "/" +
              mapWidth + "x" + mapHeight + "?" + 
              "access_token=" + accessToken;

        Debug.Log(url);
        
        mapIsLoading = true;
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("WWW ERROR: " + www.error);
        }
        else
        {
            mapIsLoading = false;
            gameObject.GetComponent<RawImage>().texture = ((DownloadHandlerTexture)www.downloadHandler).texture;

            updateMap = true;
        }
    }

    private string ReplaceComma(float value)
    {
        return value.ToString().Replace(",", ".");
    }
}