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
