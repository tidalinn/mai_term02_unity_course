using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class PositionCalculation: MonoBehaviour
{
    public TextMeshProUGUI objectCoordsText;

    public Vector3 CalculatePosition(
        Vector3 position,
        double distance,
        double userLatitude, 
        double userLongitude, 
        double objectLatitude, 
        double objectLongitude
    ) {
        // latitude - широта ↑ Y
        // longitude - долгота → X

        //       Y
        //       |
        // -X ------- X
        //       |
        //      -Y

        // A - user position
        // B - destination

        (double, double) A = (userLongitude, userLatitude);
        (double, double) B = (objectLongitude, objectLatitude);

        double AB = A.Item1 * B.Item1 + A.Item2 * B.Item2;

        double A_len = (double)Math.Sqrt(A.Item1 * A.Item1) + (double)Math.Sqrt(A.Item2 * A.Item2);
        double B_len = (double)Math.Sqrt(B.Item1 * B.Item1) + (double)Math.Sqrt(B.Item2 * B.Item2);

        double A_len_B_len = A_len * B_len;
        double AB_A_len_B_len = AB / A_len_B_len;

        double A_cos = (double)Math.Cos(Math.PI * AB_A_len_B_len / 180.0);
        double A_sin = (double)Math.Sin(Math.PI * AB_A_len_B_len / 180.0);

        // user as (0, 0)
        (int, int) sign = GetCoordsLocation(A, B);
        sign = (sign.Item1, sign.Item2);

        float x = (float)(sign.Item1 * (A_cos * distance + distance) / 2);
        float z = (float)(sign.Item2 * (A_sin * distance + distance));
        position = new Vector3(x, position.y, z);

        return position;
    }

    (int, int) GetCoordsLocation(
        (double, double) A,
        (double, double) B
    ) {        
        if (B.Item1 > A.Item1) // -> X
        {
            // user
            //     object
            if (B.Item2 < A.Item2)
            {
                return (-1, 1);
            }
            //     object
            // user
            else if (B.Item2 > A.Item2)
            {
                return (-1, -1);
            }
            // user object
            else
            {
                return (-1, 0);
            }
        }
        else if (B.Item1 < A.Item1)
        {
            //       user
            // object
            if (B.Item2 < A.Item2)
            {
                return (1, 1);
            }
            // object
            //       user
            else if (B.Item2 > A.Item2)
            {
                return (1, -1);
            }
            // object user
            else
            {
                return (1, 0);
            }
        }
        else // B.Item1 == A.Item1
        {
            // object
            // user
            if (B.Item2 > A.Item2)
            {
                return (0, -1);
            }
            // user
            // object
            else if (B.Item2 < A.Item2)
            {
                return (0, 1);
            }
            else // B.Item2 == A.Item2
            {
                return (0, 0);
            }
        }
    }
}