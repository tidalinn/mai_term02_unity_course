using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ORSView : WebView
{
    ORSUrl osrUrl;
    string url;
    string objectName = "ORSView";

    GameObject ors;

    void Start()
    {
        osrUrl = GetComponent<ORSUrl>();
        url = osrUrl.url;
        urlText.text = url.ToString();

        StartCoroutine(LoadWebView(objectName, url));

        ors = GameObject.Find(objectName);
    }

    void Update() {
        string urlUpdate = osrUrl.url;

        if (url != urlUpdate)
        {
            url = urlUpdate;
            urlText.text = url.ToString();
        }
    }

    void OnGUI()
    {
        GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
        buttonStyle.fontSize = 28;

        int buttonHeight = 80;
        int buttonWidth = 270;

        if (GUI.Button(
            new Rect(marginLeft + buttonWidth - 100, marginTop - buttonHeight - 20, buttonWidth, buttonHeight), 
            "OpenRouteService",
            buttonStyle)
        ) {
            GameObject navigator = GameObject.Find("NaviGatorView");

            if (navigator != null)
            {
                Destroy(navigator);
            }

            if (ors != null)
            {
                Destroy(ors);
            }

            StartCoroutine(LoadWebView(objectName, url));
        }
    }
}