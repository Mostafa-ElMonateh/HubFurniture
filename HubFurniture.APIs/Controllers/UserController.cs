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

        [HttpPut("editName")]
        public async Task<IActionResult> EditUserName([FromBody] EditUserDto editUserDto)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return Unauthorized();
            }

            currentUser.FirstName = editUserDto.FirstName;
            currentUser.LastName = editUserDto.LastName;

            var result = await _userManager.UpdateAsync(currentUser);

            if (result.Succeeded)
            {
                return Ok(new { message = "User's name updated successfully." });
            }
            else
            {
                return BadRequest(new { message = result.Errors });
            }
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto changePasswordDto)
        {

            if (changePasswordDto.newPassword != changePasswordDto.confirmPassword)
            {
                ModelState.AddModelError(string.Empty, "The new password and confirmation password do not match.");
                return BadRequest(ModelState);
            }

            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return NotFound(new { message = $"Unable to load user with ID '{_userManager.GetUserId(User)}'." });
                }

                var changePasswordResult = await _userManager.ChangePasswordAsync(user, changePasswordDto.currentPassword, changePasswordDto.newPassword);
                if (!changePasswordResult.Succeeded)
                {
                    foreach (var error in changePasswordResult.Errors)
                    {
                        ModelState.AddModelError("message", error.Description);
                    }
                    return BadRequest(ModelState);
                }

                return Ok(new { message = "Password changed successfully." });
            }

            return BadRequest(ModelState);
        }

        [HttpPost("ChangeEmail")]
        public async Task<IActionResult> ChangeEmail(ChangeEmailDto changeEmailDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
                }

                // Check if the current password is correct
                var isPasswordValid = await _userManager.CheckPasswordAsync(user, changeEmailDto.Password);
                if (!isPasswordValid)
                {
                    ModelState.AddModelError(string.Empty, "The current password is incorrect.");
                    return BadRequest(new {message = "The current password is incorrect." });
                }

                var changeEmailResult = await _userManager.SetEmailAsync(user, changeEmailDto.Email);
                if (!changeEmailResult.Succeeded)
                {
                    foreach (var error in changeEmailResult.Errors)
                    {
                        ModelState.AddModelError("message", error.Description);
                    }
                    return BadRequest(ModelState);
                }

                return Ok(new { message = "Email changed successfully." });
            }

            return BadRequest(ModelState);
        }

        [HttpPost("ValidatePassword")]
        public async Task<IActionResult> ValidatePassword(ValidatePasswordDto validatePasswordDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
                }

                var isPasswordValid = await _userManager.CheckPasswordAsync(user, validatePasswordDto.currentPassword);
                if (isPasswordValid)
                {
                    return Ok(new { message = "Password is valid." });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "The current password is incorrect.");
                    return BadRequest(new {message = "The current password is incorrect."});
                }
            }

            return BadRequest(ModelState);
        }


    }
}
