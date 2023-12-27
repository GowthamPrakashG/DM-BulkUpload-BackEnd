﻿using MigrateEntity.Models.DTO;

namespace MigrateEntity.Service.IService
{
    public interface IPostgreSQLService
    {
        public Task<Dictionary<string, List<TableDetailsDTO>>> GetTableDetailsForAllTablesAsync(DBConnectionDTO dBConnection);
        public Task<List<string>> GetTableNamesAsync(DBConnectionDTO dBConnection);

        public Task<TableDetailsDTO> GetTableDetailsAsync(DBConnectionDTO dBConnection, string tableName);

        public Task<List<dynamic>> GetPrimaryColumnDataAsync(DBConnectionDTO dBConnection, string tableName);

        public Task<List<Dictionary<string, string>>> InsertData(DBConnectionDTO dBConnection, string tableName, Dictionary<string, string> data);
    }
}
