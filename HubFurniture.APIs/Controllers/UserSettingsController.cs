using AutoMapper;
using HubFurniture.APIs.Dtos;
using HubFurniture.Core.Contracts.Contracts.Services;
using HubFurniture.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HubFurniture.APIs.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserSettingsController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAddcressService _addressService;
        private readonly IMapper _mapper;

        public UserSettingsController(UserManager<ApplicationUser> userManager, IAddcressService addressService, IMapper mapper)
        {
            _userManager = userManager;
            _addressService = addressService;
            _mapper = mapper;
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
                    return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
                }

                var changePasswordResult = await _userManager.ChangePasswordAsync(user, changePasswordDto.currentPassword, changePasswordDto.newPassword);
                if (!changePasswordResult.Succeeded)
                {
                    foreach (var error in changePasswordResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return BadRequest(ModelState);
                }

                return Ok("Password changed successfully.");
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
                    return BadRequest(ModelState);
                }

                var changeEmailResult = await _userManager.SetEmailAsync(user, changeEmailDto.Email);
                if (!changeEmailResult.Succeeded)
                {
                    foreach (var error in changeEmailResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return BadRequest(ModelState);
                }

                return Ok("Email changed successfully.");
            }

            return BadRequest(ModelState);
        }

        [HttpPost("AddAddress")]
        public async Task<IActionResult> AddAddress(UserAddressDto userAddressDto)
        {
            if (ModelState.IsValid)
            {
                // Get the current user
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser == null)
                {
                    return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
                }

                var userAddress = _mapper.Map<UserAddress>(userAddressDto);

                userAddress.User = currentUser;

                await _addressService.CreateAddressAsync(userAddress);

                return Ok("Address added successfully.");
            }

            return BadRequest(ModelState);
        }

        [HttpGet("GetAllAddresses")]
        public async Task<IActionResult> GetAllAddresses()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var addresses = await _addressService.GetAdressesForUserAsync(currentUser.Id);

            var addressDtos = _mapper.Map<List<UserAddressDto>>(addresses);

            return Ok(addressDtos);
        }

        [HttpPut("UpdateAddress")]
        public async Task<IActionResult> UpdateAddress(UserAddressDto userAddressDto)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser == null)
                {
                    return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
                }

                var updatedAddress = _mapper.Map<UserAddress>(userAddressDto);
                updatedAddress.UserId = currentUser.Id;

                await _addressService.UpdateAddressAsync(updatedAddress);

                return Ok("Address updated successfully.");
            }

            return BadRequest(ModelState);
        }

        [HttpGet("GetAddressById/{addressId}")]
        public async Task<IActionResult> GetAddressById(int addressId)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var address = await _addressService.GetAdressByIdForUserAsync(addressId);
            if (address == null)
            {
                return NotFound($"Address with ID '{addressId}' not found for the current user.");
            }

            var addressDto = _mapper.Map<UserAddressDto>(address);

            return Ok(addressDto);
        }

        [HttpDelete("DeleteAddress/{addressId}")]
        public async Task<IActionResult> DeleteAddress(int addressId)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var address = await _addressService.GetAdressByIdForUserAsync(addressId);
            if (address == null)
            {
                return NotFound($"Address with ID '{addressId}' not found for the current user.");
            }

            if (address.UserId != currentUser.Id)
            {
                return Forbid(); 
            }

            await _addressService.DeleteAdressByIdForUserAsync(addressId);

            return Ok("Address deleted successfully.");
        }



    }
}
