using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ObjectPlacement : MonoBehaviour
{
    public GameObject prefab;
    public TextMeshProUGUI distanceFullText;
    public TextMeshProUGUI distanceText;
    ObjectGEO objectGEO;
    UserGEO userGEO;
    DistanceCalculation distanceCalculation;
    float distance;
    public float radiusToPlace = 80f;
    public float radiusOnSpot = 20f;

    void Start()
    {
        distanceCalculation = GetComponent<DistanceCalculation>();

        objectGEO = prefab.GetComponent<ObjectGEO>();
        userGEO = GetComponent<UserGEO>();
    }

    void Update()
    {
        distance = distanceCalculation.CalculateDistance(
            userGEO.Latitude,
            userGEO.Longitude,
            objectGEO.Latitude,
            objectGEO.Longitude
        );

        distanceFullText.text = "Distance: " + distance.ToString();
        
        distance = RoundDistance(distance);
        distanceText.text = CheckDistance(distance);

        if (distance < radiusToPlace)
        {
            Vector3 objectPosition = prefab.transform.position;

            prefab.transform.position = new Vector3(objectPosition.x, objectPosition.y, distance);
            prefab.SetActive(true);
        }
        else
            prefab.SetActive(false);
    }

    private float RoundDistance(float value)
    {
        if (value >= 1f)
            return (float)Math.Round(value, 0);
        else
            return (float)Math.Round(value * 1000, 0); 
    }

    private string CheckDistance(float value)
    {
        if (value >= 1000)
        {
            return value.ToString() + "км";
        }
        else if (value <= radiusOnSpot)
        {
            return "Вы на месте";
        }
        else
        {
            return value.ToString() + "м";
        }
    }
}
