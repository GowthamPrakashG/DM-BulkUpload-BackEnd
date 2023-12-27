using DynamicTableCreation.Models.DTO;

namespace DynamicTableCreation.Services.Interface
{
    public interface IEntitylistService
    {
        IEnumerable<EntityListDto> GetEntityList(string HostName, string DatabaseName, string ProviderName);
    }
}
