using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ObjectGEO : GEO
{
    public TMP_InputField LatitudeInput;
    public TMP_InputField LongitudeInput;

    private void Start() {
        LatitudeInput.text = Latitude.ToString();
        LongitudeInput.text = Longitude.ToString();
    }

    private void Update() {
        double latInput = Convert.ToDouble(LatitudeInput.text);
        double lonInput = Convert.ToDouble(LongitudeInput.text);

        if (Latitude != latInput || Longitude != lonInput) {
            Latitude = latInput;
            Longitude = lonInput;
        }
    }
}
