using System;
namespace ProvaAPI_Authentication.Authentication
{
	public interface IApiKeyValidation
	{
		bool IsValidApiKey(string userApiKey);
	}
}

