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
                var entityList = _context.EntityListMetadataModels.Where(entlist => entlist.HostName == HostName && entlist.DatabaseName == DatabaseName && entlist.Provider == ProviderName)
                    .Select(entlist => new EntityListDto { EntityName = entlist.EntityName })
                    .ToList();
                return entityList;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred in GetEntityList: {ex.Message}");
                throw; 
            }
        }

    }
}
