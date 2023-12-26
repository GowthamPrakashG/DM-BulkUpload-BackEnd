namespace DynamicTableCreation.Models.DTO
{
    public class EntityListMetadataModelDTO
    {
        public int Id { get; set; }
        public string EntityName { get; set; }

        public string HostName { get; set; }
        public string DatabaseName { get; set; }

        public string Provider { get; set; }

        public static explicit operator EntityListMetadataModelDTO(EntityListMetadataModel data)
        {
            return new EntityListMetadataModelDTO
            {
                Id = data.Id,
                EntityName = data.EntityName,
                HostName = data.HostName,
                DatabaseName = data.DatabaseName,
                Provider = data.Provider,
            };
        }

        public static implicit operator EntityListMetadataModel(EntityListMetadataModelDTO data)
        {
            return new EntityListMetadataModel
            {
                Id = data.Id,
                EntityName = data.EntityName,
                HostName = data.HostName,
                DatabaseName = data.DatabaseName,
                Provider = data.Provider,
            };
        }
    }
}
