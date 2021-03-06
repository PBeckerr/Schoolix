﻿using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Schoolix.Controllers
{
    public class OidcConfigurationController : ControllerBase
    {
        private readonly ILogger<OidcConfigurationController> logger;

        public OidcConfigurationController(IClientRequestParametersProvider clientRequestParametersProvider, ILogger<OidcConfigurationController> _logger)
        {
            this.ClientRequestParametersProvider = clientRequestParametersProvider;
            this.logger = _logger;
        }

        public IClientRequestParametersProvider ClientRequestParametersProvider { get; }

        [HttpGet("_configuration/{clientId}")]
        public IActionResult GetClientRequestParameters([FromRoute]string clientId)
        {
            var user = this.User;
            var parameters = this.ClientRequestParametersProvider.GetClientParameters(this.HttpContext, clientId);
            return this.Ok(parameters);
        }
    }
}