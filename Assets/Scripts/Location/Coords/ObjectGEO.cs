using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectGEO : GEO
{
    public TextMeshProUGUI coords;

    private void Start() {
        coords.text = "Lat: " + Latitude.ToString() + "\nLon: " + Longitude.ToString();
    }
}
