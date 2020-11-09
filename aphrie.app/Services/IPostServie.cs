using aphrie.app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace aphrie.app.Services
{
    public interface IPostServie
    {
        Task<ApiResponse> AddPostAsync(PostModel post);
        Task<ApiResponse> GetAllPostsAsync();
        Task<ApiResponse> DeletePostAsync(string postId);
    }
}
