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
    public double distance;
    public double distanceToPlace = 80;
    public double distanceToSpot = 20;

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

        if (distance < distanceToPlace)
        {
            Vector3 objectPosition = prefab.transform.position;

            prefab.transform.position = new Vector3(objectPosition.x, objectPosition.y, (float)distance);
            prefab.SetActive(true);
        }
        else
            prefab.SetActive(false);
    }

    private double RoundDistance(double value)
    {
        if (value >= 1f)
            return (double)Math.Round(value, 0);
        else
            return (double)Math.Round(value * 1000, 0); 
    }

    private string CheckDistance(double value)
    {
        if (value >= 1000f)
        {
            return value.ToString() + "км";
        }
        else if (value < distanceToSpot)
        {
            return "Вы на месте";
        }
        else
        {
            return value.ToString() + "м";
        }
    }
}
