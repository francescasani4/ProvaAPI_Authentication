﻿using System;
using Microsoft.AspNetCore.Authorization;

namespace ProvaAPI_Authentication.Authentication.Policy
{
	public class ApiKeyHadler : AuthorizationHandler<ApiKeyRequirement>
	{
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IApiKeyValidation _apiKeyValidation;

		public ApiKeyHadler(IHttpContextAccessor httpContextAccessor, IApiKeyValidation apiKeyValidation)
		{
			_httpContextAccessor = httpContextAccessor;
			_apiKeyValidation = apiKeyValidation;
		}

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ApiKeyRequirement requirement)
		{
			string apiKey = _httpContextAccessor?.HttpContext?.Request.Headers[ApiKeyConstants.ApiKeyHeaderName].ToString();

			if(string.IsNullOrWhiteSpace(apiKey))
			{
				context.Fail();
				return Task.CompletedTask;
			}

			if(!_apiKeyValidation.IsValidApiKey(apiKey))
			{
				context.Fail();
				return Task.CompletedTask;
			}

			context.Succeed(requirement);

			return Task.CompletedTask;
		}
	}
}

