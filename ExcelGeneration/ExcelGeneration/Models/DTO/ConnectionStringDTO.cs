
namespace ExcelGeneration.Models.DTO
{
    public class ConnectionStringDTO
    {
        public int? parentId { get; set; }
        public string? tableName { get; set; }
        public string Host { get; set; }
        public string Database { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
