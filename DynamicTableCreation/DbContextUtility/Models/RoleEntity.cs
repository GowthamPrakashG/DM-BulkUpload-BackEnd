using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace DbContextUtility.Models
{
    public class RoleEntity 
    {
        [Key]
        public int Id { get; set; }
        public string RoleName { get; set; }
    }
}
