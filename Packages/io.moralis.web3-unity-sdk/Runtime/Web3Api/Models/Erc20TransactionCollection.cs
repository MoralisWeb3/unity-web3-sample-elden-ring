using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace MoralisUnity.Web3Api.Models
{
	[DataContract]
	public class Erc20TransactionCollection
	{
		/// <summary>
		/// The total number of matches for this query
		/// example: 2000
		/// </summary>
		[DataMember(Name = "total", EmitDefaultValue = false)]
		[JsonProperty(PropertyName = "total")]
		public int? Total { get; set; }

		/// <summary>
		/// The page of the current result
		/// example: 2
		/// </summary>
		[DataMember(Name = "page", EmitDefaultValue = false)]
		[JsonProperty(PropertyName = "page")]
		public int? Page { get; set; }

		/// <summary>
		/// The number of results per page
		/// example: 100
		/// </summary>
		[DataMember(Name = "page_size", EmitDefaultValue = false)]
		[JsonProperty(PropertyName = "page_size")]
		public int? PageSize { get; set; }

		/// <summary>
		/// </summary>
		[DataMember(Name = "result", EmitDefaultValue = false)]
		[JsonProperty(PropertyName = "result")]
		public List<Erc20Transaction> Result { get; set; }


		/// <summary>
		/// Get the string presentation of the object
		/// </summary>
		/// <returns>String presentation of the object</returns>
		public override string ToString()
		{
			var sb = new StringBuilder();
			sb.Append("class Erc20TransactionCollection{");
			sb.Append("  Total ").Append(Total).Append("\n");
			sb.Append("  Page ").Append(Page).Append("\n");
			sb.Append("  PageSize ").Append(PageSize).Append("\n");
			sb.Append("  Result ").Append(Result).Append("\n");
			sb.Append("}");

			return sb.ToString();
		}

		/// <summary>
		/// Get the JSON string presentation of the object
		/// </summary>
		/// <returns>JSON string presentation of the object</returns>
		public string ToJson()
		{
			return JsonConvert.SerializeObject(this, Formatting.Indented);
		}

	}
}