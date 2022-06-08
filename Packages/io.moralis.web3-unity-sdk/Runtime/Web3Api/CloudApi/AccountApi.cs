/**
*            Module: AccountApi.cs
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
using UnityEngine;

namespace MoralisUnity.Web3Api.CloudApi
{
	/// <summary>
	/// Represents a collection of functions to interact with the API endpoints
	/// </summary>
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
		public ApiClient ApiClient {get; set;}


		/// <summary>
		/// Gets native transactions in descending order based on block number
		/// </summary>
		/// <param name="address">address</param>
		/// <param name="chain">The chain to query</param>
		/// <param name="subdomain">The subdomain of the moralis server to use (Only use when selecting local devchain as chain)</param>
		/// <param name="fromBlock">The minimum block number from where to get the transactions
		/// * Provide the param 'from_block' or 'from_date'
		/// * If 'from_date' and 'from_block' are provided, 'from_block' will be used.
		/// </param>
		/// <param name="toBlock">The maximum block number from where to get the transactions.
		/// * Provide the param 'to_block' or 'to_date'
		/// * If 'to_date' and 'to_block' are provided, 'to_block' will be used.
		/// </param>
		/// <param name="fromDate">The date from where to get the transactions (any format that is accepted by momentjs)
		/// * Provide the param 'from_block' or 'from_date'
		/// * If 'from_date' and 'from_block' are provided, 'from_block' will be used.
		/// </param>
		/// <param name="toDate">Get the transactions to this date (any format that is accepted by momentjs)
		/// * Provide the param 'to_block' or 'to_date'
		/// * If 'to_date' and 'to_block' are provided, 'to_block' will be used.
		/// </param>
		/// <param name="offset">offset</param>
		/// <param name="limit">limit</param>
		/// <returns>Returns a collection of native transactions.</returns>
		public async UniTask<TransactionCollection> GetTransactions (string address, ChainList chain, string subdomain=null, int? fromBlock=null, int? toBlock=null, string fromDate=null, string toDate=null, int? offset=null, int? limit=null)
		{

			// Verify the required parameter 'address' is set
			if (address == null) throw new ApiException(400, "Missing required parameter 'address' when calling GetTransactions");

			var postBody = new Dictionary<String, String>();
			var queryParams = new Dictionary<String, String>();
			var headerParams = new Dictionary<String, String>();
			var formParams = new Dictionary<String, String>();
			var fileParams = new Dictionary<String, FileParameter>();

			var path = "/functions/getTransactions";
			if (address != null) postBody.Add("address", ApiClient.ParameterToString(address));
			if (subdomain != null) postBody.Add("subdomain", ApiClient.ParameterToString(subdomain));
			if (fromBlock != null) postBody.Add("from_block", ApiClient.ParameterToString(fromBlock));
			if (toBlock != null) postBody.Add("to_block", ApiClient.ParameterToString(toBlock));
			if (fromDate != null) postBody.Add("from_date", ApiClient.ParameterToString(fromDate));
			if (toDate != null) postBody.Add("to_date", ApiClient.ParameterToString(toDate));
			if (offset != null) postBody.Add("offset", ApiClient.ParameterToString(offset));
			if (limit != null) postBody.Add("limit", ApiClient.ParameterToString(limit));
			postBody.Add("chain", ApiClient.ParameterToHex((long)chain));

			// Authentication setting, if any
			String[] authSettings = new String[] { "ApiKeyAuth" };

			string bodyData = postBody.Count > 0 ? JsonConvert.SerializeObject(postBody) : null;

			Tuple<HttpStatusCode, Dictionary<string, string>, string> response =
				await ApiClient.CallApi(path, Method.POST, queryParams, bodyData, headerParams, formParams, fileParams, authSettings);

			if (((int)response.Item1) >= 400)
				throw new ApiException((int)response.Item1, "Error calling GetTransactions: " + response.Item3, response.Item3);
			else if (((int)response.Item1) == 0)
				throw new ApiException((int)response.Item1, "Error calling GetTransactions: " + response.Item3, response.Item3);

			return ((CloudFunctionResult<TransactionCollection>)ApiClient.Deserialize(response.Item3, typeof(CloudFunctionResult<TransactionCollection>), response.Item2)).Result;
		}
		/// <summary>
		/// Gets native balance for a specific address
		/// </summary>
		/// <param name="address">The address for which the native balance will be checked</param>
		/// <param name="chain">The chain to query</param>
		/// <param name="providerUrl">web3 provider url to user when using local dev chain</param>
		/// <param name="toBlock">The block number on which the balances should be checked</param>
		/// <returns>Returns native balance for a specific address</returns>
		public async UniTask<NativeBalance> GetNativeBalance (string address, ChainList chain, string providerUrl=null, decimal? toBlock=null)
		{

			// Verify the required parameter 'address' is set
			if (address == null) throw new ApiException(400, "Missing required parameter 'address' when calling GetNativeBalance");

			var postBody = new Dictionary<String, String>();
			var queryParams = new Dictionary<String, String>();
			var headerParams = new Dictionary<String, String>();
			var formParams = new Dictionary<String, String>();
			var fileParams = new Dictionary<String, FileParameter>();

			var path = "/functions/getNativeBalance";
			if (address != null) postBody.Add("address", ApiClient.ParameterToString(address));
			if (providerUrl != null) postBody.Add("providerUrl", ApiClient.ParameterToString(providerUrl));
			if (toBlock != null) postBody.Add("to_block", ApiClient.ParameterToString(toBlock));
			postBody.Add("chain", ApiClient.ParameterToHex((long)chain));

			// Authentication setting, if any
			String[] authSettings = new String[] { "ApiKeyAuth" };

			string bodyData = postBody.Count > 0 ? JsonConvert.SerializeObject(postBody) : null;

			Tuple<HttpStatusCode, Dictionary<string, string>, string> response =
				await ApiClient.CallApi(path, Method.POST, queryParams, bodyData, headerParams, formParams, fileParams, authSettings);

			if (((int)response.Item1) >= 400)
				throw new ApiException((int)response.Item1, "Error calling GetNativeBalance: " + response.Item3, response.Item3);
			else if (((int)response.Item1) == 0)
				throw new ApiException((int)response.Item1, "Error calling GetNativeBalance: " + response.Item3, response.Item3);

			return ((CloudFunctionResult<NativeBalance>)ApiClient.Deserialize(response.Item3, typeof(CloudFunctionResult<NativeBalance>), response.Item2)).Result;
		}
		/// <summary>
		/// Gets token balances for a specific address
		/// </summary>
		/// <param name="address">The address for which token balances will be checked</param>
		/// <param name="chain">The chain to query</param>
		/// <param name="subdomain">The subdomain of the moralis server to use (Only use when selecting local devchain as chain)</param>
		/// <param name="toBlock">The block number on which the balances should be checked</param>
		/// <returns>Returns token balances for a specific address</returns>
		public async UniTask<List<Erc20TokenBalance>> GetTokenBalances (string address, ChainList chain, string subdomain=null, decimal? toBlock=null)
		{

			// Verify the required parameter 'address' is set
			if (address == null) throw new ApiException(400, "Missing required parameter 'address' when calling GetTokenBalances");

			var postBody = new Dictionary<String, String>();
			var queryParams = new Dictionary<String, String>();
			var headerParams = new Dictionary<String, String>();
			var formParams = new Dictionary<String, String>();
			var fileParams = new Dictionary<String, FileParameter>();

			var path = "/functions/getTokenBalances";
			if (address != null) postBody.Add("address", ApiClient.ParameterToString(address));
			if (subdomain != null) postBody.Add("subdomain", ApiClient.ParameterToString(subdomain));
			if (toBlock != null) postBody.Add("to_block", ApiClient.ParameterToString(toBlock));
			postBody.Add("chain", ApiClient.ParameterToHex((long)chain));

			// Authentication setting, if any
			String[] authSettings = new String[] { "ApiKeyAuth" };

			string bodyData = postBody.Count > 0 ? JsonConvert.SerializeObject(postBody) : null;

			Tuple<HttpStatusCode, Dictionary<string, string>, string> response =
				await ApiClient.CallApi(path, Method.POST, queryParams, bodyData, headerParams, formParams, fileParams, authSettings);

			if (((int)response.Item1) >= 400)
				throw new ApiException((int)response.Item1, "Error calling GetTokenBalances: " + response.Item3, response.Item3);
			else if (((int)response.Item1) == 0)
				throw new ApiException((int)response.Item1, "Error calling GetTokenBalances: " + response.Item3, response.Item3);

			return ((CloudFunctionResult<List<Erc20TokenBalance>>)ApiClient.Deserialize(response.Item3, typeof(CloudFunctionResult<List<Erc20TokenBalance>>), response.Item2)).Result;
		}
		/// <summary>
		/// Gets ERC20 token transactions in descending order based on block number
		/// </summary>
		/// <param name="address">address</param>
		/// <param name="chain">The chain to query</param>
		/// <param name="subdomain">The subdomain of the moralis server to use (Only use when selecting local devchain as chain)</param>
		/// <param name="fromBlock">The minimum block number from where to get the transactions
		/// * Provide the param 'from_block' or 'from_date'
		/// * If 'from_date' and 'from_block' are provided, 'from_block' will be used.
		/// </param>
		/// <param name="toBlock">The maximum block number from where to get the transactions.
		/// * Provide the param 'to_block' or 'to_date'
		/// * If 'to_date' and 'to_block' are provided, 'to_block' will be used.
		/// </param>
		/// <param name="fromDate">The date from where to get the transactions (any format that is accepted by momentjs)
		/// * Provide the param 'from_block' or 'from_date'
		/// * If 'from_date' and 'from_block' are provided, 'from_block' will be used.
		/// </param>
		/// <param name="toDate">Get the transactions to this date (any format that is accepted by momentjs)
		/// * Provide the param 'to_block' or 'to_date'
		/// * If 'to_date' and 'to_block' are provided, 'to_block' will be used.
		/// </param>
		/// <param name="offset">offset</param>
		/// <param name="limit">limit</param>
		/// <returns>Returns a collection of token transactions.</returns>
		public async UniTask<Erc20TransactionCollection> GetTokenTransfers (string address, ChainList chain, string subdomain=null, int? fromBlock=null, int? toBlock=null, string fromDate=null, string toDate=null, int? offset=null, int? limit=null)
		{

			// Verify the required parameter 'address' is set
			if (address == null) throw new ApiException(400, "Missing required parameter 'address' when calling GetTokenTransfers");

			var postBody = new Dictionary<String, String>();
			var queryParams = new Dictionary<String, String>();
			var headerParams = new Dictionary<String, String>();
			var formParams = new Dictionary<String, String>();
			var fileParams = new Dictionary<String, FileParameter>();

			var path = "/functions/getTokenTransfers";
			if (address != null) postBody.Add("address", ApiClient.ParameterToString(address));
			if (subdomain != null) postBody.Add("subdomain", ApiClient.ParameterToString(subdomain));
			if (fromBlock != null) postBody.Add("from_block", ApiClient.ParameterToString(fromBlock));
			if (toBlock != null) postBody.Add("to_block", ApiClient.ParameterToString(toBlock));
			if (fromDate != null) postBody.Add("from_date", ApiClient.ParameterToString(fromDate));
			if (toDate != null) postBody.Add("to_date", ApiClient.ParameterToString(toDate));
			if (offset != null) postBody.Add("offset", ApiClient.ParameterToString(offset));
			if (limit != null) postBody.Add("limit", ApiClient.ParameterToString(limit));
			postBody.Add("chain", ApiClient.ParameterToHex((long)chain));

			// Authentication setting, if any
			String[] authSettings = new String[] { "ApiKeyAuth" };

			string bodyData = postBody.Count > 0 ? JsonConvert.SerializeObject(postBody) : null;

			Tuple<HttpStatusCode, Dictionary<string, string>, string> response =
				await ApiClient.CallApi(path, Method.POST, queryParams, bodyData, headerParams, formParams, fileParams, authSettings);

			if (((int)response.Item1) >= 400)
				throw new ApiException((int)response.Item1, "Error calling GetTokenTransfers: " + response.Item3, response.Item3);
			else if (((int)response.Item1) == 0)
				throw new ApiException((int)response.Item1, "Error calling GetTokenTransfers: " + response.Item3, response.Item3);

			return ((CloudFunctionResult<Erc20TransactionCollection>)ApiClient.Deserialize(response.Item3, typeof(CloudFunctionResult<Erc20TransactionCollection>), response.Item2)).Result;
		}
		/// <summary>
		/// Gets NFTs owned by the given address
		/// * The response will include status [SYNCED/SYNCING] based on the contracts being indexed.
		/// * Use the token_address param to get results for a specific contract only
		/// * Note results will include all indexed NFTs
		/// * Any request which includes the token_address param will start the indexing process for that NFT collection the very first time it is requested
		/// 
		/// </summary>
		/// <param name="address">The owner of a given token</param>
		/// <param name="chain">The chain to query</param>
		/// <param name="format">The format of the token id</param>
		/// <param name="offset">offset</param>
		/// <param name="limit">limit</param>
		/// <returns>Returns a collection of nft owners</returns>
		public async UniTask<NftOwnerCollection> GetNFTs (string address, ChainList chain, string format=null, int? offset=null, int? limit=null)
		{

			// Verify the required parameter 'address' is set
			if (address == null) throw new ApiException(400, "Missing required parameter 'address' when calling GetNFTs");

			var postBody = new Dictionary<String, String>();
			var queryParams = new Dictionary<String, String>();
			var headerParams = new Dictionary<String, String>();
			var formParams = new Dictionary<String, String>();
			var fileParams = new Dictionary<String, FileParameter>();

			var path = "/functions/getNFTs";
			if (address != null) postBody.Add("address", ApiClient.ParameterToString(address));
			if (format != null) postBody.Add("format", ApiClient.ParameterToString(format));
			if (offset != null) postBody.Add("offset", ApiClient.ParameterToString(offset));
			if (limit != null) postBody.Add("limit", ApiClient.ParameterToString(limit));
			postBody.Add("chain", ApiClient.ParameterToHex((long)chain));

			// Authentication setting, if any
			String[] authSettings = new String[] { "ApiKeyAuth" };

			string bodyData = postBody.Count > 0 ? JsonConvert.SerializeObject(postBody) : null;

			Tuple<HttpStatusCode, Dictionary<string, string>, string> response =
				await ApiClient.CallApi(path, Method.POST, queryParams, bodyData, headerParams, formParams, fileParams, authSettings);

			if (((int)response.Item1) >= 400)
				throw new ApiException((int)response.Item1, "Error calling GetNFTs: " + response.Item3, response.Item3);
			else if (((int)response.Item1) == 0)
				throw new ApiException((int)response.Item1, "Error calling GetNFTs: " + response.Item3, response.Item3);

			return ((CloudFunctionResult<NftOwnerCollection>)ApiClient.Deserialize(response.Item3, typeof(CloudFunctionResult<NftOwnerCollection>), response.Item2)).Result;
		}
		/// <summary>
		/// Gets the transfers of the tokens matching the given parameters
		/// </summary>
		/// <param name="address">The sender or recepient of the transfer</param>
		/// <param name="chain">The chain to query</param>
		/// <param name="format">The format of the token id</param>
		/// <param name="direction">The transfer direction</param>
		/// <param name="offset">offset</param>
		/// <param name="limit">limit</param>
		/// <returns>Returns a collection of NFT transfer</returns>
		public async UniTask<NftTransferCollection> GetNFTTransfers (string address, ChainList chain, string format=null, string direction=null, int? offset=null, int? limit=null)
		{

			// Verify the required parameter 'address' is set
			if (address == null) throw new ApiException(400, "Missing required parameter 'address' when calling GetNFTTransfers");

			var postBody = new Dictionary<String, String>();
			var queryParams = new Dictionary<String, String>();
			var headerParams = new Dictionary<String, String>();
			var formParams = new Dictionary<String, String>();
			var fileParams = new Dictionary<String, FileParameter>();

			var path = "/functions/getNFTTransfers";
			if (address != null) postBody.Add("address", ApiClient.ParameterToString(address));
			if (format != null) postBody.Add("format", ApiClient.ParameterToString(format));
			if (direction != null) postBody.Add("direction", ApiClient.ParameterToString(direction));
			if (offset != null) postBody.Add("offset", ApiClient.ParameterToString(offset));
			if (limit != null) postBody.Add("limit", ApiClient.ParameterToString(limit));
			postBody.Add("chain", ApiClient.ParameterToHex((long)chain));

			// Authentication setting, if any
			String[] authSettings = new String[] { "ApiKeyAuth" };

			string bodyData = postBody.Count > 0 ? JsonConvert.SerializeObject(postBody) : null;

			Tuple<HttpStatusCode, Dictionary<string, string>, string> response =
				await ApiClient.CallApi(path, Method.POST, queryParams, bodyData, headerParams, formParams, fileParams, authSettings);

			if (((int)response.Item1) >= 400)
				throw new ApiException((int)response.Item1, "Error calling GetNFTTransfers: " + response.Item3, response.Item3);
			else if (((int)response.Item1) == 0)
				throw new ApiException((int)response.Item1, "Error calling GetNFTTransfers: " + response.Item3, response.Item3);

			return ((CloudFunctionResult<NftTransferCollection>)ApiClient.Deserialize(response.Item3, typeof(CloudFunctionResult<NftTransferCollection>), response.Item2)).Result;
		}
		/// <summary>
		/// Gets NFTs owned by the given address
		/// * Use the token_address param to get results for a specific contract only
		/// * Note results will include all indexed NFTs
		/// * Any request which includes the token_address param will start the indexing process for that NFT collection the very first time it is requested
		/// 
		/// </summary>
		/// <param name="address">The owner of a given token</param>
		/// <param name="tokenAddress">Address of the contract</param>
		/// <param name="chain">The chain to query</param>
		/// <param name="format">The format of the token id</param>
		/// <param name="offset">offset</param>
		/// <param name="limit">limit</param>
		/// <returns>Returns a collection of nft owners</returns>
		public async UniTask<NftOwnerCollection> GetNFTsForContract (string address, string tokenAddress, ChainList chain, string format=null, int? offset=null, int? limit=null)
		{

			// Verify the required parameter 'address' is set
			if (address == null) throw new ApiException(400, "Missing required parameter 'address' when calling GetNFTsForContract");

			// Verify the required parameter 'tokenAddress' is set
			if (tokenAddress == null) throw new ApiException(400, "Missing required parameter 'tokenAddress' when calling GetNFTsForContract");

			var postBody = new Dictionary<String, String>();
			var queryParams = new Dictionary<String, String>();
			var headerParams = new Dictionary<String, String>();
			var formParams = new Dictionary<String, String>();
			var fileParams = new Dictionary<String, FileParameter>();

			var path = "/functions/getNFTsForContract";
			if (address != null) postBody.Add("address", ApiClient.ParameterToString(address));
			if (tokenAddress != null) postBody.Add("token_address", ApiClient.ParameterToString(tokenAddress));
			if (format != null) postBody.Add("format", ApiClient.ParameterToString(format));
			if (offset != null) postBody.Add("offset", ApiClient.ParameterToString(offset));
			if (limit != null) postBody.Add("limit", ApiClient.ParameterToString(limit));
			postBody.Add("chain", ApiClient.ParameterToHex((long)chain));

			// Authentication setting, if any
			String[] authSettings = new String[] { "ApiKeyAuth" };

			string bodyData = postBody.Count > 0 ? JsonConvert.SerializeObject(postBody) : null;

			Tuple<HttpStatusCode, Dictionary<string, string>, string> response =
				await ApiClient.CallApi(path, Method.POST, queryParams, bodyData, headerParams, formParams, fileParams, authSettings);

			if (((int)response.Item1) >= 400)
				throw new ApiException((int)response.Item1, "Error calling GetNFTsForContract: " + response.Item3, response.Item3);
			else if (((int)response.Item1) == 0)
				throw new ApiException((int)response.Item1, "Error calling GetNFTsForContract: " + response.Item3, response.Item3);

			return ((CloudFunctionResult<NftOwnerCollection>)ApiClient.Deserialize(response.Item3, typeof(CloudFunctionResult<NftOwnerCollection>), response.Item2)).Result;
		}
	}
}
