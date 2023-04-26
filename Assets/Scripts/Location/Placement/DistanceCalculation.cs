using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DistanceCalculation: MonoBehaviour
{
    float km_per_degree_meridian = 111.1f;
    float km_per_degree_equator = 111.3f;

    public float CalculateDistance(
        float userLatitude, 
        float userLongitude, 
        float objectLatitude, 
        float objectLongitude)
    {
        // latitude - широта
        // longitude - долгота

        // object -> B C
        //          A   D <- User

        // (float, float) A = (objectLatitude, userLongitude);
        // (float, float) B = (userLatitude, userLongitude);
        // (float, float) C = (userLatitude, objectLongitude);
        // (float, float) D = (objectLatitude, objectLongitude);

        float degrees_latitude_diff = userLatitude - objectLatitude;
        float km_latitude_diff = degrees_latitude_diff * km_per_degree_meridian;

        float objectLatitudeCos = (float)Math.Cos(Math.PI * objectLatitude / 180.0);
        float userLatitudeCos = (float)Math.Cos(Math.PI *userLatitude / 180.0);

        float degree_top = km_per_degree_equator * userLatitudeCos;
        float degree_bottom = km_per_degree_equator * objectLatitudeCos;

        float degrees_longitude_diff = objectLongitude - userLongitude;

        float BC = degrees_longitude_diff * degree_top;
        float AD = degrees_longitude_diff * degree_bottom;

        //   B C
        // A H  D

        float AB = km_latitude_diff;
        float AH = (AD - BC) / 2;

        float AB_2 = AB * AB;
        float AH_2 = AH * AH;
        float BH_2 = AB_2 - AH_2;
        float BH = (float)Math.Sqrt(BH_2);

        float HD = AD - AH;

        float HD_2 = HD * HD;
        float BD_2 = HD_2 + BH_2;
        float BD = (float)Math.Sqrt(BD_2);

        return BD;
    }
}
