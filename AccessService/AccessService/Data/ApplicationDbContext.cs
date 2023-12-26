using AccessService.Models;
using Microsoft.EntityFrameworkCore;
using AccessService.Models.DTO;
using System.Collections.Generic;
using AccessService.Model.DTO;

namespace AccessService.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<EntityListMetadataModel> EntityListMetadataModels { get; set; }
        public DbSet<EntityColumnListMetadataModel> EntityColumnListMetadataModels { get; set; }
        public DbSet<UserTableModel> UserTableModelDTO { get; set; }
        public DbSet<UserRoleModel> UserRoleModelDTO { get; set; }
        public DbSet<LogParent> logParents { get; set; }

        public DbSet<LogChild> logChilds { get; set; }
        public DbSet<UserRoleModel> UserRoleModel { get; set; }
        public DbSet<UserTableModelDTO> UserTableModel { get; set; }

    }
}