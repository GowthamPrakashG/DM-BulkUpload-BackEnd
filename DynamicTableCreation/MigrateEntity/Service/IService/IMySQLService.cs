﻿using MigrateEntity.Models.DTO;

namespace MigrateEntity.Service.IService
{
    public interface IMySQLService
    {
        public Task<Dictionary<string, List<TableDetailsDTO>>> GetTableDetailsForAllTablesAsync(DBConnectionDTO connectionDTO);
        public Task<List<string>> GetTableNamesAsync(DBConnectionDTO dBConnection);

        public Task<TableDetailsDTO> GetTableDetailsAsync(DBConnectionDTO dBConnection, string tableName);

        public Task<List<dynamic>> GetPrimaryColumnDataAsync(DBConnectionDTO dBConnection, string tableName);
    }
}
