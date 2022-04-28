using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagementAPI.Models;

namespace UserManagementAPI.Services
{
    public interface IUserService
    {
        Task<List<User>> GetUsersAsync();

        Task<TaskResponse> AddUserAsync(User user);
    }
}
