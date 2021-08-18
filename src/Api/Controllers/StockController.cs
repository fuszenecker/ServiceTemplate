﻿using System.Collections.Generic;
using System.Linq;
using System.Net;
using DigitalThinkers.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DigitalThinkers.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class StockController : RestControllerBase
    {
        private readonly ILogger<StockController> logger;
        private readonly IMonetaryService monetaryService;

        public StockController(ILogger<StockController> logger, IMonetaryService monetaryService)
        {
            this.logger = logger;
            this.monetaryService = monetaryService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Dictionary<string, uint>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult Post([FromBody]IDictionary<string, uint> request)
        {
            if (request is null)
            {
                const string message = "Request body is empty";
                this.logger.LogWarning(message);
                return BadRequest(message);
            }

            IEnumerable<string> nonNumericKeys = GetNonNumericKeys(request);

            if (nonNumericKeys.Any())
            {
                this.logger.LogWarning("Keys are not numeric: {keys}", nonNumericKeys);
                return BadRequest($"Keys {string.Join(',', nonNumericKeys)} are not numbers.");
            }

            var notes = MapNotes(request);
            this.logger.LogDebug("Storing notes: {@notes}", notes);
            monetaryService.StoreNotes(notes);

            return Ok(monetaryService.GetNotes());
        }

        [HttpGet]
        [ProducesResponseType(typeof(Dictionary<string, uint>), (int)HttpStatusCode.OK)]
        public IActionResult Get()
        {
            return Ok(monetaryService.GetNotes());
        }
    }
}
