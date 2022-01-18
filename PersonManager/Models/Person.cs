using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PersonManager.Models
{
    public class Person
    {
		[JsonProperty(PropertyName = "id")]
		public string Id { get; set; }

		[Required]
		[JsonProperty(PropertyName = "firstName")]
		public string FirstName { get; set; }

		[Required]
		[JsonProperty(PropertyName = "lastName")]
		public string LastName { get; set; }

		[Required]
		[EmailAddress]
		[JsonProperty(PropertyName = "email")]
		public string Email { get; set; }
	}
}