using Microsoft.AspNetCore.Mvc;
using MigrateEntity.Models.DTO;
using MigrateEntity.Service;
using MigrateEntity.Service.IService;
using Npgsql;
using System.Net;

namespace MigrateEntity.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EntityMigrateController : ControllerBase
    {
        private readonly IGeneralDatabaseService _generalDatabaseService;
        public EntityMigrateController(GeneralDatabaseService generalDatabaseService)
        {
            _generalDatabaseService = generalDatabaseService;
        }

        [HttpGet("GetTableDetails")]
        public async Task<ActionResult<APIResponse>> GetTableDetails([FromQuery] DBConnectionDTO connectionDto)
        {
            try
            {
                var tabledetails = await _generalDatabaseService.GetTableDetailsForAllTablesAsync(connectionDto);

                //var insertedTables = connectionStringService.AddTableDetailsToDatabase(tableDetails, connectionDto.Host, connectionDto.Database, connectionDto.Username);

                var responseModel = new APIResponse
                {
                    StatusCode = HttpStatusCode.OK,
                    IsSuccess = true,
                    Result = tabledetails
                };
                return Ok(responseModel);
            }
            catch (Exception ex)
            {
                var responseModel = new APIResponse
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    IsSuccess = false,
                    ErrorMessages = new List<string> { ex.Message },
                    Result = null
                };
                return StatusCode((int)responseModel.StatusCode, responseModel);
            }
        }
    }
}
