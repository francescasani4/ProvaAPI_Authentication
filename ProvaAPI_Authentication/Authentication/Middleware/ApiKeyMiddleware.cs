using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace ProvaAPI_Authentication.Authentication.Middleware
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IApiKeyValidation _apiKeyValidation;

        public ApiKeyMiddleware(RequestDelegate next, IApiKeyValidation apiKeyValidation)
        {
            _next = next;
            _apiKeyValidation = apiKeyValidation;
        }

        // metodo che costituisce il punto di ingresso per il middleware,
        // viene chiamato questo metodo per ogni richiesta HTTP che passa attraverso la pipeline del middleware
        public async Task InvokeAsync(HttpContext context)
        {
            if (string.IsNullOrWhiteSpace(context.Request.Headers[ApiKeyConstants.ApiKeyHeaderName]))
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return;
            }

            string? userApiKey = context.Request.Headers[ApiKeyConstants.ApiKeyHeaderName];

            if (!_apiKeyValidation.IsValidApiKey(userApiKey))
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return;
            }

            // metodo per richiamare il componente successivo del mmiddleware nella pipeline
            await _next(context);
        }
    }
}

