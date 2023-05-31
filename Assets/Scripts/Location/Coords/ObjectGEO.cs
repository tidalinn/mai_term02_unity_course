using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ObjectGEO : GEO
{
    public TMP_InputField LatitudeInput;
    public TMP_InputField LongitudeInput;

    private void Start() 
    {
        LatitudeInput.text = Latitude.ToString();
        LongitudeInput.text = Longitude.ToString();
    }

    private void Update() 
    {
        double latitudeInput = Convert.ToDouble(ReplaceDot(LatitudeInput.text));
        double longitudeInput = Convert.ToDouble(ReplaceDot(LongitudeInput.text));

        if (Latitude != latitudeInput || Longitude != longitudeInput) 
        {
            Latitude = latitudeInput;
            Longitude = longitudeInput;
        }
    }
}
