using UnityEngine;

public class GEO : MonoBehaviour
{
    public double Latitude;
    public double Longitude;

    public string ReplaceDot(string value)
    {
        return value.Replace(".", ",");
    }
}