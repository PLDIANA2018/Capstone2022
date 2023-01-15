﻿using CoinbasePro;
using CoinbasePro.Network.Authentication;
using CryptoDevilAPI.DataAccess;
using CryptoDevilAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoDevilAPI.API.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("user")]
    public class UserDataController : ControllerBase
    {
        private readonly ILogger<UserDataController> _logger;
        private readonly IUserDataRepository _userRepository;
        private readonly IUserExchangeRepository _userExchangeRepository;

        public UserDataController(ILogger<UserDataController> logger, IUserDataRepository userRepository, IUserExchangeRepository userExchangeRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
            _userExchangeRepository = userExchangeRepository;
        }

        [HttpGet]
        //[Authorize]
        [Route("{userId}/data")]
        public async Task<ActionResult> GetPortfolioDataAsync(string userId)
        {
            var userExchange = await _userExchangeRepository.GetOneAsync(userId);
            if (userExchange == null)
            {
                return NotFound();
            }
            else
            {
                var result = await _userRepository.GetPortfolioDataAsync(userExchange);
                return Ok(result);
            }
        }

        [HttpGet]
        //[Authorize]
        [Route("{userId}/orders/filled")]
        public async Task<ActionResult> GetOrdersFilled(string userId)
        {
            var result = await _userRepository.GetOrdersFilledAsync(userId);
            return Ok(result);
        }

        [HttpGet]
        //[Authorize]
        [Route("{userId}/orders/open")]
        public async Task<ActionResult> GetOrdersOpen(string userId)
        {
            var result = await _userRepository.GetOrdersOpenAsync(userId);
            return Ok(result);
        }

        [HttpGet]
        //[Authorize]
        [Route("{userId}/runs")]
        public async Task<ActionResult> GetCompletedRuns(string userId)
        {
            var result = await _userRepository.GetCompletedRunsAsync(userId);
            return Ok(result);
 
        }
    }
}
