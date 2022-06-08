/**
*            Module: DefiApi.cs
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

namespace MoralisUnity.Web3Api.Api
{
	/// <summary>
	/// Represents a collection of functions to interact with the API endpoints
	/// </summary>
	public class DefiApi : IDefiApi
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DefiApi"/> class.
		/// </summary>
		/// <param name="apiClient"> an instance of ApiClient (optional)</param>
		/// <returns></returns>
		public DefiApi(ApiClient apiClient = null)
		{
			if (apiClient == null) // use the default one in Configuration
				this.ApiClient = Configuration.DefaultApiClient; 
			else
				this.ApiClient = apiClient;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DefiApi"/> class.
		/// </summary>
		/// <returns></returns>
		public DefiApi(String basePath)
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
		/// Get the liquidity reserves for a given pair address
		/// </summary>
		/// <param name="pairAddress">Liquidity pair address</param>
		/// <param name="chain">The chain to query</param>
		/// <param name="toBlock">To get the reserves at this block number</param>
		/// <param name="toDate">Get the reserves to this date (any format that is accepted by momentjs)
		/// * Provide the param 'to_block' or 'to_date'
		/// * If 'to_date' and 'to_block' are provided, 'to_block' will be used.
		/// </param>
		/// <param name="providerUrl">web3 provider url to user when using local dev chain</param>
		/// <returns>Returns the pair reserves</returns>
		public async UniTask<ReservesCollection> GetPairReserves (string pairAddress, ChainList chain, string toBlock=null, string toDate=null, string providerUrl=null)
		{

			// Verify the required parameter 'pairAddress' is set
			if (pairAddress == null) throw new ApiException(400, "Missing required parameter 'pairAddress' when calling GetPairReserves");

			var postBody = new Dictionary<String, String>();
			var queryParams = new Dictionary<String, String>();
			var headerParams = new Dictionary<String, String>();
			var formParams = new Dictionary<String, String>();
			var fileParams = new Dictionary<String, FileParameter>();

			var path = "/{pair_address}/reserves";
			path = path.Replace("{format}", "json");
			path = path.Replace("{" + "pair_address" + "}", ApiClient.ParameterToString(pairAddress));
			queryParams.Add("chain", ApiClient.ParameterToHex((long)chain));
			if(toBlock != null) queryParams.Add("to_block", ApiClient.ParameterToString(toBlock));
			if(toDate != null) queryParams.Add("to_date", ApiClient.ParameterToString(toDate));
			if(providerUrl != null) queryParams.Add("provider_url", ApiClient.ParameterToString(providerUrl));

			// Authentication setting, if any
			String[] authSettings = new String[] { "ApiKeyAuth" };

			string bodyData = postBody.Count > 0 ? JsonConvert.SerializeObject(postBody) : null;

			Tuple<HttpStatusCode, Dictionary<string, string>, string> response =
				await ApiClient.CallApi(path, Method.GET, queryParams, bodyData, headerParams, formParams, fileParams, authSettings);

			if (((int)response.Item1) >= 400)
				throw new ApiException((int)response.Item1, "Error calling GetPairReserves: " + response.Item3, response.Item3);
			else if (((int)response.Item1) == 0)
				throw new ApiException((int)response.Item1, "Error calling GetPairReserves: " + response.Item3, response.Item3);

			return (ReservesCollection)ApiClient.Deserialize(response.Item3, typeof(ReservesCollection), response.Item2);
		}
		/// <summary>
		/// Fetches and returns pair data of the provided token0+token1 combination.
		/// The token0 and token1 options are interchangable (ie. there is no different outcome in "token0=WETH and token1=USDT" or "token0=USDT and token1=WETH")
		/// 
		/// </summary>
		/// <param name="exchange">The factory name or address of the token exchange</param>
		/// <param name="token0Address">Token0 address</param>
		/// <param name="token1Address">Token1 address</param>
		/// <param name="chain">The chain to query</param>
		/// <param name="toBlock">To get the reserves at this block number</param>
		/// <param name="toDate">Get the reserves to this date (any format that is accepted by momentjs)
		/// * Provide the param 'to_block' or 'to_date'
		/// * If 'to_date' and 'to_block' are provided, 'to_block' will be used.
		/// </param>
		/// <returns>Returns the pair address of the two tokens</returns>
		public async UniTask<ReservesCollection> GetPairAddress (string exchange, string token0Address, string token1Address, ChainList chain, string toBlock=null, string toDate=null)
		{

			// Verify the required parameter 'exchange' is set
			if (exchange == null) throw new ApiException(400, "Missing required parameter 'exchange' when calling GetPairAddress");

			// Verify the required parameter 'token0Address' is set
			if (token0Address == null) throw new ApiException(400, "Missing required parameter 'token0Address' when calling GetPairAddress");

			// Verify the required parameter 'token1Address' is set
			if (token1Address == null) throw new ApiException(400, "Missing required parameter 'token1Address' when calling GetPairAddress");

			var postBody = new Dictionary<String, String>();
			var queryParams = new Dictionary<String, String>();
			var headerParams = new Dictionary<String, String>();
			var formParams = new Dictionary<String, String>();
			var fileParams = new Dictionary<String, FileParameter>();

			var path = "/{token0_address}/{token1_address}/pairAddress";
			path = path.Replace("{format}", "json");
			path = path.Replace("{" + "token0_address" + "}", ApiClient.ParameterToString(token0Address));			path = path.Replace("{" + "token1_address" + "}", ApiClient.ParameterToString(token1Address));
			if(exchange != null) queryParams.Add("exchange", ApiClient.ParameterToString(exchange));
			queryParams.Add("chain", ApiClient.ParameterToHex((long)chain));
			if(toBlock != null) queryParams.Add("to_block", ApiClient.ParameterToString(toBlock));
			if(toDate != null) queryParams.Add("to_date", ApiClient.ParameterToString(toDate));

			// Authentication setting, if any
			String[] authSettings = new String[] { "ApiKeyAuth" };

			string bodyData = postBody.Count > 0 ? JsonConvert.SerializeObject(postBody) : null;

			Tuple<HttpStatusCode, Dictionary<string, string>, string> response =
				await ApiClient.CallApi(path, Method.GET, queryParams, bodyData, headerParams, formParams, fileParams, authSettings);

			if (((int)response.Item1) >= 400)
				throw new ApiException((int)response.Item1, "Error calling GetPairAddress: " + response.Item3, response.Item3);
			else if (((int)response.Item1) == 0)
				throw new ApiException((int)response.Item1, "Error calling GetPairAddress: " + response.Item3, response.Item3);

			return (ReservesCollection)ApiClient.Deserialize(response.Item3, typeof(ReservesCollection), response.Item2);
		}
	}
}
