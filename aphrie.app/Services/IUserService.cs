using aphrie.app.Models;
using aphrie.DBL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace aphrie.app.Services
{
    public interface IUserService
    {
        Task<ApiResponse> RegisterAsync(RegisterModel model);

        Task<ApiResponse> LoginAsync(LoginModel model);
        Task<ApiResponse> AddFriendAsync(string userName);
    }
}
