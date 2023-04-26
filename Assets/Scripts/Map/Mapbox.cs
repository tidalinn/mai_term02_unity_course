using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class Mapbox : Map
{
    UserGEO userGEO;
    float threshold = 0.00001f;
    
    void Start()
    {
        userGEO = GameObject.FindGameObjectWithTag("Player").GetComponent<UserGEO>();
    }

    void Update()
    {
        latitude = userGEO.Latitude;
        longitude = userGEO.Longitude;

        if (latitudeLast == 0f && longitudeLast == 0f)
            LoadMap();

        if (latitudeLast != latitude || longitudeLast != longitude)
            LoadMap();
    }

    private void LoadMap()
    {
        rect = gameObject.GetComponent<RawImage>().rectTransform.rect;

            mapWidth = (int)Math.Round(rect.width);
            mapHeight = (int)Math.Round(rect.height);

            StartCoroutine(GetMapbox());
            updateMap = false;

            latitudeLast = userGEO.Latitude;
            longitudeLast = userGEO.Longitude;
    }
}