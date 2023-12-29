using DynamicTableCreation.Data;
using DynamicTableCreation.Models.DTO;
using DynamicTableCreation.Services.Interface;

namespace DynamicTableCreation.Services
{
    public class EntitylistService : IEntitylistService
    {
        private readonly ApplicationDbContext _context;
        public EntitylistService(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<EntityListDto> GetEntityList(string HostName, string DatabaseName, string ProviderName)
        {
            try
            {
                var entityNames = _context.EntityListMetadataModels
                    .Where(x => x.HostName.ToLower() == HostName.ToLower() && x.DatabaseName.ToLower() == DatabaseName.ToLower())
                    .Select(x => x.EntityName)
                    .ToList();

                // Create a list of EntityListDto
                var entityListDtos = entityNames.Select(entityName => new EntityListDto
                {
                    // Set properties of EntityListDto based on your logic
                    EntityName = entityName,
                    // Set other properties as needed
                }).ToList();

                return entityListDtos;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred in GetEntityList: {ex.Message}");
                throw;
            }
        }
    }
}
