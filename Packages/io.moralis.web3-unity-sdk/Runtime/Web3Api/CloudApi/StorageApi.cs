/**
*            Module: StorageApi.cs
*       Description: Represents a collection of functions to interact with the API endpoints
*            Author: Moralis Web3 Technology AB, 559307-5988 - David B. Goodrich
*  
* NOTE: THIS FILE HAS BEEN AUTOMATICALLY GENERATED. ANY CHANGES MADE TO THIS 
* FILE WILL BE LOST
*
* MIT License
*  
* Copyright (c) 2022 Moralis Web3 Technology AB, 559307-5988
*  
* Permission is hereby granted, free of charge, to any person obtaining a copy
* of this software and associated documentation files (the 'Software'), to deal
* in the Software without restriction, including without limitation the rights
* to use, copy, modify, merge, publish, distribute, sublicense, and / or sell
* copies of the Software, and to permit persons to whom the Software is
* furnished to do so, subject to the following conditions:
*
* The above copyright notice and this permission notice shall be included in all
* copies or substantial portions of the Software.
*
* THE SOFTWARE IS PROVIDED 'AS IS', WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
* OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
* SOFTWARE.
*/
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net;
using Cysharp.Threading.Tasks;
using MoralisUnity.Web3Api.Client;
using MoralisUnity.Web3Api.Core;
using MoralisUnity.Web3Api.Core.Models;
using MoralisUnity.Web3Api.Interfaces;
using MoralisUnity.Web3Api.Models;

namespace MoralisUnity.Web3Api.CloudApi
{
	/// <summary>
	/// Represents a collection of functions to interact with the API endpoints
	/// </summary>
	public class StorageApi : IStorageApi
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="StorageApi"/> class.
		/// </summary>
		/// <param name="apiClient"> an instance of ApiClient (optional)</param>
		/// <returns></returns>
		public StorageApi(ApiClient apiClient = null)
		{
			if (apiClient == null) // use the default one in Configuration
				this.ApiClient = Configuration.DefaultApiClient; 
			else
				this.ApiClient = apiClient;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="StorageApi"/> class.
		/// </summary>
		/// <returns></returns>
		public StorageApi(String basePath)
		{
			this.ApiClient = new ApiClient(basePath);
		}

		/// <summary>
		/// Sets the base path of the API client.
		/// </summary>
		/// <param name="basePath">The base path</param>
		/// <value>The base path</value>
		public void SetBasePath(String basePath)
		{
			this.ApiClient.BasePath = basePath;
		}

		/// <summary>
		/// Gets the base path of the API client.
		/// </summary>
		/// <param name="basePath">The base path</param>
		/// <value>The base path</value>
		public String GetBasePath(String basePath)
		{
			return this.ApiClient.BasePath;
		}

		/// <summary>
		/// Gets or sets the API client.
		/// </summary>
		/// <value>An instance of the ApiClient</value>
		public ApiClient ApiClient {get; set;}


		/// <summary>
		/// Uploads multiple files and place them in a folder directory
		/// 
		/// </summary>
		/// <param name="abi">Array of JSON and Base64 Supported</param>
		/// <returns>Returns the path to the uploaded files</returns>
		public async UniTask<List<IpfsFile>> UploadFolder (List<IpfsFileRequest> abi)
		{

			// Verify the required parameter 'abi' is set
			if (abi == null) throw new ApiException(400, "Missing required parameter 'abi' when calling UploadFolder");

			var postBody = new Dictionary<String, String>();
			var queryParams = new Dictionary<String, String>();
			var headerParams = new Dictionary<String, String>();
			var formParams = new Dictionary<String, String>();
			var fileParams = new Dictionary<String, FileParameter>();

			var path = "/functions/uploadFolder";
			if (abi != null) postBody.Add("abi", ApiClient.ParameterToString(abi));

			// Authentication setting, if any
			String[] authSettings = new String[] { "ApiKeyAuth" };

			string bodyData = postBody.Count > 0 ? JsonConvert.SerializeObject(postBody) : null;

			Tuple<HttpStatusCode, Dictionary<string, string>, string> response =
				await ApiClient.CallApi(path, Method.POST, queryParams, bodyData, headerParams, formParams, fileParams, authSettings);

			if (((int)response.Item1) >= 400)
				throw new ApiException((int)response.Item1, "Error calling UploadFolder: " + response.Item3, response.Item3);
			else if (((int)response.Item1) == 0)
				throw new ApiException((int)response.Item1, "Error calling UploadFolder: " + response.Item3, response.Item3);

			return ((CloudFunctionResult<List<IpfsFile>>)ApiClient.Deserialize(response.Item3, typeof(CloudFunctionResult<List<IpfsFile>>), response.Item2)).Result;
		}
	}
}
