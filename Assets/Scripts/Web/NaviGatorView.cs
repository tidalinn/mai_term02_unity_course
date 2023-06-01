using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NaviGatorView : WebView
{
    NaviGatorUrl navigatorUrl;
    string url;
    string objectName = "NavigatorView";
    GameObject navigator;

    void Start()
    {
        navigatorUrl = GetComponent<NaviGatorUrl>();
        url = navigatorUrl.url;
        urlText.text = url.ToString();

        StartCoroutine(LoadWebView(objectName, url));

        navigator = GameObject.Find(objectName);
    }

    void Update() {
        string urlUpdate = navigatorUrl.url;

        if (url != urlUpdate)
        {
            url = urlUpdate;
            urlText.text = url.ToString();

            Destroy(navigator);
            StartCoroutine(LoadWebView(objectName, url));
        }
    }

    void OnGUI()
    {
        GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
        buttonStyle.fontSize = 28;

        int buttonHeight = 80;
        int buttonWidth = 160;

        if (GUI.Button(
            new Rect(marginLeft, marginTop - buttonHeight - 20, buttonWidth, buttonHeight), 
            "NaviGator",
            buttonStyle)
        ) {
            GameObject ors = GameObject.Find("ORSView");

            if (ors != null)
            {
                Destroy(ors);
            }

            if (navigator != null)
            {
                Destroy(navigator);
            }
            
            StartCoroutine(LoadWebView(objectName, url));
        }
    }
}