using System;
namespace ProvaAPI_Authentication.Authentication
{
	// deve essere static?
	public class ApiKeyConstants
	{
		// nome dell'header HTTP che dovrebbe contenere la chiave API quando viene inviata una richiesta
		public const string ApiKeyHeaderName = "X-Api-Key";

		// nome della chiave API all'interno di una sezione nel file di configurazione
		public const string ApiKeySectionName = "Authentication:ApiKey";
	}
}

