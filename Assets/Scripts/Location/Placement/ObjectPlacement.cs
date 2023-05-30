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
    public double distanceToPlace = 80.0;
    public double distanceToSpot = 20.0;
    public string measure;

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

        distanceFullText.text = distance.ToString();
        
        distance = RoundDistance(distance);
        distanceText.text = CheckDistance(distance);

        Vector3 objectPosition = prefab.transform.position;
        
        if (measure == "km")
            distance *= 1000;

        prefab.transform.position = new Vector3(objectPosition.x, objectPosition.y, (float)distance);
    }

    private double RoundDistance(double value)
    {
        if (value >= 1.0)
        {
            measure = "km";
            return (double)Math.Round(value, 1);
        }
        else
        {
            measure = "m";
            return (double)Math.Round(value * 1000, 0); 
        }
    }

    private string CheckDistance(double value)
    {
        if (measure == "km")
        {
            return value.ToString() + "км";
        }
        else if (measure == "m" && value < distanceToSpot)
        {
            return "Вы на месте";
        }
        else
        {
            return value.ToString() + "м";
        }
    }
}
