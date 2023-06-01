/*
 * Copyright (C) 2012 GREE, Inc.
 * 
 * This software is provided 'as-is', without any express or implied
 * warranty.  In no event will the authors be held liable for any damages
 * arising from the use of this software.
 * 
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 * 
 * 1. The origin of this software must not be misrepresented; you must not
 *    claim that you wrote the original software. If you use this software
 *    in a product, an acknowledgment in the product documentation would be
 *    appreciated but is not required.
 * 2. Altered source versions must be plainly marked as such, and must not be
 *    misrepresented as being the original software.
 * 3. This notice may not be removed or altered from any source distribution.
 */

using System.Collections;
using UnityEngine;
#if UNITY_2018_4_OR_NEWER
using UnityEngine.Networking;
#endif
using UnityEngine.UI;
using TMPro;

public class WebView : MonoBehaviour
{
    public TextMeshProUGUI statusText;
    public TextMeshProUGUI urlText;
    public int size;
    public int marginLeft;
    public int marginTop;
    public int marginRight = 20;
    public int marginBottom = 20;
    WebViewObject webViewObject;

    public IEnumerator LoadWebView(
        string objectName,
        string url
    ) {
        webViewObject = (new GameObject(objectName)).AddComponent<WebViewObject>();

        webViewObject.Init(
            cb: (msg) =>
            {
                Debug.Log(string.Format("CallFromJS[{0}]", msg));
                statusText.text = msg;
                statusText.GetComponent<Animation>().Play();
            },
            err: (msg) =>
            {
                Debug.Log(string.Format("CallOnError[{0}]", msg));
                statusText.text = msg;
                statusText.GetComponent<Animation>().Play();
            },
            httpErr: (msg) =>
            {
                Debug.Log(string.Format("CallOnHttpError[{0}]", msg));
                statusText.text = msg;
                statusText.GetComponent<Animation>().Play();
            },
            started: (msg) =>
            {
                Debug.Log(string.Format("CallOnStarted[{0}]", msg));
            },
            hooked: (msg) =>
            {
                Debug.Log(string.Format("CallOnHooked[{0}]", msg));
            },
            cookies: (msg) =>
            {
                Debug.Log(string.Format("CallOnCookies[{0}]", msg));
            },
            ld: (msg) =>
            {
                Debug.Log(string.Format("CallOnLoaded[{0}]", msg));

#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX || UNITY_IOS
                // NOTE: the following js definition is required only for UIWebView; if
                // enabledWKWebView is true and runtime has WKWebView, Unity.call is defined
                // directly by the native plugin.
#if true
                var js = @"
                    if (!(window.webkit && window.webkit.messageHandlers)) {
                        window.Unity = {
                            call: function(msg) {
                                window.location = 'unity:' + msg;
                            }
                        };
                    }
                ";
#else
                // NOTE: depending on the situation, you might prefer this 'iframe' approach.
                // cf. https://github.com/gree/unity-webview/issues/189
                var js = @"
                    if (!(window.webkit && window.webkit.messageHandlers)) {
                        window.Unity = {
                            call: function(msg) {
                                var iframe = document.createElement('IFRAME');
                                iframe.setAttribute('src', 'unity:' + msg);
                                document.documentElement.appendChild(iframe);
                                iframe.parentNode.removeChild(iframe);
                                iframe = null;
                            }
                        };
                    }
                ";
#endif

#elif UNITY_WEBPLAYER || UNITY_WEBGL
                var js = @"
                    window.Unity = {
                        call:function(msg) {
                            parent.unityWebView.sendMessage('WebViewObject', msg);
                        }
                    };
                ";
#else
                var js = "";
#endif
                webViewObject.EvaluateJS(js + @"Unity.call('ua=' + navigator.userAgent)");
            }
        );

#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
        webViewObject.bitmapRefreshCycle = 1;
#endif
        size = (int)(Screen.height / 2);
        marginLeft = Screen.width - size - marginRight;
        marginTop = Screen.height - size - marginBottom;

        Debug.Log(Screen.height);

        webViewObject.SetMargins(marginLeft, marginTop, marginRight, marginBottom);
        webViewObject.SetTextZoom(100);  // android only. cf. https://stackoverflow.com/questions/21647641/android-webview-set-font-size-system-default/47017410#47017410
        webViewObject.SetVisibility(true);

#if !UNITY_WEBPLAYER && !UNITY_WEBGL

        if (url.StartsWith("http")) 
        {
            webViewObject.LoadURL(url.Replace(" ", "%20"));
        } 
        else 
        {
            var exts = new string[] {
                ".jpg",
                ".js",
                ".html"  // should be last
            };

            foreach (var ext in exts) 
            {
                var url_ext = url.Replace(".html", ext);
                var src = System.IO.Path.Combine(Application.streamingAssetsPath, url_ext);
                var dst = System.IO.Path.Combine(Application.temporaryCachePath, url_ext);
                byte[] result = null;

                if (src.Contains("://")) 
                {  // for Android

#if UNITY_2018_4_OR_NEWER

                    // NOTE: a more complete code that utilizes UnityWebRequest can be found in https://github.com/gree/unity-webview/commit/2a07e82f760a8495aa3a77a23453f384869caba7#diff-4379160fa4c2a287f414c07eb10ee36d
                    var unityWebRequest = UnityWebRequest.Get(src);
                    yield return unityWebRequest.SendWebRequest();

                    result = unityWebRequest.downloadHandler.data;
#else
                    var www = new WWW(src);
                    yield return www;
                    result = www.bytes;
#endif
                } 
                else 
                {
                    result = System.IO.File.ReadAllBytes(src);
                }

                System.IO.File.WriteAllBytes(dst, result);

                if (ext == ".html") 
                {
                    webViewObject.LoadURL("file://" + dst.Replace(" ", "%20"));
                    break;
                }
            }
        }
#else
        if (url.StartsWith("http")) {
            webViewObject.LoadURL(url.Replace(" ", "%20"));
        } else {
            webViewObject.LoadURL("StreamingAssets/" + url.Replace(" ", "%20"));
        }
#endif
        yield break;
    }
}
