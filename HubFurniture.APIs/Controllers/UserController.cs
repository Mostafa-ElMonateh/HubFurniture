using HubFurniture.Core.Contracts.Contracts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using HubFurniture.Core.Entities;

namespace HubFurniture.APIs.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(IUserService userService, UserManager<ApplicationUser> userManager)
        {
            _userService = userService;
            _userManager = userManager;
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

            var basketId = await _userService.GetBasketId(currentUser.Id);
            return Ok(new { BasketId = basketId });
        }
    }
}
