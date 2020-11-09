using aphrie.app.Extensions;
using aphrie.app.Models;
using aphrie.BLL;
using aphrie.DBL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace aphrie.app.Services
{
    public class PostService : IPostServie
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly UnitOfWork _unitOfWork;
        public PostService( 
            UnitOfWork unitOfWork, 
            IHttpContextAccessor httpContextAccessor,
            UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<ApiResponse> AddPostAsync(PostModel post)
        {
            var id = _httpContextAccessor.HttpContext.User.GetLoggedInUserId<string>();
            var m = new List<Media>(); 
            foreach (var item in post.ImgSrc)
            {
                var newMedia = new Media()
                {
                    Id = Guid.NewGuid().ToString(),
                    Path = item
                };
                m.Add(newMedia);
                await _unitOfWork.MediaManager.AddAsync(newMedia);
            };

            var p = new Post
            {
                Id = Guid.NewGuid().ToString(),
                Description = post.Description,
                UserId = id,
                Medias = m
            };

            await _unitOfWork.PostManager.AddAsync(p);
            return new ApiResponse
            {
                Data = "Your post has been added",
                StatusCode = (int)HttpStatusCode.OK
            };
        }

        public async Task<ApiResponse> GetAllPostsAsync()
        {
            var email = _httpContextAccessor.HttpContext.User.GetLoggedInUserEmail();
            var user = await _userManager.FindByEmailAsync(email);
            var allFriends = _unitOfWork.FriendManager.GetAll().Where(f => f.UserId == user.Id.ToString()).ToList();
            //if friendlist is null
            if(allFriends.Count == 0)
            {
                return new ApiResponse
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Data = "No Posts found"
                };
            }
            var friendsPosts = new List<Post>();

            foreach (var item in allFriends)
            {
                friendsPosts.AddRange(_unitOfWork.PostManager.GetAllBind().Where(p => p.UserId == item.FriendId));
            }

            if(friendsPosts.Count == 0)
            {
                return new ApiResponse
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Data = "No Posts found"
                };
            }

            return new ApiResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Data = friendsPosts
            };
        }

        public async Task<ApiResponse> DeletePostAsync(string postId)
        {
            var Id = _httpContextAccessor.HttpContext.User.GetLoggedInUserId<string>();
            var userPosts = _unitOfWork.PostManager.GetAll().Where(p => p.UserId == Id);

            var post = userPosts.FirstOrDefault(p => p.Id == postId);

            if (post == null)
            {
                return new ApiResponse
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Data = "Not post found with this Id to be removed"
                };
            }
            await _unitOfWork.PostManager.RemoveAsync(post);
            return new ApiResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Data = "Your Post has been deleted"
            };

        }
    }

}
