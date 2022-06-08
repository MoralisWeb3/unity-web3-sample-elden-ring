﻿using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System.Net;
using MoralisUnity.SolanaApi.Client;
using MoralisUnity.SolanaApi.Interfaces;
using MoralisUnity.SolanaApi.Models;

namespace MoralisUnity.SolanaApi.Api
{
    public class AccountApi : IAccountApi
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AccountApi"/> class.
		/// </summary>
		/// <param name="apiClient"> an instance of ApiClient (optional)</param>
		/// <returns></returns>
		public AccountApi(ApiClient apiClient = null)
		{
			if (apiClient == null) // use the default one in Configuration
				this.ApiClient = Configuration.DefaultApiClient;
			else
				this.ApiClient = apiClient;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="AccountApi"/> class.
		/// </summary>
		/// <returns></returns>
		public AccountApi(String basePath)
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
		public ApiClient ApiClient { get; set; }

		public async UniTask<NativeBalance> Balance(NetworkTypes network, string address)
        {
			// Verify the required parameter 'pairAddress' is set
			if (address == null) throw new ApiException(400, "Missing required parameter 'address' when calling Balance");

			var headerParams = new Dictionary<String, String>();

			var path = "/account/{network}/{address}/balance";
			path = path.Replace("{" + "network" + "}", ApiClient.ParameterToString(network.ToString()));
			path = path.Replace("{" + "address" + "}", ApiClient.ParameterToString(address));

			// Authentication setting, if any
			String[] authSettings = new String[] { "ApiKeyAuth" };

			Tuple<HttpStatusCode, Dictionary<string, string>, string> response = await ApiClient.CallApi(path, Method.GET, null, null, headerParams, null, null, authSettings);

			if (((int)response.Item1) >= 400)
				throw new ApiException((int)response.Item1, "Error calling Balance: " + response.Item3, response.Item3);
			else if (((int)response.Item1) == 0)
				throw new ApiException((int)response.Item1, "Error calling Balance: " + response.Item3, response.Item3);

			return ((CloudFunctionResult<NativeBalance>)ApiClient.Deserialize(response.Item3, typeof(CloudFunctionResult<NativeBalance>), response.Item2)).Result;
		}

		public async UniTask<List<SplTokenBalanace>> GetSplTokens(NetworkTypes network, string address)
		{
			// Verify the required parameter 'pairAddress' is set
			if (address == null) throw new ApiException(400, "Missing required parameter 'address' when calling GetSplTokens");

			var headerParams = new Dictionary<String, String>();

			var path = "/account/{network}/{address}/tokens";
			path = path.Replace("{" + "network" + "}", ApiClient.ParameterToString(network.ToString()));
			path = path.Replace("{" + "address" + "}", ApiClient.ParameterToString(address));

			// Authentication setting, if any
			String[] authSettings = new String[] { "ApiKeyAuth" };

			Tuple<HttpStatusCode, Dictionary<string, string>, string> response = await ApiClient.CallApi(path, Method.GET, null, null, headerParams, null, null, authSettings);

			if (((int)response.Item1) >= 400)
				throw new ApiException((int)response.Item1, "Error calling GetSplTokens: " + response.Item3, response.Item3);
			else if (((int)response.Item1) == 0)
				throw new ApiException((int)response.Item1, "Error calling GetSplTokens: " + response.Item3, response.Item3);

			return ((CloudFunctionResult<List<SplTokenBalanace>>)ApiClient.Deserialize(response.Item3, typeof(CloudFunctionResult<List<SplTokenBalanace>>), response.Item2)).Result;
		}

		public async UniTask<List<SplNft>> GetNFTs(NetworkTypes network, string address)
        {
			// Verify the required parameter 'pairAddress' is set
			if (address == null) throw new ApiException(400, "Missing required parameter 'address' when calling GetNFTs");

			var headerParams = new Dictionary<String, String>();

			var path = "/account/{network}/{address}/nft";
			path = path.Replace("{" + "network" + "}", ApiClient.ParameterToString(network.ToString()));
			path = path.Replace("{" + "address" + "}", ApiClient.ParameterToString(address));

			// Authentication setting, if any
			String[] authSettings = new String[] { "ApiKeyAuth" };

			Tuple<HttpStatusCode, Dictionary<string, string>, string> response = await ApiClient.CallApi(path, Method.GET, null, null, headerParams, null, null, authSettings);

			if (((int)response.Item1) >= 400)
				throw new ApiException((int)response.Item1, "Error calling GetNFTs: " + response.Item3, response.Item3);
			else if (((int)response.Item1) == 0)
				throw new ApiException((int)response.Item1, "Error calling GetNFTs: " + response.Item3, response.Item3);

			return ((CloudFunctionResult<List<SplNft>>)ApiClient.Deserialize(response.Item3, typeof(CloudFunctionResult<List<SplNft>>), response.Item2)).Result;
		}

		public async UniTask<Portfolio> GetPortfolio(NetworkTypes network, string address)
        {
			// Verify the required parameter 'pairAddress' is set
			if (address == null) throw new ApiException(400, "Missing required parameter 'address' when calling GetPortfolio");

			var headerParams = new Dictionary<String, String>();

			var path = "/account/{network}/{address}/portfolio";
			path = path.Replace("{" + "network" + "}", ApiClient.ParameterToString(network.ToString()));
			path = path.Replace("{" + "address" + "}", ApiClient.ParameterToString(address));

			// Authentication setting, if any
			String[] authSettings = new String[] { "ApiKeyAuth" };

			Tuple<HttpStatusCode, Dictionary<string, string>, string> response = await ApiClient.CallApi(path, Method.GET, null, null, headerParams, null, null, authSettings);

			if (((int)response.Item1) >= 400)
				throw new ApiException((int)response.Item1, "Error calling GetPortfolio: " + response.Item3, response.Item3);
			else if (((int)response.Item1) == 0)
				throw new ApiException((int)response.Item1, "Error calling GetPortfolio: " + response.Item3, response.Item3);

			return ((CloudFunctionResult<Portfolio>)ApiClient.Deserialize(response.Item3, typeof(CloudFunctionResult<Portfolio>), response.Item2)).Result;
		}
	}
}
