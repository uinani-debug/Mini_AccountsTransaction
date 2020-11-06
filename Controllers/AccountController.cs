using AutoMapper;
using AccountLibrary.API.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace AccountLibrary.API.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private IConfiguration _configuration;
        System.Net.Http.HttpClient client;

        public AccountController(
            IMapper mapper, ILogger<AccountController> logger, IConfiguration iConfig)
        {
           
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));

            _logger = logger;
            _configuration = iConfig;

            client = new System.Net.Http.HttpClient();
        }

        [Route("accountdetails/{accountNumber}")]
        [HttpGet]
        public async Task<ActionResult> GetAccounts(string accountNumber)
        {
            string token = string.Empty;
            _logger.LogInformation("Method start account");
            string url = _configuration.GetSection("MySettings").GetSection("URLAccountDetails").Value;
            string url1 = _configuration.GetSection("MySettings").GetSection("URLAccountTransaction").Value;

            url = url + "?AccountNumber=" + accountNumber;
            url1 = url1 + "?AccountNumber=" + accountNumber;
            _logger.LogInformation("http call start");
            var accountsResponse = await client.GetAsync(url);
            var accountsTransactionResponse = await client.GetAsync(url1);
            _logger.LogInformation("http call end");

            var responseBodyAsText = await accountsResponse.Content.ReadAsStringAsync();
            var accountdetailsresponse = JsonConvert.DeserializeObject<AccountDetails>(responseBodyAsText);

            var accountsTransactionAsText = await accountsTransactionResponse.Content.ReadAsStringAsync();
            var accounttransactionresponse = JsonConvert.DeserializeObject<IEnumerable<Transaction>>(accountsTransactionAsText);


            accountdetailsresponse.Transactions = accounttransactionresponse.ToList();


            _logger.LogInformation("string response " + responseBodyAsText);

            if (accountsResponse.StatusCode == System.Net.HttpStatusCode.OK)
                return Ok(accountdetailsresponse);
            return NotFound();

        }

        [Route("health")]
        [HttpGet]
        public ActionResult health()
        {
            return Ok();
        }

    }

}