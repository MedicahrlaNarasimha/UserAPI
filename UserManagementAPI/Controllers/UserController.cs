using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagementAPI.Dtos;
using UserManagementAPI.Models;
using UserManagementAPI.Services;

namespace UserManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region Private Varibles

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<UserController> _logger;
        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper _mapper;
        /// <summary>
        /// 
        /// </summary>
        private readonly IUserService _userService;
        #endregion Private Varibles

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="userService"></param>
        public UserController(ILogger<UserController> logger, IMapper mapper, IUserService userService)
        {
            _logger = logger;
            _mapper = mapper;
            _userService = userService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<User>), 200)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUsersAsync()
        {
            _logger.LogInformation("Getting  User List");
            var response = await _userService.GetUsersAsync();
            if (response != null)
            {
                return Ok(response);
            }
            return Ok(new TaskResponse { IsSuccess = false, SystemMessage = "Unable To Add a User, Please try again later.." });

        }

        /// <summary>
        /// Adding User details
        /// </summary>
        /// <param name="createUserRequestDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(TaskResponse), 200)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateUserAsync(CreateUserRequestDto createUserRequestDto)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation("Adding a User {FirstName}-{LastName}", createUserRequestDto.FirstName, createUserRequestDto.LastName);
                var userDetails = _mapper.Map<User>(createUserRequestDto);
                var response = await _userService.AddUserAsync(userDetails);
                if (response != null)
                {
                    return Ok(response);
                }
                return Ok(new TaskResponse { IsSuccess = false, SystemMessage = "Unable To Add a User, Please try again later.." });
            }
            else
            {
                return Ok(new TaskResponse { IsSuccess = false, SystemMessage = "Invalid request." });
            }
        }
    }
}
