using System;
using Microsoft.AspNetCore.Mvc;

namespace ProvaAPI_Authentication.Authentication.CustomAttributes
{
	public class ApiKeyAttribute : ServiceFilterAttribute
	{
		public ApiKeyAttribute() : base(typeof(ApiKeyAuthFilter))
		{
		}
	}
}

