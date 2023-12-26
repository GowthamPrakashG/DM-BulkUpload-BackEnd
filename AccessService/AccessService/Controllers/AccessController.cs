using Microsoft.AspNetCore.Mvc;
using AccessService.Models.DTO;
using AccessService.Services;
using AccessService.Model.DTO;
using System.Net;
using AccessService.Models;
using Azure;

namespace AccessService.Controllers
{
    public class AccessController : Controller
    {
        protected APIResponse _response;
        private readonly AccessesService _accessService;

        public AccessController(AccessesService accessService)
        {
            _accessService = accessService;
            _response = new();
        }

        [HttpPost("login")]
        public async Task<ActionResult<APIResponse>> Login([FromBody] LoginViewModelDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessage = ModelState.Values.SelectMany(c => c.Errors).Select(e => e.ErrorMessage).ToList();
                    return BadRequest(_response);
                }

                var userDetailsDTO = await _accessService.AuthenticateAsync(model.Email, model.Password);
                if (userDetailsDTO.RoleName != null || userDetailsDTO.Token != null)
                {
                    _response.StatusCode = HttpStatusCode.OK;
                    _response.IsSuccess = true;
                    _response.Result = userDetailsDTO;
                    return Ok(_response);
                }
                else
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessage = new List<string>() { "Username or password is incorrect" };
                    return BadRequest(_response);
                }
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { $"An error occurred while processing your request: {Environment.NewLine} {ex.Message} " };
                return BadRequest(_response);
            }
        }

        [HttpPost("createUser")]
        public async Task<ActionResult<APIResponse>> CreateUser([FromBody] UserTableModelDTO userModel)
        {
            try
            {
                if (!ModelState.IsValid || userModel == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessage = ModelState.Values.SelectMany(c => c.Errors).Select(e => e.ErrorMessage).ToList();
                    return BadRequest(_response);
                }

                var createdUser = await _accessService.CreateUserAsync(userModel);

                if (createdUser != null)
                {
                    _response.StatusCode = HttpStatusCode.OK;
                    _response.IsSuccess = true;
                    _response.Result = createdUser;
                    return Ok(_response);
                }
                else
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessage = new List<string>() { "Failed to create user. Check role details and EmailId" };
                    return BadRequest(_response);
                }
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { $"An error occurred while processing your request: {Environment.NewLine} { ex.Message } " };
                return BadRequest(_response);
            }
        }
    }
}
