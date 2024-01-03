namespace MigrateEntity.Models.DTO
{
    public class TableDetailsDTO
    {
        public string TableName { get; set; }
        public List<ColumnDetailsDTO> Columns { get; set; }
    }
}
