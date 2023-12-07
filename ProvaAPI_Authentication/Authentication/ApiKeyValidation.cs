using System;
namespace ProvaAPI_Authentication.Authentication
{
	public class ApiKeyValidation : IApiKeyValidation
	{
		// permette di recuperare la chiave dalla configurazione
		private readonly IConfiguration _cofiguration;

		public ApiKeyValidation(IConfiguration configuration)
		{
			_cofiguration = configuration;
		}

		public bool IsValidApiKey(string userApiKey)
		{
			if(string.IsNullOrWhiteSpace(userApiKey))
			{
				return false;
			}

			string? apiKey = _cofiguration.GetValue<string>(ApiKeyConstants.ApiKeySectionName);

			if(apiKey == null || apiKey != userApiKey)
			{
				return false;
			}

			return true;
		}
	}
}

