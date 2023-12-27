﻿using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace DbContextUtility.Models
{

    public class TableMetaDataEntity : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public string EntityName { get; set; }
        public string HostName { get; set; }
        public string DatabaseName { get; set; }
        public string Provider { get; set; }
    }

}
