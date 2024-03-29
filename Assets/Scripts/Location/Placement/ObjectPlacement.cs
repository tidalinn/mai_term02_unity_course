using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ObjectPlacement : MonoBehaviour
{
    public GameObject prefab;
    public TextMeshProUGUI distanceFullText;
    public TextMeshProUGUI distanceTextLabel;
    public TextMeshProUGUI distanceText;
    public TextMeshProUGUI placementText;
    ObjectGEO objectGEO;
    UserGEO userGEO;
    DistanceCalculation distanceCalculation;
    PositionCalculation positionCalculation;
    public double distance;
    public Vector3 position;
    public double distanceToApproach = 30.0;
    public string measure;
    Vector3 positionFixed;

    void Start()
    {
        distanceCalculation = GetComponent<DistanceCalculation>();
        positionCalculation = GetComponent<PositionCalculation>();

        objectGEO = prefab.GetComponent<ObjectGEO>();
        userGEO = GetComponent<UserGEO>();

        placementText.text = prefab.transform.position.ToString();
    }

    void Update()
    {
        // calculate distance from user to the object
        distance = distanceCalculation.CalculateDistance(
            userGEO.Latitude,
            userGEO.Longitude,
            objectGEO.Latitude,
            objectGEO.Longitude
        );

        distanceFullText.text = distance.ToString();
        
        distance = RoundDistance(distance);
        distanceText.text = CheckDistance(distance);

        if (measure == "km")
        {
            distance *= 1000;
        }

        // get object position regarding the user
        position = positionCalculation.CalculatePosition(
            prefab.transform.position,
            distance,
            userGEO.Latitude,
            userGEO.Longitude,
            objectGEO.Latitude,
            objectGEO.Longitude
        );

        if (distance <= distanceToApproach)
        {
            Vector3 positionFixed = prefab.transform.position;
            prefab.transform.position = new Vector3(0, positionFixed.y, (float)distance);;
            placementText.text = prefab.transform.position.ToString();
        }
        else
        {
            if (!float.IsNaN(position.x) && !float.IsNaN(position.z)) 
            {
                prefab.transform.position = position;
                placementText.text = prefab.transform.position.ToString();
            }
        }
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
        if (objectGEO.Latitude != 0 && objectGEO.Longitude != 0)
        {
            distanceTextLabel.text = "По прямой до объекта:";

            if (measure == "km")
            {
                return value.ToString() + "км";
            }
            else if (measure == "m" && value < distanceToApproach)
            {
                return "Вы на месте";
            }
            else
            {
                return value.ToString() + "м";
            }
        }
        else
        {
            distanceTextLabel.text = "";
            return "";
        }
    }
}
