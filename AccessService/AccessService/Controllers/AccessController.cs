using Microsoft.AspNetCore.Mvc;
using AccessService.Models.DTO;
using AccessService.Services;
using AccessService.Model.DTO;
using System.Net;
using AccessService.Models;
namespace AccessService.Controllers
{
    public class AccessController : Controller
    {
        private readonly AccessesService _accessService;

        public AccessController(AccessesService accessService)
        {
            _accessService = accessService; 
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModelDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var userDetailsDTO = await _accessService.AuthenticateAsync(model.Email, model.Password);
                if (userDetailsDTO != null)
                {
                    var responseModel = new APIResponse
                    {
                        StatusCode = HttpStatusCode.OK,
                        IsSuccess = true,
                        Result = userDetailsDTO.Token
                    };
                    return Ok(responseModel);
                }
                else
                {
                    var errorResponse = new
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        IsSuccess = false,
                        ErrorMessage = "Invalid credentials"
                    };
                    return BadRequest(errorResponse);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost("createUser")]
        public async Task<IActionResult> CreateUser([FromBody] UserTableModelDTO userModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errorResponse = new
                    {
                        StatusCode = HttpStatusCode.SeeOther,
                        IsSuccess = false,
                        ErrorMessage = "Invalid credentials"
                    };
                    return BadRequest(ModelState);
                }

                var createdUser = await _accessService.CreateUserAsync(userModel);

                if (createdUser != null)
                {
                    var errorResponse = new
                    {
                        StatusCode = HttpStatusCode.OK,
                        IsSuccess = true,
                        Result = createdUser
                    };
                    return Ok(errorResponse);
                }
                else
                {
                    var errorResponse = new
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        IsSuccess = false,
                        ErrorMessage = "Failed to create user. Check role details and EmailId"
                    };
                    return BadRequest(errorResponse);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
