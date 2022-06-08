using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using WebRequest = MoralisUnity.Platform.Services.Models.WebRequest;
using UnityEngine;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;
using MoralisUnity.Platform.Abstractions;
using Newtonsoft.Json;

namespace MoralisUnity.Platform.Services
{
    /// <summary>
    /// A universal implementation of <see cref="IWebClient"/>.
    /// </summary>
    public class UniversalWebClient : IWebClient
    {
        static HashSet<string> ContentHeaders { get; } = new HashSet<string>
        {
            { "Allow" },
            { "Content-Disposition" },
            { "Content-Encoding" },
            { "Content-Language" },
            { "Content-Length" },
            { "Content-Location" },
            { "Content-MD5" },
            { "Content-Range" },
            { "Content-Type" },
            { "Expires" },
            { "Last-Modified" }
        };

        static List<string> allowedHeaders { get; } = new List<string>
        {
            "x-parse-application-id",
            "x-parse-client-version",
            "x-parse-installation-id",
            "x-parse-session-token",
            "content-type"
        };
        public UniversalWebClient() { }

        public async UniTask<Tuple<HttpStatusCode, string>> ExecuteAsync(Models.WebRequest httpRequest)
        {
            Tuple<HttpStatusCode, string> result = default;

            UnityWebRequest webRequest; 

            switch (httpRequest.Method)
            {
                case "DELETE":
                    webRequest = UnityWebRequest.Delete(httpRequest.Target);
                    break;
                case "POST":
                    webRequest = CreatePostRequest(httpRequest);
                    break;
                case "PUT":
                    webRequest = CreatePutRequest(httpRequest);
                    break;
                default:
                    webRequest = UnityWebRequest.Get(httpRequest.Target);
                    break;
            }

            if (httpRequest.Headers != null)
            {
                foreach (KeyValuePair<string, string> header in httpRequest.Headers)
                {
                    if (webRequest.GetRequestHeader(header.Key) != null) continue;

                    if (!String.IsNullOrWhiteSpace(header.Value) &&
                        allowedHeaders.Contains(header.Key.ToLower()))
                    {
                        webRequest.SetRequestHeader(header.Key, header.Value);
                    }
                }
            }

            try
            {
                await webRequest.SendWebRequest();
            }
            catch (Exception exp)
            {
                Debug.LogError($"Target: {httpRequest.Target}");
                Debug.LogError($"Error: {exp.Message}");
            }

            HttpStatusCode responseStatus = HttpStatusCode.BadRequest;
            string responseText = "{}";

            if (Enum.IsDefined(typeof(HttpStatusCode), (int)webRequest.responseCode))
            {
                responseStatus = (HttpStatusCode)Enum.ToObject(typeof(HttpStatusCode), webRequest.responseCode);
            }
            if  (webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log("Error Getting Wallet Info: " + webRequest.error);
                responseText = webRequest.error;
            }
            else
            {
                responseText = webRequest.downloadHandler == null ? responseText : webRequest.downloadHandler.text;
            }

            result = new Tuple<HttpStatusCode, string>(responseStatus, responseText);
            
            // Signals that this UnityWebRequest is no longer being used, and should clean up any resources it is using.
            webRequest.Dispose();
            
            return result;
        }

        private UnityWebRequest CreatePostRequest(Models.WebRequest httpRequest)
        {
            var req = new UnityWebRequest(httpRequest.Target, "POST");
            Stream data = httpRequest.Data;

            if ((httpRequest.Data is null && httpRequest.Method.ToLower().Equals("post") ? new MemoryStream(new byte[0]) : httpRequest.Data) is Stream { } adjData)
            {
                data = adjData;
            }

            byte[] buffer = new byte[data.Length];
            data.Read(buffer, 0, buffer.Length);
            data.Position = 0;

            req.uploadHandler = (UploadHandler)new UploadHandlerRaw(buffer);
            req.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

            return req; 
        }

        private UnityWebRequest CreatePutRequest(Models.WebRequest httpRequest)
        {
            string requestData = null;
            Stream data = httpRequest.Data;

            if ((httpRequest.Data is null && httpRequest.Method.ToLower().Equals("post") ? new MemoryStream(new byte[0]) : httpRequest.Data) is Stream { } adjData)
            {
                data = adjData;
            }

            byte[] buffer = new byte[data.Length];
            data.Read(buffer, 0, buffer.Length);
            data.Position = 0;

            requestData = Encoding.UTF8.GetString(buffer);

            return UnityWebRequest.Put(httpRequest.Target, requestData);
        }
    }
}
