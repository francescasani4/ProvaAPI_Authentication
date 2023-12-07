using System;
using Microsoft.AspNetCore.Authorization;

namespace ProvaAPI_Authentication.Authentication.Policy
{
    // IAuthorizationRequirement interfaccia che rappresenta un requisito
    // che un criterio di autorizzazione deve soddisfare per avere successo
    public class ApiKeyRequirement : IAuthorizationRequirement
    {
        // funge da classe indicatore per il requisito specifico dell'autenticazione della chiave
	}
}

