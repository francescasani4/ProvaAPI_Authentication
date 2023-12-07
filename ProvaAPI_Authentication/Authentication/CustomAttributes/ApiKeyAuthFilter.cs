using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ProvaAPI_Authentication.Authentication.CustomAttributes
{
	public class ApiKeyAuthFilter : IAuthorizationFilter
	{
		private readonly IApiKeyValidation _apiKeyValidation;

		public ApiKeyAuthFilter(IApiKeyValidation apiKeyValidation)
		{
			_apiKeyValidation = apiKeyValidation;
		}

		// implementazione dell'interfaccia IAuthorizationFilter
		public void OnAuthorization(AuthorizationFilterContext context)
		{
			string userApiKey = context.HttpContext.Request.Headers[ApiKeyConstants.ApiKeyHeaderName].ToString();

			if (string.IsNullOrWhiteSpace(userApiKey))
			{
				context.Result = new BadRequestResult();
				return;
			}

			if (!_apiKeyValidation.IsValidApiKey(userApiKey))
				context.Result = new UnauthorizedResult();
		}
	}
}

