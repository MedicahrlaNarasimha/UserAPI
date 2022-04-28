using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UserManagementAPI.Models;

namespace UserManagementAPI.Services
{
    public class UserService : IUserService
    {

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<UserService> _logger;
        public UserService(ILogger<UserService> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<TaskResponse> AddUserAsync(User user)
        {
            try
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "UserData.json");
                if (!File.Exists(filePath))
                {
                    File.Create(filePath);
                    var jsonData = JsonConvert.SerializeObject(user);
                    System.IO.File.WriteAllText(filePath, jsonData);
                }
                else
                {
                    var jsonData = System.IO.File.ReadAllText(filePath);
                    var userList = JsonConvert.DeserializeObject<List<User>>(jsonData)
                                          ?? new List<User>();
                    userList.Add(new User { FirstName = user.FirstName, LastName = user.LastName });
                    jsonData = JsonConvert.SerializeObject(userList);
                    System.IO.File.WriteAllText(filePath, jsonData);
                }
                return new TaskResponse { IsSuccess = true, SystemMessage = "User details addedd sucessfully." };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                return new TaskResponse { IsSuccess = false, SystemMessage = "Error while adding user, Please try again..." };
            }
        }

        public async Task<List<User>> GetUsersAsync()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "UserData.json");
            var jsonData = System.IO.File.ReadAllText(filePath);
            var userList = JsonConvert.DeserializeObject<List<User>>(jsonData)
                                          ?? new List<User>();
            return userList;
        }
    }
}
