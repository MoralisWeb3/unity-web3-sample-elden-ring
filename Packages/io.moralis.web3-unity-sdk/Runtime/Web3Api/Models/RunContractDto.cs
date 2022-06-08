using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace MoralisUnity.Web3Api.Models
{
	[DataContract]
	public class RunContractDto
	{
		/// <summary>
		/// The contract abi
		/// </summary>
		[DataMember(Name = "abi", EmitDefaultValue = false)]
		[JsonProperty(PropertyName = "abi")]
		public object Abi { get; set; }

		/// <summary>
		/// The params for the given function
		/// </summary>
		[DataMember(Name = "params", EmitDefaultValue = false)]
		[JsonProperty(PropertyName = "params")]
		public object Params { get; set; }


		/// <summary>
		/// Get the string presentation of the object
		/// </summary>
		/// <returns>String presentation of the object</returns>
		public override string ToString()
		{
			var sb = new StringBuilder();
			sb.Append("class RunContractDto{");
			sb.Append("  Abi ").Append(Abi).Append("\n");
			sb.Append("  Params ").Append(Params).Append("\n");
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