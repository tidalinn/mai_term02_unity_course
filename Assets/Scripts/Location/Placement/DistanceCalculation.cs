using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DistanceCalculation: MonoBehaviour
{
    double km_per_degree_meridian = 111.1;
    double km_per_degree_equator = 111.3;

    public double CalculateDistance(
        double userLatitude, 
        double userLongitude, 
        double objectLatitude, 
        double objectLongitude)
    {
        // latitude - широта ↑ Y
        // longitude - долгота → X

        // object ->  B C
        //          A     D <- User

        // (float, float) A = (objectLatitude, userLongitude);
        // (float, float) B = (userLatitude, userLongitude);
        // (float, float) C = (userLatitude, objectLongitude);
        // (float, float) D = (objectLatitude, objectLongitude);

        double degrees_latitude_diff = userLatitude - objectLatitude;
        double km_latitude_diff = degrees_latitude_diff * km_per_degree_meridian;

        double objectLatitudeCos = (double)Math.Cos(Math.PI * objectLatitude / 180.0);
        double userLatitudeCos = (double)Math.Cos(Math.PI * userLatitude / 180.0);

        double degree_top = km_per_degree_equator * userLatitudeCos;
        double degree_bottom = km_per_degree_equator * objectLatitudeCos;

        double degrees_longitude_diff = objectLongitude - userLongitude;

        double BC = degrees_longitude_diff * degree_top;
        double AD = degrees_longitude_diff * degree_bottom;

        //   B C
        // A H  D

        double AB = km_latitude_diff;
        double AH = (AD - BC) / 2;

        double AB_2 = AB * AB;
        double AH_2 = AH * AH;
        double BH_2 = AB_2 - AH_2;
        double BH = (double)Math.Sqrt(BH_2);

        double HD = AD - AH;

        double HD_2 = HD * HD;
        double BD_2 = HD_2 + BH_2;
        double BD = (double)Math.Sqrt(BD_2);

        return BD;
    }
}
