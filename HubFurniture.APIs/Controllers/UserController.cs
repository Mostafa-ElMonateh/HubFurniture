using HubFurniture.Core.Contracts.Contracts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using HubFurniture.Core.Entities;
using HubFurniture.APIs.Dtos;
using AutoMapper;
using static StackExchange.Redis.Role;

namespace HubFurniture.APIs.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;


        public UserController(IUserService userService, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userService = userService;
            _userManager = userManager;
            _mapper = mapper;

        }

        [HttpPost("setBasketId")]
        public async Task<IActionResult> SetBasketId(string basketId)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return Unauthorized(); 
            }

            await _userService.UpdateBasketId(currentUser.Id, basketId);
            return Ok("Basket ID updated successfully.");
        }

        [HttpGet("getBasketId")]
        public async Task<IActionResult> GetBasketId()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return Unauthorized(); 
            }

            var basketId = await _userService.GetBasketId(currentUser.BasketId);
            return Ok(new { BasketId = basketId });
        }

        [HttpGet("getUserInfo")]
        public async Task<IActionResult> GetUserInfo()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return Unauthorized();
            }

            var currentApplicationUser = await _userService.GetUserById(currentUser.Id);
            var userInfoDto = _mapper.Map<UserInfoDto>(currentApplicationUser);
            return Ok(userInfoDto );
        }
    }
}
